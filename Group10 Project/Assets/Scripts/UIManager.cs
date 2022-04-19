using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    public GameObject MenuPanel;
    public GameObject AnswerPanel;
    public GameObject DialoguePanel;
    public GameObject ReviewOrNotPanel;
    public GameObject ReviewPanel;
    public GameObject FeedbackPanel;
    public GameObject Dialogue;
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
    public GameObject timerGO;

    public string DialogueText;
    public string QuestionText;
    private bool feedbacking; //stop calling next question if providing feedback
    private bool retry;
    private Timer timer;
    private Slider timerSlider;



    void Start()
    {
        feedbacking = false;
        //Just for this version:
        DialogueText = "People talk";
        QuestionText = "Question contents";
        SetPanels();
        SetDialogue();
        for (int i = 0; i < answers.Length; i++)
        {
            int closureIndex = i; //prevents closure problem
            answers[closureIndex].onClick.AddListener(() => TaskOnClick(closureIndex));
        }
        feedbackYes.onClick.AddListener(() =>FeedbackYes());
        feedbackNo.onClick.AddListener(() =>FeedbackNo());
        finishReviewButton.onClick.AddListener(() =>CallSimilarQuestion());
        goodButton.onClick.AddListener(() =>Continue());
        badButton.onClick.AddListener(() =>Continue());
        continueButton.onClick.AddListener(() =>ContinueAfterFeedback());
        acceptReviewButton.onClick.AddListener(() =>CallReview());
        refuseReviewButton.onClick.AddListener(() =>Continue());

    }

    void Update()
    {
        if (Dialogue != null)
        {
            TurnPages();
        }
        if(!timer.IsActive()){
            timerGO.SetActive(false);
        }else{
            timerSlider.value = timer.TimeLeft()/timer.timeLimit;
        }
    }


    public void SetPanels()
    {
        if (MenuPanel != null)
        {
            MenuPanel.gameObject.SetActive(true);
        }
        if (AnswerPanel != null)
        {
            AnswerPanel.gameObject.SetActive(false);
        }
        if (DialoguePanel != null)
        {
            DialoguePanel.gameObject.SetActive(true);
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
        if(ElaborateFeedbackPanel != null){
            ElaborateFeedbackPanel.SetActive(false); 
        }
        if(CorrectReviewPanel != null){
            CorrectReviewPanel.SetActive(false);
        }
        if(timerGO != null){
            timerGO.SetActive(false);
            timer = timerGO.GetComponent<Timer>();
            timerSlider = timerGO.GetComponent<Slider>();
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
            Continue();


        }
    }
    public void NextScene()
    {
        MenuPanel.gameObject.SetActive(false);

        Scene scene = SceneManager.GetActiveScene();
        int lastScene = SceneManager.sceneCountInBuildSettings - 1;
        int next;
        if (scene.buildIndex == lastScene)
        {
            next = 0;
        }
        else
        {
            next = scene.buildIndex + 1;
        }
        SceneManager.LoadScene(next);
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
        else if(answer && retry)
        {
            Dialogue.GetComponent<TMPro.TextMeshProUGUI>().text = "Well Done!!!";
            GetFeedback();
        }
        else if(!answer && retry)
        {
            ElaborateFeedback();
        }
        else
        {
            //player answers incorrectly.
            retry  = true;
            ReviewOrNot();
        }
    }

    public void ElaborateFeedback(){
        ElaborateFeedbackPanel.SetActive(true);
        elaborateFeedback.GetComponent<TMPro.TextMeshProUGUI>().text = QManager.GetElaborateFeedback();
    }

    public void TaskOnClick(int idx)
    {
        timer.EndTimer();
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
        TimerStart();
        //GetFeedback();
    }

    public void GetFeedback()
    {
        //Get feedback from the player
        FeedbackPanel.gameObject.SetActive(true);
        Dialogue.GetComponent<TMPro.TextMeshProUGUI>().text = "How do you feel about this question?";
    }

    public void FeedbackYes(){
       CallReview();
    }

    public void FeedbackNo(){
        DialoguePanel.SetActive(true);
        AnswerPanel.SetActive(true);
        QManager.SetQuestionText(retry);
        TimerStart();
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
        TimerStart();
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
    public void QuestionReview(){

    }
    public void TimerStart(){
        if(QManager.HasTimer())
        {
            timerGO.SetActive(true);
            timer.StartTimer();
        }
    }
}
