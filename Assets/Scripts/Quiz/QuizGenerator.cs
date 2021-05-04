using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Quiz;

public class QuizGenerator : MonoBehaviour{
    //references. Public because they're probably disabled at runtime.
    [Header("UI Text")]
    public Text titleText;
    public Text questionText;
    public Text subtitleText;
    
    [Header("Difficulty")]
    public GameObject difficultySelector;
    public Toggle[] toggles;        //0 = easy, 1 = normal, 2 = clinical.

    [Header("Helpers")]
    public DropBox box;             //used on Q_Box questions
    public RegionPicker picker;     //used on Q_Region questions
    public GameObject sphere;       //used on Q_Region questions
    private BrainMaster2 brain;
    public Image quizBackground;

    //data cache
    public string databaseFilename = "QuestionDatabase";     //customizeable database. Tab-delimited file must be uploaded to Resources.
    private Q[] qdb;                //the generated question database.
    private bool[] difficulties = new bool[]{true, true, true};     //easy, normal, clinical.

    //variables
    private Quiz.Status currentStatus = Status.Expired;    //used for logic flow. sort of a state machine.
    private int wrongAnswerCount = 0;           //scorekeeping
    private int correctAnswerCount = 0;         //scorekeeping

    void OnEnable() {
        if (brain == null) brain = GameObject.Find("blender-brain-opt").GetComponent<BrainMaster2>();
        changeDifficulty();

        //this gets triggered on returns to quiz mode, but not the first time.
        if (currentStatus == Status.Inactive) nextQuestion();
    }

    void Update() {
        //cheat codes :) 
        if (Application.isEditor) {
            if (Input.GetKeyUp(KeyCode.F1)) currentStatus = Status.Right;
            if (Input.GetKeyUp(KeyCode.F2)) currentStatus = Status.Wrong;
            if (Input.GetKeyUp(KeyCode.F3)) picker.gameObject.SetActive(!picker.gameObject.activeSelf);
            if (Input.GetKeyUp(KeyCode.F4)) box.gameObject.SetActive(!box.gameObject.activeSelf);
            if (Input.GetKeyUp(KeyCode.F5)) sphere.SetActive(!sphere.activeSelf);
        }

        if (Input.GetKeyUp(KeyCode.Space)) nextQuestion();
    }

    //UTILITIES
    public void changeDifficulty() {
        //sync the internal bools with the GUI elements
        //0 = easy, 1 = normal, 2 = clinical.
        difficulties[0] = toggles[0].isOn;
        difficulties[1] = toggles[1].isOn;
        difficulties[2] = toggles[2].isOn;
    }

    void generateDB(){
        //generate the database
        changeDifficulty();
        difficultySelector.SetActive(false);
        qdb = Quiz.Load.LoadAll(databaseFilename, difficulties);
        Debug.Log("Database generated: " + qdb.Length.ToString() + " questions.");

        //randomize the question order with the knuth shuffle:
        System.Random random = new System.Random();
        for (int i = 0; i < qdb.Length; i++) {
            int j = random.Next(i, qdb.Length);
            Q temp = qdb[i];
            qdb[i] = qdb[j];
            qdb[j] = temp;
        }

        //string debugPlaylist = "Playlist = ";
        //for (int i = 0; i < qdb.Length; i++) {
        //    debugPlaylist += qdb[i].ID.ToString() + ", ";
        //}
        //Debug.Log(debugPlaylist);
    }

    public void nextQuestion(){
        if (menuMaster.currentMode != menuMaster.Modes.Quiz) return;
        if (qdb == null) generateDB();

        //hitting the button without answering counts as a wrong answer.
        if (currentStatus == Status.Waiting)
            currentStatus = Status.Wrong;
        else 
            generateQuestion();
    }

