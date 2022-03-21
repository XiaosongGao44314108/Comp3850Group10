using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionManager : MonoBehaviour
{
    public TextAsset jsonFile;
    public Text questionTextBox;
    public Text ans0TextBox;
    public Text ans1TextBox;
    public Text ans2TextBox;
    public Text ans3TextBox;

    private int currentQuestionIdx = 0;

    public class Question
	{
        public string questionText;
        public string ans0;
        public string ans1;
        public string ans2;
        public string ans3;
        public int correctIdx;
        public int timeLimit;
        public int points;
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

	public void SetQuestionText()
	{
        Question question = questionList.questions[currentQuestionIdx];
        questionTextBox.text = question.questionText;
        ans0TextBox.text = question.ans0;
        ans1TextBox.text = question.ans1;
        ans2TextBox.text = question.ans2;
        ans3TextBox.text = question.ans3;
    }

    public void AnswerQuestion(int answerIdx)
    {
        Question question = questionList.questions[currentQuestionIdx];

        if (question.correctIdx == answerIdx)
        {
            //points += question.points
            NextQuestion();
        }
    }
    

    private void NextQuestion()
	{
        currentQuestionIdx++;
        SetQuestionText();
    }

}
