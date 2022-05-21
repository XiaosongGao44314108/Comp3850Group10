using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class QuestionManager : MonoBehaviour
{
    public TextAsset jsonFile;
    public TextMeshProUGUI questionTextBox;
    public TextMeshProUGUI ans0TextBox;
    public TextMeshProUGUI ans1TextBox;
    public TextMeshProUGUI ans2TextBox;
    public TextMeshProUGUI ans3TextBox;
    public UIManager UIManager;

    public float baseScoreIncrement;
    private int currentQuestionIdx;
    private int currentDialogueIdx;
    private bool answer;
    private int randomQuestion;
    private int score;

    [System.Serializable]
    public class Question
	{
        public string question;
        public bool numericQuestion;
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
        answer = true;
        questionPool = JsonUtility.FromJson<QuestionPool>(jsonFile.text);
        questionList = questionPool.questionPool[currentQuestionIdx];
        dialogue = questionList.dialogue;
    }

    public void SetDialogueText()
    {
        if(currentQuestionIdx >= questionPool.questionPool.Length){
            UIManager.BackToMain();
        }else{
            if(currentDialogueIdx >= dialogue.Length){
                UIManager.Continue();
            }else{
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
        randomQuestion = (int)Random.Range(0,questionList.questions.Length-1);
        question = questionList.questions[randomQuestion];
        if(retry > 0){
            questionTextBox.SetText(question.question+" Hint:"+question.hint);
        }else{
            questionTextBox.SetText(question.question);
        }

		if (!question.numericQuestion)
        {
            ans0TextBox.SetText(question.ans0);
            ans1TextBox.SetText(question.ans1);
            ans2TextBox.SetText(question.ans2);
            ans3TextBox.SetText(question.ans3);
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
            score = (int)baseScoreIncrement;
            if(question.hasTimer)
            {
                score += (int)(baseScoreIncrement*UIManager.Timer.timeRatio());
            }
            answer = true;
            if(currentQuestionIdx == questionPool.questionPool.Length-1)
            {
            UIManager.UpdateScore(score);
            //UIManager.BackToMain();
            }
            UIManager.SetFeedbacking(true);
            //NextQuestion();
        }else{
            answer = false;
        }        
        
        UIManager.CallContinue(answer, score);
    }

    public void AnswerNumericQuestion(int numAnswer)
    {
        questionList = questionPool.questionPool[currentQuestionIdx];
        question = questionList.questions[randomQuestion];
        score = 0;
        print("ans:" + question.numericAnswer + "   given ans: " + numAnswer);
        if (question.numericAnswer == numAnswer)
        {
            score = (int)baseScoreIncrement;
            if (question.hasTimer)
            {
                score += (int)(baseScoreIncrement * UIManager.Timer.timeRatio());
            }
            answer = true;
            if (currentQuestionIdx == questionPool.questionPool.Length - 1)
            {
                UIManager.UpdateScore(score);
                //UIManager.BackToMain();
            }
            UIManager.SetFeedbacking(true);
            //NextQuestion();
        }
        else
        {
            answer = false;
        }

        UIManager.CallContinue(answer, score);
    }


    public void NextQuestion()
	{
        currentQuestionIdx++;
        currentDialogueIdx = 0;
        if(currentQuestionIdx<questionPool.questionPool.Length)
        {
            questionList = questionPool.questionPool[currentQuestionIdx];
            dialogue = questionList.dialogue;  
        }
        
    }

    public void NextDialogue()
    {
        currentDialogueIdx++;
        //SetDialogueText();
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