    //this gets triggered when the switch button exits quiz mode
    void OnDisable() {
        brain.stopAllFlashing();
        if (sphere != null) sphere.SetActive(false);
        picker.gameObject.SetActive(false);
        box.gameObject.SetActive(false);        
        StopAllCoroutines();
        CancelInvoke();
        currentStatus = Status.Inactive;
    }

    //queue up the next question in the randomized playlist;
    void generateQuestion(){
        if (correctAnswerCount + wrongAnswerCount >= qdb.Length) {
            quizCompleted();
            return;
        }

        generateQuestion(correctAnswerCount + wrongAnswerCount);
    }

    //specify a specific question ID.
    void generateQuestion(int questionID){
        currentStatus = Status.Waiting;
        audioManager.play("Next");

        //error handling
        if (questionID >= qdb.Length) {
            //Debug.LogWarning("Question ID is out of range! " + questionID.ToString() + "/" + qdb.Length.ToString());
			quizCompleted();			
            return;
        }

        //resetting things
        StopAllCoroutines();    //there should never be more than one running at a time, but just in case...
        brainPart3.redPart = null;  //clears the undo state
        sphere.SetActive(false);
        picker.gameObject.SetActive(false);
        box.gameObject.SetActive(false);
        //brain.putBackAll();
        flashBackground(Status.Waiting);
        

        //update the UI with the question data
        titleText.text = "Question #" + (wrongAnswerCount + correctAnswerCount + 1).ToString() + " of " + qdb.Length.ToString() + " (" + qdb[questionID].difficulty.ToString() + " difficulty) :";
        questionText.text = qdb[questionID].question;
        
        //launch the coroutine that will watch for a correct response
        if (qdb[questionID] is Quiz.Q_Simple)
            StartCoroutine("QSimpleHandler", questionID);
        else if (qdb[questionID] is Quiz.Q_Region)
            StartCoroutine("QRegionHandler", questionID);
        else if (qdb[questionID] is Quiz.Q_Box)
            StartCoroutine("QBoxHandler", questionID);
        else 
            Debug.LogWarning("Invalid question object!?");
    }

    //**HANDLERS**//
    IEnumerator QSimpleHandler(int currentQuestion){
        currentStatus = Status.Waiting;
        brain.putBackAll();

        Q_Simple currentQ = (Q_Simple)qdb[currentQuestion];
        List<string> wrongAnswers = new List<string>();     //keep track of wrong answers, to avoid duplicating mistake counts
        List<string> rightAnswers = new List<string>(currentQ.correctAnswers.Length);   
        rightAnswers.AddRange(currentQ.correctAnswers);     //convert it to a list because it's easier to scan than an array
        subtitleText.text = "Use the IDENTIFY tool to click on the correct part.";

        //check with brainPart3 for redpart to match. Loop ends right or wrong
        while (currentStatus == Status.Waiting) {
            yield return new WaitForEndOfFrame();
            if (brainPart3.redPart != null) {
                if (rightAnswers.Contains(brainPart3.redPart.name)) 
                    currentStatus = Status.Right;
                else if (!wrongAnswers.Contains(brainPart3.redPart.name))
                    wrongAnswers.Add(brainPart3.redPart.name);
            }

            if (wrongAnswers.Count == 2)
                subtitleText.text = "Last chance!";
            if (wrongAnswers.Count >= 3){
                currentStatus = Status.Wrong;
            }
        }

        //make the correct part(s) flash.
        foreach (string s in currentQ.correctAnswers) {
            try {
                brainPart3 bp = GameObject.Find(s).GetComponent<brainPart3>();
                bp.startFlashing();
            }
            catch{ }
        }

        //display the answer text        
        if (currentQ.answerOverride != "") {
            questionText.text += "\nAnswer: " + currentQ.answerOverride;
        } else if (currentStatus == Status.Right) {
            string tempText = brainPart3.redPart.name;
            if (tempText.EndsWith("CL") || tempText.EndsWith("CR"))
                tempText = tempText.Substring(0, tempText.Length - 3);
            questionText.text += "\nAnswer: " + tempText;
        } else {
            string tempText = currentQ.correctAnswers[0];
            if (tempText.EndsWith("CL") || tempText.EndsWith("CR"))
                tempText = tempText.Substring(0, tempText.Length - 3);
            questionText.text += "\nAnswer: " + tempText;
        }

        if (currentStatus == Status.Right)
            StartCoroutine("correctAnswer");
        else
            StartCoroutine("wrongAnswer");
    }

