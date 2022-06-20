using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class QuestionManager : MonoBehaviour
{
    public TextAsset jsonFile;
    public TextMeshProUGUI questionTextBox;
    public TextMeshProUGUI fourAns0TextBox;
    public TextMeshProUGUI fourAns1TextBox;
    public TextMeshProUGUI fourAns2TextBox;
    public TextMeshProUGUI fourAns3TextBox;
    public TextMeshProUGUI imaFourAns0TextBox;
    public TextMeshProUGUI imaFourAns1TextBox;
    public TextMeshProUGUI imaFourAns2TextBox;
    public TextMeshProUGUI imaFourAns3TextBox;


    public TextMeshProUGUI threeAns0TextBox;
    public TextMeshProUGUI threeAns1TextBox;
    public TextMeshProUGUI threeAns2TextBox;
    public TextMeshProUGUI imaThreeAns0TextBox;
    public TextMeshProUGUI imaThreeAns1TextBox;
    public TextMeshProUGUI imaThreeAns2TextBox;

    public TextMeshProUGUI twoAns0TextBox;
    public TextMeshProUGUI twoAns1TextBox;
    public TextMeshProUGUI imaTwoAns0TextBox;
    public TextMeshProUGUI imaTwoAns1TextBox;

    public UIManager UIManager;

    public float baseScoreIncrement;

    private int currentQuestionIdx;
    private int currentDialogueIdx;
    private bool answer;
    private int randomQuestion;
    private int score;
    private int currentNumberOfAnswers;//number of answers a question has
    private int numCorrectAnswers;
    private int numWrongAnswers;

    [System.Serializable]
    public class Question
    {
        public string question;
        public bool numericQuestion;
        public bool hasImage;
        public int imageIdx;
        public string ans0;
        public string ans1;
        public string ans2;
        public string ans3;
        public int numericAnswer;
        public int correctIdx;
        public int timeLimit;
        public int points;
        public string hint;
        public string elaborateFeedback;
        public bool hasTimer;
    }

    [System.Serializable]
    public class Dialogue
    {
        public int speaker;
        public string speech;
    }

    [System.Serializable]
    public class QuestionList
    {
        public Dialogue[] dialogue;
        public Question[] questions;
    }

    [System.Serializable]
    public class QuestionPool
    {
        public QuestionList[] questionPool;
    }

    private QuestionPool questionPool = new QuestionPool();
    private QuestionList questionList;
    private Question question;

    private Dialogue[] dialogue;

    void Start()
    {
        currentDialogueIdx = 0;
        currentQuestionIdx = 0;
        numCorrectAnswers = 0;
        numWrongAnswers = 0;
        answer = true;
        questionPool = JsonUtility.FromJson<QuestionPool>(jsonFile.text);
        questionList = questionPool.questionPool[currentQuestionIdx];
        dialogue = questionList.dialogue;
    }

    public void SetDialogueText()
    {
        if (currentQuestionIdx > questionPool.questionPool.Length - 1)
        {
            GameManager.Instance.UpdateTime();
            GameManager.Instance.UpdateCorrectAnswers(numCorrectAnswers);
            GameManager.Instance.UpdateWrongAnswers(numWrongAnswers);
            UIManager.ExportData();
            GameManager.Instance.UpdateAttempts();
            
            UIManager.BackToMain();
        }
        else
        {
            if (currentDialogueIdx >= dialogue.Length)
            {
                UIManager.Continue();
            }
            else
            {
                questionTextBox.SetText(dialogue[currentDialogueIdx].speech);
                UIManager.SetSpeaker(dialogue[currentDialogueIdx].speaker);
                NextDialogue();                
            }
        }
    }

    public bool SetQuestionText(int retry)
    {
        UIManager.SetSpeaker(0);//set avatar to the professor
        questionList = questionPool.questionPool[currentQuestionIdx];
        dialogue = questionList.dialogue;
        randomQuestion = Random.Range(0, questionList.questions.Length);
        question = questionList.questions[randomQuestion];
        if (retry > 0)
        {
            questionTextBox.SetText(question.question + " Hint:" + question.hint);
        }
        else
        {
            questionTextBox.SetText(question.question);
        }

        if (!question.numericQuestion)
        {
            //get the number of answers the current multi-question has:
            int numberOfAnswers = 4;
            if (question.ans0 == "")
            {
                numberOfAnswers--;
            }
            if (question.ans1 == "")
            {
                numberOfAnswers--;
            }
            if (question.ans2 == "")
            {
                numberOfAnswers--;
            }
            if (question.ans3 == "")
            {
                numberOfAnswers--;
            }
            currentNumberOfAnswers = numberOfAnswers;
            UIManager.SetAnswerPanels();
            if (currentNumberOfAnswers == 4)
            {
                fourAns0TextBox.SetText(question.ans0);
                fourAns1TextBox.SetText(question.ans1);
                fourAns2TextBox.SetText(question.ans2);
                fourAns3TextBox.SetText(question.ans3);
                imaFourAns0TextBox.SetText(question.ans0);
                imaFourAns1TextBox.SetText(question.ans1);
                imaFourAns2TextBox.SetText(question.ans2);
                imaFourAns3TextBox.SetText(question.ans3);
            }
            else if (currentNumberOfAnswers == 3)
            {
                threeAns0TextBox.SetText(question.ans0);
                threeAns1TextBox.SetText(question.ans1);
                threeAns2TextBox.SetText(question.ans2);
                imaThreeAns0TextBox.SetText(question.ans0);
                imaThreeAns1TextBox.SetText(question.ans1);
                imaThreeAns2TextBox.SetText(question.ans2);
            }
            else //it will only support multi-question with 2, 3 or 4 answers
            {
                twoAns0TextBox.SetText(question.ans0);
                twoAns1TextBox.SetText(question.ans1);
                imaTwoAns0TextBox.SetText(question.ans0);
                imaTwoAns1TextBox.SetText(question.ans1);
            }
        }
        return question.numericQuestion;
    }

    public void AnswerQuestion(int answerIdx)
    {
        questionList = questionPool.questionPool[currentQuestionIdx];
        question = questionList.questions[randomQuestion];
        score = 0;
        if (question.correctIdx == answerIdx)
        {
            numCorrectAnswers++;
            score = (int)baseScoreIncrement;
            if (question.hasTimer)
            {
                score += (int)(baseScoreIncrement * UIManager.Timer.timeRatio());
                UIManager.Timer.EndTimer();
            }
            answer = true;
            UIManager.SetFeedbacking(true);
        }
        else
        {
            numWrongAnswers++;
            answer = false;
        }

        UIManager.CallContinue(answer, score);
        if (currentQuestionIdx == questionPool.questionPool.Length - 1)
        {
            UIManager.UpdateScore();
        }
    }

    public void AnswerNumericQuestion(int numAnswer)
    {
        questionList = questionPool.questionPool[currentQuestionIdx];
        question = questionList.questions[randomQuestion];
        score = 0;
        if (question.numericAnswer == numAnswer)
        {
            score = (int)baseScoreIncrement;
            if (question.hasTimer)
            {
                score += (int)(baseScoreIncrement * UIManager.Timer.timeRatio());
                UIManager.Timer.EndTimer();
            }
            answer = true;
            UIManager.SetFeedbacking(true);
            //NextQuestion();
        }
        else
        {
            answer = false;
        }

        UIManager.CallContinue(answer, score);
        if (currentQuestionIdx == questionPool.questionPool.Length - 1)
        {
            UIManager.UpdateScore();           
        }
    }

    public int NumberOfAnswers()//How many answers a question has
    {
        return currentNumberOfAnswers;
    }

    public bool ContainImage()//the method returns if a question contains an image
    {
        return question.hasImage;//needs to be upadted 
    }

    public int ImageInQuestion()//return the index of image in the question
    {
        return question.imageIdx;//needs to be updated
    }


    public void NextQuestion()
    {
        currentQuestionIdx++;
        currentDialogueIdx = 0;
        if (currentQuestionIdx < questionPool.questionPool.Length)
        {
            questionList = questionPool.questionPool[currentQuestionIdx];
            dialogue = questionList.dialogue;
        }

    }

    public void NextDialogue()
    {
        currentDialogueIdx++;
    }

    public string GetElaborateFeedback()
    {
        questionList = questionPool.questionPool[currentQuestionIdx];
        question = questionList.questions[randomQuestion];
        return question.elaborateFeedback;
    }

    public bool HasTimer()
    {
        questionList = questionPool.questionPool[currentQuestionIdx];
        question = questionList.questions[randomQuestion];
        return question.hasTimer;
    }

}
