using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;

public class ServerTalker : MonoBehaviour
{
    private GameManager gm;

    private int G1L1 = 0;
    private int G1L2 = 1;
    private int G1L3 = 2;
    private int G2L1 = 3;
    private int G2L2 = 4;
    private int G2L3 = 5;
    private int G3L1 = 6;
    private int G3L2 = 7;
    private int G3L3 = 8;

    private static ServerTalker instance;

    public static ServerTalker Instance
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
    }

    [SerializeField]
    private string url = "https://docs.google.com/forms/u/0/d/e/1FAIpQLSc_Co6GkBU6bQygTlXkmYrygdrmJqSZ-enbfl8tX5SSKpbyrw/formResponse";
    
    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.Instance;   
    }

    public void SendData()
    {
        StartCoroutine(Post());   
    }

    // public void ProcessServerResponse( string rawResponse)
    // {
    //     JSONNode node = JSON.Parse( rawResponse);
    // }

    // IEnumerator GetWebData(string address, string myID)
    // {
    //     UnityWebRequest www = UnityWebRequest.Get(address + myID);
    //     yield return www.SendWebRequest();

    //     if(www.result != UnityWebRequest.Result.Success)
    //     {
    //         Debug.LogError("Did not recieve address");
    //     }
    //     else
    //     {
    //         Debug.Log(www.downloadHandler.text);

    //         ProcessServerResponse(www.downloadHandler.text);
    //     }
    // }

    IEnumerator Post()
    {
        WWWForm form = new WWWForm();
        form.AddField("entry.34897578", (Random.Range(0,1000000).ToString()));

        //Goal 1 Level 1 stats
        form.AddField("entry.1080417622",gm.GetScore(G1L1).ToString());
        form.AddField("entry.369565930", gm.GetTime(G1L1).ToString());
        form.AddField("entry.492518558", gm.GetAttempts(G1L1).ToString());
        //Goal 1 Level 2 stats
        form.AddField("entry.923020887", gm.GetScore(G1L2).ToString());
        form.AddField("entry.176841123", gm.GetTime(G1L2).ToString());
        form.AddField("entry.1277324144",gm.GetAttempts(G1L2).ToString());
        //Goal 1 Level 3 stats
        form.AddField("entry.312124291", gm.GetScore(G1L3).ToString());
        form.AddField("entry.161198104", gm.GetTime(G1L3).ToString());
        form.AddField("entry.1206017703",gm.GetAttempts(G1L3).ToString());

        //Goal 2 Level 1 stats
        form.AddField("entry.1947427761",gm.GetScore(G2L1).ToString());
        form.AddField("entry.810207971", gm.GetTime(G2L1).ToString());
        form.AddField("entry.1514502663",gm.GetAttempts(G2L1).ToString());
        //Goal 2 Level 2 stats
        form.AddField("entry.1006584821", gm.GetScore(G2L2).ToString());
        form.AddField("entry.1826112354", gm.GetTime(G2L2).ToString());
        form.AddField("entry.1577019073", gm.GetAttempts(G2L2).ToString());
        //Goal 2 Level 3 stats
        form.AddField("entry.108712064", gm.GetScore(G2L3).ToString());
        form.AddField("entry.1271957616", gm.GetTime(G2L3).ToString());
        form.AddField("entry.1396601119", gm.GetAttempts(G2L3).ToString());

        //Goal 3 Level 1 stats
        form.AddField("entry.1502111622", gm.GetScore(G3L1).ToString());
        form.AddField("entry.174673293", gm.GetTime(G3L1).ToString());
        form.AddField("entry.1697976351", gm.GetAttempts(G3L1).ToString());
        //Goal 3 Level 2 stats
        form.AddField("entry.102906973", gm.GetScore(G3L2).ToString());
        form.AddField("entry.1516867018", gm.GetTime(G3L2).ToString());
        form.AddField("entry.1055877435", gm.GetAttempts(G3L2).ToString());
        //Goal 3 Level 3 stats
        form.AddField("entry.1102200459", gm.GetScore(G3L3).ToString());
        form.AddField("entry.726837207", gm.GetTime(G3L3).ToString());
        form.AddField("entry.1633045288", gm.GetAttempts(G3L3).ToString());

        form.AddField("entry.622307752", gm.GetTotalTime().ToString());

        UnityWebRequest www = UnityWebRequest.Post(url, form);
        yield return www.SendWebRequest();
    }
}
