using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesLocker : MonoBehaviour
{
    private static ScenesLocker instance;

    private bool g1L1;
    private bool g1L2;
    private bool g1L3;
    private bool g2L1;
    private bool g2L2;
    private bool g2L3;
    private bool g3L1;
    private bool g3L2;
    private bool g3L3;

    public static ScenesLocker Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.Log("There's no ScenesLocker");
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
        InitScenesLocker();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void InitScenesLocker()
    {
        SetG1L1(true);
        SetG1L2(false);
        SetG1L3(false);
        SetG2L1(false);
        SetG2L2(false);
        SetG2L3(false);
        SetG3L1(false);
        SetG3L2(false);
        SetG3L3(false);
    }

    public bool GetG1L1()
    {
        return g1L1;
    }

    public bool GetG1L2()
    {
        return g1L2;
    }

    public bool GetG1L3()
    {
        return g1L3;
    }

    public bool GetG2L1()
    {
        return g2L1;
    }

    public bool GetG2L2()
    {
        return g2L2;
    }

    public bool GetG2L3()
    {
        return g2L3;
    }

    public bool GetG3L1()
    {
        return g3L1;
    }

    public bool GetG3L2()
    {
        return g3L2;
    }

    public bool GetG3L3()
    {
        return g3L3;
    }

    public void SetG1L1(bool isNotLocked)
    {
        g1L1 = isNotLocked;
    }

    public void SetG1L2(bool isNotLocked)
    {
        g1L2 = isNotLocked;
    }

    public void SetG1L3(bool isNotLocked)
    {
        g1L3 = isNotLocked;
    }

    public void SetG2L1(bool isNotLocked)
    {
        g2L1 = isNotLocked;
    }

    public void SetG2L2(bool isNotLocked)
    {
        g2L2 = isNotLocked;
    }

    public void SetG2L3(bool isNotLocked)
    {
        g2L3 = isNotLocked;
    }

    public void SetG3L1(bool isNotLocked)
    {
        g3L1 = isNotLocked;
    }

    public void SetG3L2(bool isNotLocked)
    {
        g3L2 = isNotLocked;
    }

    public void SetG3L3(bool isNotLocked)
    {
        g3L3 = isNotLocked;
    }


}
