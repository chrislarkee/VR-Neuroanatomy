//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

namespace Quiz {
    public enum Difficulty {Easy, Normal, Clinical, Debug};
    public enum Status {Inactive, Waiting, Right, Wrong, Expired};

    //**QUESTION TYPES**//
    public class Q {
        //base parameters for all question types. Don't construct this directly, because there's no answers.
        public string question;       //the main question
        public string instruction;    //a clue or instruction    
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
        public string answerOverride;                   //override what the answer box says in response.
        public Vector3 hitPoint = Vector3.zero;     //the coordinate of the desired click, in world coordinates, with the part in its original position
        public float distanceThreshold = 1f;        //how far from the target is acceptable (the radius of the pink sphere)
    }

    //public class Q_MultipleChoice : Q {
    //    //a question with multiple choices, selected by a gui?
    //    public new string subtitle = "";
    //    public string[] choices;
    //    public int correctAnswer = 0;        
    //}

    //**CONSTRUCTOR METHODS**//
    public static class Questions {
        //single strings as parameters
        public static Q_Simple simple (string title, string correctAnswer){
            //if you don't specify a difficulty, it will be 'normal' by default
            string[] singleAnswer = new string[1] {correctAnswer};
            return simple(title, singleAnswer, "", Difficulty.Normal);
        }

        public static Q_Simple simple(string title, string correctAnswer, string alternativeAnswer) {
            //if you don't specify a difficulty, it will be 'normal' by default
            string[] singleAnswer = new string[1] {correctAnswer};
            return simple(title, singleAnswer, alternativeAnswer, Difficulty.Normal);
        }

        public static Q_Simple simple(string title, string correctAnswer, Difficulty difficulty) {
            //if you don't specify a difficulty, it will be 'normal' by default
            string[] singleAnswer = new string[1]{correctAnswer};
            return simple(title, singleAnswer, "", difficulty);
        }

        //string arrays as parameters
        public static Q_Simple simple(string title, string[] correctAnswers) {
            return simple(title, correctAnswers, "", Difficulty.Normal);
        }

        public static Q_Simple simple(string title, string[] correctAnswers, Difficulty difficulty) {
            return simple(title, correctAnswers, "", difficulty);
        }

        public static Q_Simple simple(string title, string[] correctAnswers, string answerOverride) {
            return simple(title, correctAnswers, answerOverride, Difficulty.Normal);
        }

        //the master one
        public static Q_Simple simple(string title, string[] correctAnswers, string answerOverride, Difficulty difficulty) {
            Q_Simple newQuestion = new Q_Simple();
            newQuestion.question = title;
            newQuestion.instruction = "Use the IDENTIFY tool to click on the correct part.";
            newQuestion.difficulty = difficulty;
            newQuestion.correctAnswers = correctAnswers;
            newQuestion.answerOverride = answerOverride;
            return newQuestion;
        }

        public static Q_Region region(string title, string correctAnswer, Vector3 hitPoint, float distanceThreshold) {
            //if you don't specify a difficulty, it will be 'normal' by default
            return region(title, correctAnswer, "", hitPoint, distanceThreshold, Difficulty.Normal);
        }

        public static Q_Region region(string title, string correctAnswer, string answerOverride, Vector3 hitPoint, float distanceThreshold)
        {
            //if you don't specify a difficulty, it will be 'normal' by default
            return region(title, correctAnswer, answerOverride, hitPoint, distanceThreshold, Difficulty.Normal);
        }

        public static Q_Region region(string title, string correctAnswer, Vector3 hitPoint, float distanceThreshold, Difficulty difficulty) {
            return region(title, correctAnswer, "", hitPoint, distanceThreshold, difficulty);
        }

        public static Q_Region region(string title, string correctAnswer, string answerOverride, Vector3 hitPoint, float distanceThreshold, Difficulty difficulty) {
            Q_Region newQuestion = new Q_Region();
            newQuestion.question = title;
            newQuestion.instruction = "Use the IDENTIFY tool place the marker on the specific point.";            
            newQuestion.correctAnswer = correctAnswer;
            newQuestion.answerOverride = answerOverride;
            newQuestion.hitPoint = hitPoint;
            newQuestion.distanceThreshold = distanceThreshold;
            newQuestion.difficulty = difficulty;
            return newQuestion;
        }

        public static Q_Box box(string title, string[] correctAnswers) {
            //if you don't specify a difficulty, it will be 'normal' by default
            return box(title, correctAnswers, Difficulty.Normal);
        }

        public static Q_Box box(string title, string[] correctAnswers, Difficulty difficulty) {
            Q_Box newQuestion = new Q_Box();
            newQuestion.question = title;
            newQuestion.instruction = "Use the MOVE tool to bring " + correctAnswers.Length.ToString() + " parts into the box.";
            newQuestion.difficulty = difficulty;
            newQuestion.correctAnswers = correctAnswers;
            return newQuestion;
        }
    }
}
