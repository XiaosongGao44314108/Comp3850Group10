using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject menuPanel;

    void Start()
    {
        menuPanel.gameObject.SetActive(true);
    }


    public void NextScene()
    {
        menuPanel.gameObject.SetActive(false);

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

    public void ExitGame()
    {
        //quit editting mode in unity, also quit game
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