    IEnumerator QBoxHandler(int currentQuestion)
    {
		currentStatus = Status.Waiting;
        brain.putBackAll();

        box.gameObject.SetActive(true);
        Q_Box currentQ = (Q_Box)qdb[currentQuestion];
        List<string> answers = new List<string>(currentQ.correctAnswers);
        subtitleText.text = "Use the MOVE tool to bring " + answers.Count.ToString() + " parts into the box.";
        List<string> flashingParts = new List<string>();
        int correctCount;        

        //wait until everything in the box matches the correctAnswers. Order doesn't matter.
        while (currentStatus == Status.Waiting) {
            yield return new WaitForEndOfFrame();

            correctCount = 0;
            foreach (GameObject go in box.contents) {
                if (answers.Contains(go.name)) {
                    //box.StartCoroutine("correctAnswer");
                    correctCount++;
                    if (!flashingParts.Contains(go.name)) {
                        flashingParts.Add(go.name);
                        brainPart3 part = (brainPart3)go.GetComponent(typeof(brainPart3));
                        part.startFlashing();
                        subtitleText.text = correctCount.ToString() + " out of " + answers.Count.ToString() + " correct.";
                    }
                }
            }

            //Debug.Log(box.contents.Length.ToString() + ", "  + correctCount.ToString() + ", " + answers.Count.ToString());
            if (box.contents.Length == answers.Count && correctCount == answers.Count)
                currentStatus = Status.Right;
            //overloading the box triggers the 'wrong' status.
            else if (box.contents.Length + 1 > answers.Count) {
                currentStatus = Status.Wrong;
            }
        }

        questionText.text += "\nAnswers: " + string.Join(",", currentQ.correctAnswers);

        //make the correct parts flash.
        foreach (string s in currentQ.correctAnswers) {
            brainPart3 bp = GameObject.Find(s).GetComponent<brainPart3>();
            if (bp != null) bp.startFlashing();
        }

        box.gameObject.SetActive(false);
        if (currentStatus == Status.Right)
            StartCoroutine("correctAnswer");
        else
            StartCoroutine("wrongAnswer");
    }

    IEnumerator QRegionHandler(int currentQuestion) {
        //setup
        currentStatus = Status.Waiting;
        Q_Region currentQ = (Q_Region)qdb[currentQuestion];
        subtitleText.text = "Use the IDENTIFY tool place the marker on the specific point.";

        //picker
        picker.gameObject.SetActive(true);
        int clickCounter = 0;
        float clickDistance = 10f;

        //offset
        Vector3 targetOriginalPosition = Vector3.zero;
        Vector3 adjustedPoint = currentQ.hitPoint;
        Transform correctPart = GameObject.Find(currentQ.correctAnswer).transform;
        if (correctPart != null) {
            targetOriginalPosition = correctPart.GetComponent<brainPart3>().originalPosition();
            adjustedPoint = currentQ.hitPoint + (correctPart.position - targetOriginalPosition);
        }

        //sphere
        sphere.transform.localScale = Vector3.one * currentQ.distanceThreshold;
        sphere.transform.position = adjustedPoint;

        while (currentStatus == Status.Waiting) {
            yield return new WaitForEndOfFrame();          

            if (correctPart != null){
                adjustedPoint = currentQ.hitPoint + (correctPart.position - targetOriginalPosition);
                sphere.transform.position = adjustedPoint;
            }

            if (picker.scanCounter != clickCounter) {
                clickCounter = picker.scanCounter;                
                clickDistance = Vector3.Distance(picker.transform.position, adjustedPoint);
                //Debug.Log("Click Distance = " + clickDistance.ToString());

                if (clickDistance <= currentQ.distanceThreshold * 0.5f) { 
                    currentStatus = Status.Right;
                } else if (clickCounter >= 5) {
                    currentStatus = Status.Wrong;
                }
            }
        }

        if (currentQ.answerOverride != "") {
            questionText.text += "\nAnswer: " + currentQ.answerOverride;
        } else 
            questionText.text += "\nAnswer: " + currentQ.correctAnswer;

        //brainPart3 bp = correctPart.GetComponent<brainPart3>();
        //if (bp != null) bp.startFlashing();
        picker.gameObject.SetActive(false);
        sphere.SetActive(true);         //show the right answer position

        if (currentStatus == Status.Right)
            StartCoroutine("correctAnswer");
        else
            StartCoroutine("wrongAnswer");
    }

