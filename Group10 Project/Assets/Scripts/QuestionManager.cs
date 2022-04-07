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

    private int currentQuestionIdx = 0;
    private bool answer = true;

    [System.Serializable]
    public class Question
	{
        public string question;
        public string ans0;
        public string ans1;
        public string ans2;
        public string ans3;
        public int correctIdx;
        public int timeLimit;
        public int points;
        public string hint;
    }

    [System.Serializable]
    public class QuestionList
	{
        public Question[] questions;
	}

    public QuestionList questionList = new QuestionList();

    void Start()
    {
        questionList = JsonUtility.FromJson<QuestionList>(jsonFile.text);
    }

	public void SetQuestionText(bool hint)
	{
        Question question = questionList.questions[currentQuestionIdx];
        if(hint){
            questionTextBox.SetText(question.question+" Hint:"+question.hint);
        }else{
            questionTextBox.SetText(question.question);
        }
        ans0TextBox.SetText(question.ans0);
        ans1TextBox.SetText(question.ans1);
        ans2TextBox.SetText(question.ans2);
        ans3TextBox.SetText(question.ans3);
        
    }

    public void AnswerQuestion(int answerIdx)
    {
        Question question = questionList.questions[currentQuestionIdx];

        if (question.correctIdx == answerIdx)
        {
            //points += question.points
            answer = true;
            NextQuestion();
        }else{
            answer = false;
        }        
        
        UIManager.CallContinue(answer);
    }
    

    private void NextQuestion()
	{
        currentQuestionIdx++;
        SetQuestionText(false);
    }

}
