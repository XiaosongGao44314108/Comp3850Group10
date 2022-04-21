using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int [] scores;

    private static GameManager instance;

    public static GameManager Instance{
        get{
            if(instance == null){
                Debug.Log("There's no game manager");
            }
            return instance;
        }
    }

    void Awake(){
        if(instance != null){
            Destroy(gameObject);
        }else{
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        scores = new int [9];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetScore(int idx){
        return scores[idx];
    }
    
    public void UpdateScore(int score){
        int idx = SceneManager.GetActiveScene().buildIndex;
        if(score > scores[idx]){
            scores[idx] = score;
        }
    }
}
