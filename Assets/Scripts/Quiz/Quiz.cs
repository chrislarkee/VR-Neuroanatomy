using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Quiz {
    public enum Difficulty {Easy, Normal, Clinical, Debug};
    public enum Status {Inactive, Waiting, Right, Wrong, Expired};

    //**QUESTION TYPES**//
    public class Q {
        //base parameters for all question types. Don't construct this directly, because there's no answers.
        public int ID;                //simple counter. Only used for shuffle debugging.
        public string question;       //the main question
        public Difficulty difficulty;
        
    }

    //unique structures for different types of questions. These EXTEND the base Q class, because they have different answer formats
    public class Q_Simple : Q {
        //a question with one answer. Identify the answer.
        public string[] correctAnswers;
        public string answerOverride;   //an optional, secondary answer. This can contain multiple items.
    }
    public class Q_Box : Q
    {
        //a question with multiple items. drag items into the magic box.
        public string[] correctAnswers;
    }

    public class Q_Region : Q {
        //Point a specific region on a brain part.
        public string correctAnswer;                //should match a part in the heirarchy
        public string answerOverride;               //override what the answer box says in response.
        public Vector3 hitPoint;                    //the coordinate of the desired click, in world coordinates, with the part in its original position
        public float distanceThreshold;             //how far from the target is acceptable (the radius of the pink sphere)
    }

    //We didn't use this one, but it might be a good idea someday
    //public class Q_MultipleChoice : Q {
    //    //a question with multiple choices, selected by a gui?
    //    public new string subtitle = "";
    //    public string[] choices;
    //    public int correctAnswer = 0;        
    //}

    //**LOADERS**//
    public static class Load {
        public static Q[] LoadAll(string filename){
            bool[] diffs = new bool[]{true, true, true};
            return LoadAll(filename, diffs);
        }

        public static Q[] LoadAll(string filename, bool[] difficulties) {
            //disallow an empty database
            if (difficulties[0] == false && difficulties[1] == false && difficulties[2] == false)
                difficulties = new bool[] {true, true, true};

            //load the CSV file from Resources
            string[] data = Resources.Load<TextAsset>(filename).text.Split('\n');
            List<Q> allQuestions = new List<Q>();
            int idCounter = -1;

            //each entry in data[] contains one line. Loop through every line, except the header
            for (int i = 1; i < data.Length - 1; i++) {
                string[] line = data[i].Split('\t');     //split line into cells, tab-delimited

                //if the question has an inactive difficulty setting, skip it.
                Difficulty diffCheck = Utilities.parseDifficulty(line[1]);
                if (difficulties[(int)diffCheck] == false) continue;
                
                idCounter++;

                //0=Type, 1=Difficulty, 2=Question, 3=Answers, 4=Override, 5=hitPoint, 6=distanceThreshold
                if (line[0].ToUpper().StartsWith("S")) {
                    //simple-specific parameters
                    Quiz.Q_Simple newQuestion = new Quiz.Q_Simple();
                    newQuestion.ID = idCounter;
                    newQuestion.difficulty = diffCheck;
                    newQuestion.question = Utilities.validate(line[2]);
                    newQuestion.correctAnswers = Utilities.separateAnswers(line[3]);
                    newQuestion.answerOverride = line[4];
                    allQuestions.Add(newQuestion);

                } else if (line[0].ToUpper().StartsWith("B")) {
                    //box-specific parameters
                    Quiz.Q_Box newQuestion = new Quiz.Q_Box();
                    newQuestion.ID = idCounter;
                    newQuestion.difficulty = diffCheck;
                    newQuestion.question = Utilities.validate(line[2]);
                    newQuestion.correctAnswers = Utilities.separateAnswers(line[3]);
                    allQuestions.Add(newQuestion);
                   
                } else if (line[0].ToUpper().StartsWith("R")) {
                    //region-specific parameters
                    Quiz.Q_Region newQuestion = new Quiz.Q_Region();
                    newQuestion.ID = idCounter;
                    newQuestion.difficulty = diffCheck;
                    newQuestion.question = Utilities.validate(line[2]);
                    newQuestion.correctAnswer = Utilities.validate(line[3]);
                    newQuestion.answerOverride = line[4];
                    newQuestion.hitPoint = Utilities.parseVec3(line[5]);
                    newQuestion.distanceThreshold = float.Parse(line[6]);
                    allQuestions.Add(newQuestion);
                } else {
                    //improper lines, including the header perhaps
                    continue;
                }
            }

            //Debug.Log("Total: " + allQuestions.Count.ToString());
            return allQuestions.ToArray();
        }
    }

    public static class Utilities {
        public static Vector3 parseVec3(string input) {
            //convert the string from the text file into a valid vector3
            if (input.StartsWith("\"")) input = input.Substring(1, input.Length - 2);
            string[] separated = input.Split(',');
            Vector3 output = new Vector3(float.Parse(separated[0]), float.Parse(separated[1]), float.Parse(separated[2]));
            return output;
        }

        public static Quiz.Difficulty parseDifficulty(string input) {
            //convert the string from the text file into the difficulty Enum
            if (input.ToUpper().StartsWith("E")) 
                return Difficulty.Easy;
            else if (input.ToUpper().StartsWith("N")) 
                return Quiz.Difficulty.Normal;
            else if (input.ToUpper().StartsWith("C")) 
                return Quiz.Difficulty.Clinical;
            else if (input.ToUpper().StartsWith("D")) 
                return Quiz.Difficulty.Debug;
            else 
                return Difficulty.Normal;
        }

        public static string[] separateAnswers(string input) {
            //trim quotes
            if (input.StartsWith("\"")) input = input.Substring(1, input.Length - 2);

            string[] separated = input.Split(',');
            return separated;
        }

        public static string validate(string input){
            //no quotes!    
            if (input.StartsWith("\"")) 
                input = input.Substring(1, input.Length - 2);

            //no trailing spaces!
            if (input.EndsWith(" "))
                input = input.Substring(0, input.Length - 2);
        
            return input;
        }
    }
}
