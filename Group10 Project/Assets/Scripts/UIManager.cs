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
    public GameObject Dialogue;
    public QuestionManager QManager;
    public Button[] answers;

    public string DialogueText;
    public string QuestionText;

    private bool answered;


    void Start()
    {
        //Just for this version:
        DialogueText = "People talk";
        QuestionText = "Question contents";
        answered = false;
        SetPanels();
        SetDialogue();
        for(int i = 0; i<answers.Length;i++){
            int closureIndex = i ; //prevents closure problem
            answers[closureIndex].onClick.AddListener(() => TaskOnClick(closureIndex));
        }
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
        }
        if (AnswerPanel != null)
        {
            AnswerPanel.gameObject.SetActive(false);
        }
        if (DialoguePanel != null)
        {
            DialoguePanel.gameObject.SetActive(true);
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
        if (Input.GetButton("TurnPages"))
        {
            //what happen after pressing space
            //Dialogue.GetComponent<TMPro.TextMeshProUGUI>().text = QuestionText;
            AnswerPanel.SetActive(true);
            QManager.SetQuestionText();
            
        }

        if (Input.GetMouseButtonDown(0))
        {
            //what happen after clicking dialogue
            RectTransform rect = AnswerPanel.GetComponent<RectTransform>();
            bool mouseOnDia = RectTransformUtility.RectangleContainsScreenPoint(rect, Input.mousePosition);
            if (!mouseOnDia)
            {
                //Dialogue.GetComponent<TMPro.TextMeshProUGUI>().text = QuestionText;
                
                AnswerPanel.SetActive(true);
                QManager.SetQuestionText();
    
            }
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
        if(answer){
            Dialogue.GetComponent<TMPro.TextMeshProUGUI>().text = "Well Done!!!";
        }else{
            Dialogue.GetComponent<TMPro.TextMeshProUGUI>().text = "Not Quite, let's try again";
        }
    }

    public void TaskOnClick(int idx){
      QManager.AnswerQuestion(idx);
    }


    public void ExitGame()
    {
        //quit editting mode in unity, also quit game
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
