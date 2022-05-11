using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int[] scores;

    private bool[] levelsLockstates;

    private static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.Log("There's no game manager");
            }
            return instance;
        }
    }

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        scores = new int[9];

        levelsLockstates = new bool[9];
        InitScenesLocker();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public int GetScore(int idx)
    {
        return scores[idx];
    }

    public void UpdateScore(int score)
    {
        int idx = SceneManager.GetActiveScene().buildIndex - 1;
        if (score > scores[idx])
        {
            scores[idx] = score;
        }

        CallUnlockNextLevel(idx);
    }


    private void InitScenesLocker()
    {
        SetLockState(0, true);
        for (int i = 1; i < levelsLockstates.Length; i++)
        {
            SetLockState(i, false);
        }
    }

    public bool GetLockState(int idx)
    {
        return levelsLockstates[idx];
    }

    public void SetLockState(int idx, bool isUnlock)
    {
        levelsLockstates[idx] = isUnlock;
    }

    public void CallUnlockNextLevel(int idx) //same as idx in score[], for level 1, idx is 0
    {
        if (idx < levelsLockstates.Length - 1)//goal 3 level 3 does not need to unlock next level, so levelsLockstates[8] do nothing
        {
            SetLockState(idx + 1, true);
        }
    }

}