    //**RESPONSES**
    IEnumerator correctAnswer() {
        audioManager.play("Correct");
        flashBackground(Status.Right);
        yield return new WaitForEndOfFrame();		
		
        currentStatus = Status.Inactive;
        titleText.text = "Correct!";

        correctAnswerCount++;
        int percentage = Mathf.CeilToInt((correctAnswerCount / (float)(correctAnswerCount + wrongAnswerCount)) * 100f);
        subtitleText.text = "Current Score: " + correctAnswerCount.ToString() + " correct, " + wrongAnswerCount.ToString() + " incorrect. (" + percentage.ToString() + "%)";
        subtitleText.text += " Press NEXT to continue.";
    }

    IEnumerator wrongAnswer() {
        audioManager.play("Incorrect");
        flashBackground(Status.Wrong);
        yield return new WaitForEndOfFrame();		
		
        currentStatus = Status.Inactive;
        titleText.text = "Incorrect.";

        wrongAnswerCount++;
        int percentage = Mathf.CeilToInt((correctAnswerCount / (float)(correctAnswerCount + wrongAnswerCount)) * 100f);
        subtitleText.text = "Current Score: " + correctAnswerCount.ToString() + " correct, " + wrongAnswerCount.ToString() + " incorrect. (" + percentage.ToString() + "%)";
        subtitleText.text += " Press NEXT to continue.";
    }
	
	public void quizCompleted() {
        audioManager.play("Intro");

        sphere.SetActive(false);
        picker.gameObject.SetActive(false);
        brain.putBackAll();

        currentStatus = Status.Inactive;
        titleText.text = "Quiz Complete!";

        int percentage = Mathf.CeilToInt((correctAnswerCount / (float)(correctAnswerCount + wrongAnswerCount)) * 100f);
        questionText.text = "Final Score: " + correctAnswerCount.ToString() + " correct, " + wrongAnswerCount.ToString() + " incorrect. (" + percentage.ToString() + "%)";
        subtitleText.text = "";
		
		//reset score
		correctAnswerCount = 0;
		wrongAnswerCount = 0;
		qdb = null;		//this will trigger a reshuffle
    }

    private void flashBackground(Quiz.Status targetStatus){
        Color targetColor;
        if (targetStatus == Status.Right) {
            targetColor = new Color32(43, 119, 30, 152);    //green
            quizBackground.DOColor(targetColor, 0.3f).SetLoops(5, LoopType.Yoyo);
        }
        else if (targetStatus == Status.Wrong) {
            targetColor = new Color32(192, 11, 40, 152);    //reddish
            quizBackground.DOColor(targetColor, 0.3f).SetLoops(5, LoopType.Yoyo);
        }
        else  {
            targetColor = new Color32(38, 57, 152, 152);    //blue
            DOTween.Kill(quizBackground);
            quizBackground.color = targetColor;
        }
    }
}
