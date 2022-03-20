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
    public GameObject PausePanel;
    public GameObject Dialogue;


    public string DialogueText;
    public string QuestionText;

    void Start()
    {
        //Just for this version:
        DialogueText = "People talk";
        QuestionText = "Question contents";
        SetPanels();
        SetDialogue();
    }

    void Update()
    {
        TurnPages();
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
        if (PausePanel != null)
        {
            PausePanel.gameObject.SetActive(false);
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
            Dialogue.GetComponent<TMPro.TextMeshProUGUI>().text = QuestionText;
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

    public void CallPause()
    {
        DialoguePanel.SetActive(false);
        AnswerPanel.SetActive(false);
        PausePanel.SetActive(true);
    }

    public void CallContinue()
    {
        DialoguePanel.SetActive(true);
        AnswerPanel.SetActive(true);
        PausePanel.SetActive(false);
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
