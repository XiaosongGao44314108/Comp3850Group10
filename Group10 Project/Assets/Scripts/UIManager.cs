using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    public GameObject MenuPanel;
    public GameObject GoalPanel;
    public GameObject LevelPanel;
    public GameObject AnswerPanel;
    public GameObject DialoguePanel;
    public GameObject ReviewOrNotPanel;
    public GameObject ReviewPanel;
    public GameObject FeedbackPanel;
    public GameObject Dialogue;  //dialogue text
    public GameObject ElaborateFeedbackPanel;
    public GameObject CorrectReviewPanel;

    public QuestionManager QManager;
    public Button[] answers;
    public Button feedbackYes;
    public Button feedbackNo;
    public Button finishReviewButton;
    public Button goodButton;
    public Button badButton;
    public Button continueButton;
    public Button acceptReviewButton;
    public Button refuseReviewButton;
    public TextMeshProUGUI elaborateFeedback;

    public string DialogueText;
    public string QuestionText;
    private bool feedbacking; //stop calling next question if providing feedback
    private bool retry;
    private bool IsDiaActive;
    private bool IsMainActive;//diaglogue should not be activated if Main menu panel is activated
    private bool SelectedGoalOne;
    private bool SelectedGoalTwo;
    private bool SelectedGoalThree;



    void Start()
    {
        IsDiaActive = false; //dialog is not activated at the beginning
        IsMainActive = false;
        SelectedGoalOne = false;
        SelectedGoalTwo = false;
        SelectedGoalThree = false;

        feedbacking = false;
        //Just for this version:
        DialogueText = "People talk";
        QuestionText = "Question contents";
        //answered = false;
        SetPanels();
        SetDialogue();
        for (int i = 0; i < answers.Length; i++)
        {
            int closureIndex = i; //prevents closure problem
            answers[closureIndex].onClick.AddListener(() => TaskOnClick(closureIndex));
        }
        feedbackYes.onClick.AddListener(() => FeedbackYes());
        feedbackNo.onClick.AddListener(() => FeedbackNo());
        finishReviewButton.onClick.AddListener(() => CallSimilarQuestion());
        goodButton.onClick.AddListener(() => Continue());
        badButton.onClick.AddListener(() => Continue());
        continueButton.onClick.AddListener(() => ContinueAfterFeedback());
        acceptReviewButton.onClick.AddListener(() => CallReview());
        refuseReviewButton.onClick.AddListener(() => Continue());

    }

    void Update()
    {
        if (Dialogue != null)
        {
            TurnPages();
        }
    }


    public void SetPanels()
    {
        if (MenuPanel != null)
        {
            MenuPanel.gameObject.SetActive(true);
            IsMainActive = true;
        }
        if (GoalPanel != null)
        {
            GoalPanel.gameObject.SetActive(false);
        }
        if (LevelPanel != null)
        {
            LevelPanel.gameObject.SetActive(false);
        }
        if (AnswerPanel != null)
        {
            AnswerPanel.gameObject.SetActive(false);
        }
        if (DialoguePanel != null)
        {
            DialoguePanel.gameObject.SetActive(false);
        }
        if (ReviewOrNotPanel != null)
        {
            ReviewOrNotPanel.gameObject.SetActive(false);
        }
        if (ReviewPanel != null)
        {
            ReviewPanel.gameObject.SetActive(false);
        }
        if (FeedbackPanel != null)
        {
            FeedbackPanel.gameObject.SetActive(false);
        }
        if (ElaborateFeedbackPanel != null)
        {
            ElaborateFeedbackPanel.SetActive(false);
        }
        if (CorrectReviewPanel != null)
        {
            CorrectReviewPanel.SetActive(false);
        }


    }

    public void SetDialogue()
    {
        if (DialoguePanel != null)
        {
            Dialogue.GetComponent<TMPro.TextMeshProUGUI>().text = DialogueText;
        }

    }

    public void TurnPages()
    {
        if (Input.GetButton("TurnPages") || Input.GetMouseButtonDown(0) && feedbacking == false)
        {
            if (IsDiaActive == false)//if the dia is not activated yet
            {
                if (!IsMainActive)//if the main menu is not activated
                {
                    IsDiaActive = true;
                    DialoguePanel.gameObject.SetActive(true);
                }
                //if the dia is not activated yet but the main menu is activated, do nothing
            }
            else //the dia is already activated
            {
                retry = false;
                //what happen after pressing space
                //Dialogue.GetComponent<TMPro.TextMeshProUGUI>().text = QuestionText;
                AnswerPanel.SetActive(true);
                QManager.SetQuestionText(retry);
            }
        }
    }
    // public void NextScene()
    // {
    //     MenuPanel.gameObject.SetActive(false);

    //     Scene scene = SceneManager.GetActiveScene();
    //     int lastScene = SceneManager.sceneCountInBuildSettings - 1;
    //     int next;
    //     if (scene.buildIndex == lastScene)
    //     {
    //         next = 0;
    //     }
    //     else
    //     {
    //         next = scene.buildIndex + 1;
    //     }
    //     SceneManager.LoadScene(next);
    // }
    public void ChoosingGoal() // what happens after clicking play
    {
        MenuPanel.gameObject.SetActive(false);
        GoalPanel.gameObject.SetActive(true);
        IsMainActive = false;
        DialoguePanel.gameObject.SetActive(true);
        Dialogue.GetComponent<TMPro.TextMeshProUGUI>().text = "Choose one goal";
    }
    public void ChoosingGoalOneLevel() // What happens after clicking GoalOne
    {
        SelectedGoalOne = true;
        GoalPanel.gameObject.SetActive(false);
        LevelPanel.gameObject.SetActive(true);
        Dialogue.GetComponent<TMPro.TextMeshProUGUI>().text = "Choose one level";
    }
    public void ChoosingGoalTwoLevel() // What happens after clicking GoalTwo
    {
        SelectedGoalTwo = true;
        GoalPanel.gameObject.SetActive(false);
        LevelPanel.gameObject.SetActive(true);
        Dialogue.GetComponent<TMPro.TextMeshProUGUI>().text = "Choose one level";
    }
    public void ChoosingGoalThreeLevel() // What happens after clicking GoalThree
    {
        SelectedGoalThree = true;
        GoalPanel.gameObject.SetActive(false);
        LevelPanel.gameObject.SetActive(true);
        Dialogue.GetComponent<TMPro.TextMeshProUGUI>().text = "Choose one level";
    }

    public void LoadingLevelOne() //What happens after clicking Level One
    {
        if (SelectedGoalOne)
        {
            SceneManager.LoadScene(1);
        }
        if (SelectedGoalTwo)
        {
            SceneManager.LoadScene(4);
        }
        if (SelectedGoalThree)
        {
            SceneManager.LoadScene(7);
        }
    }

    public void LoadingLevelTwo() //What happens after clicking Level Two
    {
        if (SelectedGoalOne)
        {
            SceneManager.LoadScene(2);
        }
        if (SelectedGoalTwo)
        {
            SceneManager.LoadScene(5);
        }
        if (SelectedGoalThree)
        {
            SceneManager.LoadScene(8);
        }
    }

    public void LoadingLevelThree() //What happens after clicking Level Three
    {
        if (SelectedGoalOne)
        {
            SceneManager.LoadScene(3);
        }
        if (SelectedGoalTwo)
        {
            SceneManager.LoadScene(6);
        }
        if (SelectedGoalThree)
        {
            SceneManager.LoadScene(9);
        }
    }

    public void BackToMain()
    {
        SceneManager.LoadScene(0);
    }


    public void CallContinue(bool answer)
    {
        DialoguePanel.SetActive(true);
        AnswerPanel.SetActive(false);
        if (answer && !retry)
        {
            ReviewQuestion();
            Dialogue.GetComponent<TMPro.TextMeshProUGUI>().text = "Well Done!!!";
        }
        else if (answer && retry)
        {
            Dialogue.GetComponent<TMPro.TextMeshProUGUI>().text = "Well Done!!!";
            GetFeedback();
        }
        else if (!answer && retry)
        {
            ElaborateFeedback();
        }
        else
        {
            //player answers incorrectly.
            retry = true;
            ReviewOrNot();
        }
    }

    public void ElaborateFeedback()
    {
        ElaborateFeedbackPanel.SetActive(true);
        elaborateFeedback.GetComponent<TMPro.TextMeshProUGUI>().text = QManager.GetElaborateFeedback();
    }

    public void TaskOnClick(int idx)
    {
        QManager.AnswerQuestion(idx);
    }

    public void ReviewOrNot()
    {
        feedbacking = true;
        //let player chooses whether to review videos/images about incorrect answer
        //Active ReviewOrNotPanel after answer incorrectly
        Dialogue.GetComponent<TMPro.TextMeshProUGUI>().text = "Not Quite, do you want to know more about why your answer is incorrectly?";
        ReviewOrNotPanel.gameObject.SetActive(true);
    }

    public void CallReview()
    {
        //Player choose to review videos/images about incorrect answer
        //Disactive ReviewOrNot Panel
        //Active Review Panel
        //Clear contents in dialogue box
        AnswerPanel.gameObject.SetActive(false);
        CorrectReviewPanel.gameObject.SetActive(false);
        Dialogue.GetComponent<TMPro.TextMeshProUGUI>().text = "";
        ReviewOrNotPanel.gameObject.SetActive(false);
        ReviewPanel.gameObject.SetActive(true);

    }

    public void CallSimilarQuestion()
    {
        ReviewPanel.gameObject.SetActive(false);
        //display different question of similar type at her
        //do not know how to implement yet
        //need to add more contents at here later

        //ReviewOrNot Panel may not be actived yet if player chooses not to review related images
        ReviewPanel.gameObject.SetActive(false);
        ReviewOrNotPanel.gameObject.SetActive(false);
        //setup question
        DialoguePanel.SetActive(true);
        AnswerPanel.SetActive(true);
        QManager.SetQuestionText(retry);
        //GetFeedback();
    }

    public void GetFeedback()
    {
        //Get feedback from the player
        FeedbackPanel.gameObject.SetActive(true);
        Dialogue.GetComponent<TMPro.TextMeshProUGUI>().text = "How do you feel about this question?";
    }

    public void FeedbackYes()
    {
        CallReview();
    }

    public void FeedbackNo()
    {
        DialoguePanel.SetActive(true);
        AnswerPanel.SetActive(true);
        QManager.SetQuestionText(retry);
    }

    public void CallNextQuestion()
    {
        FeedbackPanel.gameObject.SetActive(false);
        feedbacking = false;
    }


    public void ExitGame()
    {
        //quit editting mode in unity, also quit game
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    //temporary method
    public void Continue()
    {
        retry = false;
        CorrectReviewPanel.SetActive(false);
        //what happen after pressing space
        //Dialogue.GetComponent<TMPro.TextMeshProUGUI>().text = QuestionText;
        AnswerPanel.SetActive(true);
        QManager.SetQuestionText(retry);
    }

    public void ContinueAfterFeedback()
    {
        QManager.NextQuestion();
        ElaborateFeedbackPanel.SetActive(false);
        retry = false;
        //what happen after pressing space
        //Dialogue.GetComponent<TMPro.TextMeshProUGUI>().text = QuestionText;
        GetFeedback();
    }

    //ask player if they want to review question
    public void ReviewQuestion()
    {
        CorrectReviewPanel.SetActive(true);
        feedbacking = true;
    }

    //review the question
    public void QuestionReview()
    {

    }
}
