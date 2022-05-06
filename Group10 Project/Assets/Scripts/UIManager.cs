using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Runtime.InteropServices;

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
    public Button returnButton;
    public Texture [] avatars;
    public RawImage speakerAvatar;
    

    private GameManager GManager;
    public QuestionManager QManager;
    public TextMeshProUGUI elaborateFeedback;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI Lvl1HighscoreText;
    public TextMeshProUGUI Lvl2HighscoreText;
    public TextMeshProUGUI Lvl3HighscoreText;
    public TextMeshProUGUI GoalLvlText;
    public GameObject timerGO;

    private string DialogueText;
    private string QuestionText;
    private bool feedbacking; //stop calling next question if providing feedback
    private int retry;
    private bool answering;
    private Timer timer;
    public Timer Timer
    {
        get
        {
            return timer;
        }
    }
    private Slider timerSlider;
    private bool IsDiaActive;
    private bool IsMainActive;//diaglogue should not be activated if Main menu panel is activated
    private bool SelectedGoalOne;
    private bool SelectedGoalTwo;
    private bool SelectedGoalThree;
    private int currentScore;




    void Start()
    {
        GManager = (GameManager)FindObjectOfType(typeof(GameManager));
        IsDiaActive = false; //dialog is not activated at the beginning
        IsMainActive = false;
        SelectedGoalOne = false;
        SelectedGoalTwo = false;
        SelectedGoalThree = false;
        feedbacking = false;

        currentScore = 0;
        //Just for this version:
        DialogueText = "People talk";
        QuestionText = "Question contents";
        SetPanels();
        if(Dialogue != null){
            DialogueContinue();
        }
        //SetDialogue();
    }

    void Update()
    {
        if (Dialogue != null && !feedbacking)
        {
            TurnPages();
        }
        if (timerGO != null)
        {
            if (!timer.IsActive())
            {
                timerGO.SetActive(false);
            }
            else
            {
                timerSlider.value = timer.TimeLeft() / timer.timeLimit;
            }
        }
        if (scoreText != null)
        {
            scoreText.GetComponent<TMPro.TextMeshProUGUI>().text = "Score: " + currentScore;
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
        if (returnButton != null)
        {
            returnButton.gameObject.SetActive(false);
        }
        if (timerGO != null)
        {
            timerGO.SetActive(false);
            timer = timerGO.GetComponent<Timer>();
            timerSlider = timerGO.GetComponent<Slider>();
        }


    }

    // public void SetDialogue()
    // {
    //     if (DialoguePanel != null)
    //     {
    //         QManager.SetDialogueText();
    //     }

    // }

    public void TurnPages()
    {
        if (Input.GetButtonUp("TurnPages") || Input.GetMouseButtonUp(0) && feedbacking == false)
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
                DialogueContinue();
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
        //DialoguePanel.gameObject.SetActive(true);
        //Dialogue.GetComponent<TMPro.TextMeshProUGUI>().text = "Choose one goal";
    }
    public void ChoosingGoalOneLevel() // What happens after clicking GoalOne
    {
        SelectedGoalOne = true;
        GoalPanel.gameObject.SetActive(false);
        returnButton.gameObject.SetActive(true);
        LevelPanel.gameObject.SetActive(true);
        //Dialogue.GetComponent<TMPro.TextMeshProUGUI>().text = "Choose one level";
        GoalLvlText.GetComponent<TMPro.TextMeshProUGUI>().text = "Goal: No Poverty";
        Lvl1HighscoreText.GetComponent<TMPro.TextMeshProUGUI>().text = GManager.GetScore(0) + "";
        Lvl2HighscoreText.GetComponent<TMPro.TextMeshProUGUI>().text = GManager.GetScore(1) + "";
        Lvl3HighscoreText.GetComponent<TMPro.TextMeshProUGUI>().text = GManager.GetScore(2) + "";
    }
    public void ChoosingGoalTwoLevel() // What happens after clicking GoalTwo
    {
        SelectedGoalTwo = true;
        GoalPanel.gameObject.SetActive(false);
        returnButton.gameObject.SetActive(true);
        LevelPanel.gameObject.SetActive(true);
        //Dialogue.GetComponent<TMPro.TextMeshProUGUI>().text = "Choose one level";
        GoalLvlText.GetComponent<TMPro.TextMeshProUGUI>().text = "Goal: Quality Education";
        Lvl1HighscoreText.GetComponent<TMPro.TextMeshProUGUI>().text = GManager.GetScore(3) + "";
        Lvl2HighscoreText.GetComponent<TMPro.TextMeshProUGUI>().text = GManager.GetScore(4) + "";
        Lvl3HighscoreText.GetComponent<TMPro.TextMeshProUGUI>().text = GManager.GetScore(5) + "";
    }
    public void ChoosingGoalThreeLevel() // What happens after clicking GoalThree
    {
        SelectedGoalThree = true;
        GoalPanel.gameObject.SetActive(false);
        returnButton.gameObject.SetActive(true);
        LevelPanel.gameObject.SetActive(true);
        //Dialogue.GetComponent<TMPro.TextMeshProUGUI>().text = "Choose one level";
        GoalLvlText.GetComponent<TMPro.TextMeshProUGUI>().text = "Goal: Good Health and Well-Being";
        Lvl1HighscoreText.GetComponent<TMPro.TextMeshProUGUI>().text = GManager.GetScore(6) + "";
        Lvl1HighscoreText.GetComponent<TMPro.TextMeshProUGUI>().text = GManager.GetScore(7) + "";
        Lvl1HighscoreText.GetComponent<TMPro.TextMeshProUGUI>().text = GManager.GetScore(8) + "";
    }

    public void LoadingLevelOne() //What happens after clicking Level One
    {
        if (SelectedGoalOne)
        {
            SceneManager.LoadScene(1);
        }
        if (SelectedGoalTwo)
        {
            SceneManager.LoadScene(1);
        }
        if (SelectedGoalThree)
        {
            SceneManager.LoadScene(1);
        }

        IsMainActive = false;
    }

    public void LoadingLevelTwo() //What happens after clicking Level Two
    {
        if (SelectedGoalOne)
        {
            SceneManager.LoadScene(1);
        }
        if (SelectedGoalTwo)
        {
            SceneManager.LoadScene(1);
        }
        if (SelectedGoalThree)
        {
            SceneManager.LoadScene(1);
        }

        IsMainActive = false;
    }

    public void LoadingLevelThree() //What happens after clicking Level Three
    {
        if (SelectedGoalOne)
        {
            SceneManager.LoadScene(1);
        }
        if (SelectedGoalTwo)
        {
            SceneManager.LoadScene(1);
        }
        if (SelectedGoalThree)
        {
            SceneManager.LoadScene(1);
        }

        IsMainActive = false;
    }

    public void ReturnToGoal() //click button and return to goal panel while selecting level
    {
        SelectedGoalOne = false;
        SelectedGoalTwo = false;
        SelectedGoalThree = false;
        GoalPanel.gameObject.SetActive(true);
        returnButton.gameObject.SetActive(false);
        LevelPanel.gameObject.SetActive(false);
    }

    public void BackToMain()
    {
        SceneManager.LoadScene(0);
    }


    public void CallContinue(bool answer, int score)
    {
        DialoguePanel.SetActive(true);
        AnswerPanel.SetActive(false);
        currentScore += score;
        scoreText.GetComponent<TMPro.TextMeshProUGUI>().text = "Score: " + currentScore;
        feedbacking = true;
        Debug.Log(retry);
        if (answer && retry == 0)
        {
            ReviewQuestion();
            Dialogue.GetComponent<TMPro.TextMeshProUGUI>().text = "Well Done!!!";
        }
        else if (answer && retry <3)
        {
            Dialogue.GetComponent<TMPro.TextMeshProUGUI>().text = "Well Done!!!";
            GetFeedback();
        }
        else if (!answer && retry==2)
        {
            retry = 0;
            ElaborateFeedback();
        }
        else
        {
            //player answers incorrectly.
            retry ++;
            ReviewOrNot();
        }
    }

    public void ElaborateFeedback()
    {
        CorrectReviewPanel.SetActive(false);
        feedbacking = true;
        ElaborateFeedbackPanel.SetActive(true);
        elaborateFeedback.GetComponent<TMPro.TextMeshProUGUI>().text = QManager.GetElaborateFeedback();
    }

    public void TaskOnClick(int idx)
    {
        answering = false;
        QManager.AnswerQuestion(idx);
        timer.EndTimer();

    }

    public void ReviewOrNot()
    {
        feedbacking = true;
        //let player chooses whether to review videos/images about incorrect answer
        //Active ReviewOrNotPanel after answer incorrectly
        Dialogue.GetComponent<TMPro.TextMeshProUGUI>().text = "Not Quite, do you want to know more about why your answer is incorrect?";
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
        //ReviewPanel.gameObject.SetActive(false);
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
        TimerStart();
        //GetFeedback();
    }

    public void GetFeedback()
    {
        //Get feedback from the player
        feedbacking = true;
        FeedbackPanel.gameObject.SetActive(true);
        Dialogue.GetComponent<TMPro.TextMeshProUGUI>().text = "How do you feel about this question?";
    }

    public void CallNextQuestion()
    {
        FeedbackPanel.gameObject.SetActive(false);
        feedbacking = false;
        QManager.NextQuestion();
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
        retry = 0;
        CorrectReviewPanel.SetActive(false);
        ReviewPanel.gameObject.SetActive(false);
        ReviewOrNotPanel.gameObject.SetActive(false);
        FeedbackPanel.gameObject.SetActive(false);
        ElaborateFeedbackPanel.SetActive(false);
        feedbacking = false;
        //what happen after pressing space
        //Dialogue.GetComponent<TMPro.TextMeshProUGUI>().text = QuestionText;
        AnswerPanel.SetActive(true);
        QManager.SetQuestionText(retry);
        if (!answering)
        {
            TimerStart();
            answering = true;
        }
    }

    public void DialogueContinue(){
        CorrectReviewPanel.SetActive(false);
        ReviewPanel.gameObject.SetActive(false);
        ReviewOrNotPanel.gameObject.SetActive(false);
        FeedbackPanel.gameObject.SetActive(false);
        ElaborateFeedbackPanel.SetActive(false);
        QManager.SetDialogueText();
        feedbacking = false;
    }
    public void ContinueAfterFeedback()
    {
        QManager.NextQuestion();
        ElaborateFeedbackPanel.SetActive(false);
        retry = 0;
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
    public void TimerStart()
    {
        if (QManager.HasTimer())
        {
            timerGO.SetActive(true);
            timer.StartTimer();
        }
    }

    public void UpdateScore(int score)
    {
        GManager.UpdateScore(currentScore + score);
    }

    public void SetFeedbacking(bool x){
        feedbacking = x;
    }

    public void SetSpeaker(int speaker){
        speakerAvatar.texture = avatars[speaker];
    }

    public void OpenWindow(){
        Application.OpenURL("https://www.youtube.com/watch?v=VpQVQv5DSe8");
        CallReview();
    }
}
