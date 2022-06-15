using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int[] scores;
    public int[] Scores{
        get
        {
            return scores;
        }
    }
    private float[] times;
    public float[] Times
    {
        get
        {
            return times;
        }
    }
    private int[] attempts;
    public int[] Attempts
    {
        get
        {
            return attempts;
        }
    }

    private bool[] levelsLockstates;
    public bool[] LevelsLockstates
    {
        get
        {
            return levelsLockstates;
        }
    }

    private PlayerData playerData;

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
            DontDestroyOnLoad(this.gameObject);
        }

        playerData = SaveSystem.LoadData();
    }
    // Start is called before the first frame update
    void Start()
    {
        if(playerData == null)
        {
            scores = new int[9];
            times = new float[9];
            attempts = new int[9];
            levelsLockstates = new bool[9];
            InitScenesLocker();
        }
        else
        {
            scores = playerData.scores;
            times = playerData.times;
            attempts = playerData.attempts;
            levelsLockstates = playerData.levelsLockstates;
            playerData = new PlayerData(this);
        }
        
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
    }

    public float GetTime(int idx)
    {
        return times[idx];
    }

    public void UpdateTime()
    {
        Debug.Log(Time.timeSinceLevelLoad);
        int idx = SceneManager.GetActiveScene().buildIndex - 1;
        times[idx] += (float)Time.timeSinceLevelLoad;
    }

    public float GetTotalTime()
    {
        float totalTime = 0;
        foreach(float t in times)
        {
            totalTime += t;
        }

        return totalTime;
    }

    public int GetAttempts(int idx)
    {
        return attempts[idx];
    }

    public void UpdateAttempts()
    {
        int idx = SceneManager.GetActiveScene().buildIndex - 1;
        attempts[idx]++;
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
        }else{
            ServerTalker.Instance.SendData();
        }
        SaveSystem.SaveData();
    }

}
