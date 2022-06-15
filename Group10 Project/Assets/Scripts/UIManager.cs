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
    public GameObject FourChoicesPanel;
    public GameObject ImaFourChoicesPanel;
    public GameObject ThreeChoicesPanel;
    public GameObject ImaThreeChoicesPanel;
    public GameObject TwoChoicesPanel;
    public GameObject ImaTwoChoicesPanel;
    // private Vector3 ImaFourChoicesPos; //if question contains an image, the "child objects" of answer panel will move to these positions
    // private Vector3 ImaThreeChoicesPos;
    // private Vector3 ImaTwoChoicesPos;
    public RawImage ChoicesIma;//image in multi-choices question
    public Texture[] QuestionImgs;
    private Vector3 ChoicesImaPos;

    public GameObject NumericAnswerPanel;
    public GameObject DialoguePanel;
    public GameObject ReviewOrNotPanel;
    public GameObject ReviewPanel;
    public GameObject ReviewTextPanel;
    public GameObject ReviewButtons;
    public GameObject FinishReviewButton;
    public GameObject FeedbackPanel;
    public GameObject Dialogue;  //dialogue text
    public GameObject ElaborateFeedbackPanel;
    public GameObject CorrectReviewPanel;
    public Button returnButton;
    public Texture[] avatars;
    public RawImage speakerAvatar;
    public Button goalOneButton;
    public Button goalTwoButton;
    public Button goalThreeButton;
    public Button levelOneButton;
    public Button levelTwoButton;
    public Button levelThreeButton;


    private GameManager GManager;
    public QuestionManager QManager;
    public TextMeshProUGUI elaborateFeedback;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI Lvl1HighscoreText;
    public TextMeshProUGUI Lvl2HighscoreText;
    public TextMeshProUGUI Lvl3HighscoreText;
    public TextMeshProUGUI GoalLvlText;
    public GameObject timerGO;
    public TMP_InputField numericAnswerField;

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
        GManager = GameManager.Instance;
        IsDiaActive = false; //dialog is not activated at the beginning
        IsMainActive = false;
        SelectedGoalOne = false;
        SelectedGoalTwo = false;
        SelectedGoalThree = false;
        feedbacking = false;

        // ImaFourChoicesPos = new Vector3(0, -220, 0);
        // ImaThreeChoicesPos = new Vector3(0, -80, 0);
        // ImaTwoChoicesPos = new Vector3(0, -220, 0);

        currentScore = 0;
        //Just for this version:
        DialogueText = "People talk";
        QuestionText = "Question contents";
        SetPanels();
        if (Dialogue != null)
        {
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
        if (FourChoicesPanel != null)
        {
            FourChoicesPanel.gameObject.SetActive(false);
        }
        if (ImaFourChoicesPanel != null)
        {
            ImaFourChoicesPanel.gameObject.SetActive(false);
        }
        if (ThreeChoicesPanel != null)
        {
            ThreeChoicesPanel.gameObject.SetActive(false);
        }
        if (ImaThreeChoicesPanel != null)
        {
            ImaThreeChoicesPanel.gameObject.SetActive(false);
        }
        if (TwoChoicesPanel != null)
        {
            TwoChoicesPanel.gameObject.SetActive(false);
        }
        if (ImaTwoChoicesPanel != null)
        {
            ImaTwoChoicesPanel.gameObject.SetActive(false);
        }
        if (ChoicesIma != null)
        {
            ChoicesIma.gameObject.SetActive(false);
        }
        if (NumericAnswerPanel != null)
        {
            NumericAnswerPanel.gameObject.SetActive(false);
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

        //lock/unlock goal buttons:
        if (GManager.GetLockState(0))
        {
            goalOneButton.interactable = true;
        }
        else
        {
            goalOneButton.interactable = false;
        }
        if (GManager.GetLockState(3))
        {
            goalTwoButton.interactable = true;
        }
        else
        {
            goalTwoButton.interactable = false;
        }

        if (GManager.GetLockState(6))
        {
            goalThreeButton.interactable = true;
        }
        else
        {
            goalThreeButton.interactable = false;
        }
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

        //lock/unlock level buttons in goal one:
        if (GManager.GetLockState(0))
        {
            levelOneButton.interactable = true;
        }
        else
        {
            levelOneButton.interactable = false;
        }
        if (GManager.GetLockState(1))
        {
            levelTwoButton.interactable = true;
        }
        else
        {
            levelTwoButton.interactable = false;
        }
        if (GManager.GetLockState(2))
        {
            levelThreeButton.interactable = true;
        }
        else
        {
            levelThreeButton.interactable = false;
        }
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

        //lock/unlock level buttons in goal two:
        if (GManager.GetLockState(3))
        {
            levelOneButton.interactable = true;
        }
        else
        {
            levelOneButton.interactable = false;
        }
        if (GManager.GetLockState(4))
        {
            levelTwoButton.interactable = true;
        }
        else
        {
            levelTwoButton.interactable = false;
        }
        if (GManager.GetLockState(5))
        {
            levelThreeButton.interactable = true;
        }
        else
        {
            levelThreeButton.interactable = false;
        }
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
        Lvl2HighscoreText.GetComponent<TMPro.TextMeshProUGUI>().text = GManager.GetScore(7) + "";
        Lvl3HighscoreText.GetComponent<TMPro.TextMeshProUGUI>().text = GManager.GetScore(8) + "";

        //lock/unlock level buttons in goal three:
        if (GManager.GetLockState(6))
        {
            levelOneButton.interactable = true;
        }
        else
        {
            levelOneButton.interactable = false;
        }
        if (GManager.GetLockState(7))
        {
            levelTwoButton.interactable = true;
        }
        else
        {
            levelTwoButton.interactable = false;
        }
        if (GManager.GetLockState(8))
        {
            levelThreeButton.interactable = true;
        }
        else
        {
            levelThreeButton.interactable = false;
        }
    }

    public void LoadingLevelOne() //What happens after clicking Level One
    {
        if (SelectedGoalOne)
        {
            SceneManager.LoadScene(1);
            // GManager.SetLockState(1, true);//unlock g1l2
        }
        if (SelectedGoalTwo)
        {
            SceneManager.LoadScene(4);
            // GManager.SetLockState(4, true);//unlock g2L2
        }
        if (SelectedGoalThree)
        {
            SceneManager.LoadScene(7);
            // GManager.SetLockState(7, true);//unlock g3L2
        }

        IsMainActive = false;
    }

    public void LoadingLevelTwo() //What happens after clicking Level Two
    {
        if (SelectedGoalOne)
        {
            SceneManager.LoadScene(2);
            // GManager.SetLockState(2, true);//unlock g1l3
        }
        if (SelectedGoalTwo)
        {
            SceneManager.LoadScene(5);
            // GManager.SetLockState(5, true);//unlock g2L3
        }
        if (SelectedGoalThree)
        {
            SceneManager.LoadScene(8);
            // GManager.SetLockState(8, true);//unlock g3l3
        }

        IsMainActive = false;
    }

    public void LoadingLevelThree() //What happens after clicking Level Three
    {
        if (SelectedGoalOne)
        {
            SceneManager.LoadScene(3);
            // GManager.SetLockState(3, true);//unlock g2l1
        }
        if (SelectedGoalTwo)
        {
            SceneManager.LoadScene(6);
            // GManager.SetLockState(6, true);//unlock g3l1
        }
        if (SelectedGoalThree)
        {
            SceneManager.LoadScene(9);
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
        NumericAnswerPanel.SetActive(false);
        currentScore += score;
        scoreText.GetComponent<TMPro.TextMeshProUGUI>().text = "Score: " + currentScore;
        feedbacking = true;
        if (answer && retry == 0)
        {

            ReviewQuestion();
            Dialogue.GetComponent<TMPro.TextMeshProUGUI>().text = "Well Done!!!";
        }
        else if (answer && retry < 3)
        {
            Dialogue.GetComponent<TMPro.TextMeshProUGUI>().text = "Well Done!!!";
            CallNextQuestion();
            GetFeedback();

        }
        else if (!answer && retry == 2)
        {
            retry = 0;
            ElaborateFeedback();
        }
        else
        {
            //player answers incorrectly.
            retry++;
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
        ChoicesIma.gameObject.SetActive(false);
        answering = false;
        QManager.AnswerQuestion(idx);
    }

    public void SubmitNumericAnswer()
    {
        ChoicesIma.gameObject.SetActive(false);
        answering = false;
        QManager.AnswerNumericQuestion(int.Parse(numericAnswerField.text));
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
        NumericAnswerPanel.gameObject.SetActive(false);
        CorrectReviewPanel.gameObject.SetActive(false);
        Dialogue.GetComponent<TMPro.TextMeshProUGUI>().text = "";
        ReviewOrNotPanel.gameObject.SetActive(false);
        ReviewPanel.gameObject.SetActive(true);
    }

    public void CallTextReview()
    {
        ReviewTextPanel.gameObject.SetActive(true);
        ReviewPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(1000, 400);
        ReviewButtons.SetActive(false);
        FinishReviewButton.SetActive(true);
        FinishReviewButton.GetComponent<RectTransform>().localPosition = new Vector2(0, -170);
    }

    public void CallVideoReview()
    {
        ReviewButtons.SetActive(false);
        FinishReviewButton.SetActive(true);
        OpenWindow();
    }

    public void ResetReviewPanel()
    {
        ReviewTextPanel.gameObject.SetActive(false);
        ReviewPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(600, 100);
        FinishReviewButton.GetComponent<RectTransform>().localPosition = new Vector2(0, 0);
        ReviewButtons.SetActive(true);
        FinishReviewButton.SetActive(false);

    }
    public void CallSimilarQuestion()
    {
        //ReviewPanel.gameObject.SetActive(false);
        //display different question of similar type at her
        //do not know how to implement yet
        //need to add more contents at here later

        //ReviewOrNot Panel may not be actived yet if player chooses not to review related images
        ResetReviewPanel();
        ReviewPanel.gameObject.SetActive(false);
        ReviewOrNotPanel.gameObject.SetActive(false);
        //setup question
        DialoguePanel.SetActive(true);
        if (QManager.SetQuestionText(retry))
            SetNumericAnswerPanel();
        else
        {
            AnswerPanel.SetActive(true);
            SetAnswerPanels();
        }
        TimerStart();
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
        if (QManager.SetQuestionText(retry))
        {
            ChoicesIma.gameObject.SetActive(true);
            SetNumericAnswerPanel();
            if (!answering)
            {
                TimerStart();
                answering = true;
            }
        }
        else
        {
            AnswerPanel.SetActive(true);
            if (!answering)
            {
                TimerStart();
                answering = true;
            }
        }
    }

    public void SetAnswerPanels()
    {
        int numOfChoice = QManager.NumberOfAnswers();
        bool hasAnimage = QManager.ContainImage();

        if (numOfChoice == 4)//if number of Choices is 4
        {
            ImaThreeChoicesPanel.SetActive(false);
            ThreeChoicesPanel.SetActive(false);
            ImaTwoChoicesPanel.SetActive(false);
            TwoChoicesPanel.SetActive(false);

            if (hasAnimage)
            {
                FourChoicesPanel.SetActive(false);
                ImaFourChoicesPanel.SetActive(true);
                // FourChoicesPanel.transform.localPosition = ImaFourChoicesPos;
                ChoicesIma.gameObject.SetActive(true);
                // ChoicesImaPos = new Vector3(-350, 0, 0);
                // ChoicesIma.transform.localPosition = ChoicesImaPos;
                ChoicesIma.texture = QuestionImgs[QManager.ImageInQuestion()];
            }
            else
            {
                FourChoicesPanel.SetActive(true);
                ImaFourChoicesPanel.SetActive(false);
                // FourChoicesPanel.transform.localPosition = new Vector3(0, 0, 0);
                ChoicesIma.gameObject.SetActive(false);
            }
        }
        else if (numOfChoice == 3)
        {
            ImaFourChoicesPanel.SetActive(false);
            FourChoicesPanel.SetActive(false);
            ImaTwoChoicesPanel.SetActive(false);
            TwoChoicesPanel.SetActive(false);
            if (hasAnimage)
            {
                ThreeChoicesPanel.SetActive(false);
                ImaThreeChoicesPanel.SetActive(true);
                // ThreeChoicesPanel.transform.localPosition = ImaThreeChoicesPos;
                ChoicesIma.gameObject.SetActive(true);
                // ChoicesImaPos = new Vector3(-515, 0, 0);
                // ChoicesIma.transform.localPosition = ChoicesImaPos;
                ChoicesIma.texture = QuestionImgs[QManager.ImageInQuestion()];
            }
            else
            {
                ThreeChoicesPanel.SetActive(true);
                ImaThreeChoicesPanel.SetActive(false);
                // ThreeChoicesPanel.transform.localPosition = new Vector3(0, 0, 0);
                ChoicesIma.gameObject.SetActive(false);
            }
        }
        else
        {
            ImaFourChoicesPanel.SetActive(false);
            FourChoicesPanel.SetActive(false);
            ImaThreeChoicesPanel.SetActive(false);
            ThreeChoicesPanel.SetActive(false);
            if (hasAnimage)
            {
                TwoChoicesPanel.SetActive(false);
                ImaTwoChoicesPanel.SetActive(true);
                // TwoChoicesPanel.transform.localPosition = ImaTwoChoicesPos;
                ChoicesIma.gameObject.SetActive(true);
                // ChoicesImaPos = new Vector3(-350, 0, 0);
                // ChoicesIma.transform.localPosition = ChoicesImaPos;
                ChoicesIma.texture = QuestionImgs[QManager.ImageInQuestion()];
            }
            else
            {
                TwoChoicesPanel.SetActive(true);
                ImaTwoChoicesPanel.SetActive(false);
                // TwoChoicesPanel.transform.localPosition = new Vector3(0, 0, 0);
                ChoicesIma.gameObject.SetActive(false);
            }
        }
    }

    public void DialogueContinue()
    {
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
        ElaborateFeedbackPanel.SetActive(false);
        retry = 0;
        //what happen after pressing space
        //Dialogue.GetComponent<TMPro.TextMeshProUGUI>().text = QuestionText;
        CallNextQuestion();
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

    public void UpdateScore()
    {
        GameManager.Instance.UpdateScore(currentScore);
    }

    public void SetFeedbacking(bool x)
    {
        feedbacking = x;
    }

    public void SetSpeaker(int speaker)
    {
        speakerAvatar.texture = avatars[speaker];
    }

    public void SetNumericAnswerPanel()
    {
        NumericAnswerPanel.SetActive(true);
        if(QManager.ContainImage())
        {
            ChoicesIma.gameObject.SetActive(true);
            ChoicesIma.texture = QuestionImgs[QManager.ImageInQuestion()];
            NumericAnswerPanel.GetComponent<RectTransform>().localPosition = new Vector3(0, -100, 0);
        }
        else
        {
            NumericAnswerPanel.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
        }
    }

    public void OpenWindow()
    {
        Application.OpenURL("https://www.youtube.com/watch?v=VpQVQv5DSe8");
        CallReview();
    }
}
