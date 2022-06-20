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

        form.AddField("entry.34897578", (gm.ID));   //ID

        //Goal 1 Level 1 stats
        form.AddField("entry.1080417622",gm.GetScore(G1L1).ToString());     //Highscore 
        form.AddField("entry.369565930", gm.GetTime(G1L1).ToString());      //Total time spent
        form.AddField("entry.492518558", gm.GetAttempts(G1L1).ToString());  //Total attempts
        form.AddField("entry.1377791902", gm.GetCorrectAnswers(G1L1).ToString());  //Correct answers
        form.AddField("entry.1992670242", gm.GetWrongAnswers(G1L1).ToString());  //Incorrect answers
        form.AddField("entry.1298942318", gm.GetOptFeedback(G1L1).ToString());  //Total number opted for feedback
        form.AddField("entry.99814428", gm.GetVideoFeedback(G1L1).ToString());  //Total number of video feedback
        form.AddField("entry.169917066", gm.GetTextFeedback(G1L1).ToString());  //Total number of text feedback
        form.AddField("entry.1047875063", gm.GetGoodFeedback(G1L1).ToString());  //Good Feedback
        form.AddField("entry.204688757", gm.GetBadFeedback(G1L1).ToString());  //Bad Feedback
        //Goal 1 Level 2 stats
        form.AddField("entry.923020887", gm.GetScore(G1L2).ToString());
        form.AddField("entry.176841123", gm.GetTime(G1L2).ToString());
        form.AddField("entry.1277324144",gm.GetAttempts(G1L2).ToString());
        form.AddField("entry.826040159", gm.GetCorrectAnswers(G1L2).ToString());  //Correct answers
        form.AddField("entry.1491113969", gm.GetWrongAnswers(G1L2).ToString());  //Incorrect answers
        form.AddField("entry.1611035522", gm.GetOptFeedback(G1L2).ToString());  //Total number opted for feedback
        form.AddField("entry.939219834", gm.GetVideoFeedback(G1L2).ToString());  //Total number of video feedback
        form.AddField("entry.1074652144", gm.GetTextFeedback(G1L2).ToString());  //Total number of text feedback
        form.AddField("entry.637733665", gm.GetGoodFeedback(G1L2).ToString());  //Good Feedback
        form.AddField("entry.1466453324", gm.GetBadFeedback(G1L2).ToString());  //Bad Feedback

        //Goal 1 Level 3 stats
        form.AddField("entry.312124291", gm.GetScore(G1L3).ToString());
        form.AddField("entry.161198104", gm.GetTime(G1L3).ToString());
        form.AddField("entry.1206017703",gm.GetAttempts(G1L3).ToString());
        form.AddField("entry.262877016", gm.GetCorrectAnswers(G1L3).ToString());  //Correct answers
        form.AddField("entry.788300970", gm.GetWrongAnswers(G1L3).ToString());  //Incorrect answers
        form.AddField("entry.1887352581", gm.GetOptFeedback(G1L3).ToString());  //Total number opted for feedback
        form.AddField("entry.1248293545", gm.GetVideoFeedback(G1L3).ToString());  //Total number of video feedback
        form.AddField("entry.1079711192", gm.GetTextFeedback(G1L3).ToString());  //Total number of text feedback
        form.AddField("entry.1282656392", gm.GetGoodFeedback(G1L3).ToString());  //Good Feedback
        form.AddField("entry.1034970297", gm.GetBadFeedback(G1L3).ToString());  //Bad Feedback

        //Goal 2 Level 1 stats
        form.AddField("entry.1947427761",gm.GetScore(G2L1).ToString());
        form.AddField("entry.810207971", gm.GetTime(G2L1).ToString());
        form.AddField("entry.1514502663",gm.GetAttempts(G2L1).ToString());
        form.AddField("entry.1995765599", gm.GetCorrectAnswers(G2L1).ToString());  //Correct answers
        form.AddField("entry.247156845", gm.GetWrongAnswers(G2L1).ToString());  //Incorrect answers
        form.AddField("entry.848738735", gm.GetOptFeedback(G2L1).ToString());  //Total number opted for feedback
        form.AddField("entry.1592822236", gm.GetVideoFeedback(G2L1).ToString());  //Total number of video feedback
        form.AddField("entry.1150970646", gm.GetTextFeedback(G2L1).ToString());  //Total number of text feedback
        form.AddField("entry.415737149", gm.GetGoodFeedback(G2L1).ToString());  //Good Feedback
        form.AddField("entry.1210223670", gm.GetBadFeedback(G2L1).ToString());  //Bad Feedback
        //Goal 2 Level 2 stats
        form.AddField("entry.1006584821", gm.GetScore(G2L2).ToString());
        form.AddField("entry.1826112354", gm.GetTime(G2L2).ToString());
        form.AddField("entry.1577019073", gm.GetAttempts(G2L2).ToString());
        form.AddField("entry.61648891", gm.GetCorrectAnswers(G2L2).ToString());  //Correct answers
        form.AddField("entry.1557131185", gm.GetWrongAnswers(G2L2).ToString());  //Incorrect answers
        form.AddField("entry.1426835391", gm.GetOptFeedback(G2L2).ToString());  //Total number opted for feedback
        form.AddField("entry.366582017", gm.GetVideoFeedback(G2L2).ToString());  //Total number of video feedback
        form.AddField("entry.1699025472", gm.GetTextFeedback(G2L2).ToString());  //Total number of text feedback
        form.AddField("entry.1275859945", gm.GetGoodFeedback(G2L2).ToString());  //Good Feedback
        form.AddField("entry.257502728", gm.GetBadFeedback(G2L2).ToString());  //Bad Feedback
        //Goal 2 Level 3 stats
        form.AddField("entry.108712064", gm.GetScore(G2L3).ToString());
        form.AddField("entry.1271957616", gm.GetTime(G2L3).ToString());
        form.AddField("entry.1396601119", gm.GetAttempts(G2L3).ToString());
        form.AddField("entry.497459561", gm.GetCorrectAnswers(G2L3).ToString());  //Correct answers
        form.AddField("entry.76946656", gm.GetWrongAnswers(G2L3).ToString());  //Incorrect answers
        form.AddField("entry.936248726", gm.GetOptFeedback(G2L3).ToString());  //Total number opted for feedback
        form.AddField("entry.711541309", gm.GetVideoFeedback(G2L3).ToString());  //Total number of video feedback
        form.AddField("entry.1119564091", gm.GetTextFeedback(G2L3).ToString());  //Total number of text feedback
        form.AddField("entry.81368307", gm.GetGoodFeedback(G2L3).ToString());  //Good Feedback
        form.AddField("entry.360491193", gm.GetBadFeedback(G2L3).ToString());  //Bad Feedback

        //Goal 3 Level 1 stats
        form.AddField("entry.1502111622", gm.GetScore(G3L1).ToString());
        form.AddField("entry.174673293", gm.GetTime(G3L1).ToString());
        form.AddField("entry.1697976351", gm.GetAttempts(G3L1).ToString());
        form.AddField("entry.657285965", gm.GetCorrectAnswers(G3L1).ToString());  //Correct answers
        form.AddField("entry.1689844332", gm.GetWrongAnswers(G3L1).ToString());  //Incorrect answers
        form.AddField("entry.959908058", gm.GetOptFeedback(G3L1).ToString());  //Total number opted for feedback
        form.AddField("entry.1463019555", gm.GetVideoFeedback(G3L1).ToString());  //Total number of video feedback
        form.AddField("entry.450278386", gm.GetTextFeedback(G3L1).ToString());  //Total number of text feedback
        form.AddField("entry.1665732490", gm.GetGoodFeedback(G3L1).ToString());  //Good Feedback
        form.AddField("entry.2119999843", gm.GetBadFeedback(G3L1).ToString());  //Bad Feedback
        //Goal 3 Level 2 stats
        form.AddField("entry.102906973", gm.GetScore(G3L2).ToString());
        form.AddField("entry.1516867018", gm.GetTime(G3L2).ToString());
        form.AddField("entry.1055877435", gm.GetAttempts(G3L2).ToString());
        form.AddField("entry.2016782363", gm.GetCorrectAnswers(G3L2).ToString());  //Correct answers
        form.AddField("entry.617863815", gm.GetWrongAnswers(G3L2).ToString());  //Incorrect answers
        form.AddField("entry.88678081", gm.GetOptFeedback(G3L2).ToString());  //Total number opted for feedback
        form.AddField("entry.1568431329", gm.GetVideoFeedback(G3L2).ToString());  //Total number of video feedback
        form.AddField("entry.913407041", gm.GetTextFeedback(G3L2).ToString());  //Total number of text feedback
        form.AddField("entry.443670852", gm.GetGoodFeedback(G3L2).ToString());  //Good Feedback
        form.AddField("entry.1145231053", gm.GetBadFeedback(G3L2).ToString());  //Bad Feedback
        //Goal 3 Level 3 stats
        form.AddField("entry.1102200459", gm.GetScore(G3L3).ToString());
        form.AddField("entry.726837207", gm.GetTime(G3L3).ToString());
        form.AddField("entry.1633045288", gm.GetAttempts(G3L3).ToString());
        form.AddField("entry.1937373230", gm.GetCorrectAnswers(G3L3).ToString());  //Correct answers
        form.AddField("entry.583958549", gm.GetWrongAnswers(G3L3).ToString());  //Incorrect answers
        form.AddField("entry.834312602", gm.GetOptFeedback(G3L3).ToString());  //Total number opted for feedback
        form.AddField("entry.241281674", gm.GetVideoFeedback(G3L3).ToString());  //Total number of video feedback
        form.AddField("entry.887159344", gm.GetTextFeedback(G3L3).ToString());  //Total number of text feedback
        form.AddField("entry.302702412", gm.GetGoodFeedback(G3L3).ToString());  //Good Feedback
        form.AddField("entry.718447437", gm.GetBadFeedback(G3L3).ToString());  //Bad Feedback

        form.AddField("entry.622307752", gm.GetTotalTime().ToString()); //Total time across all levels

        UnityWebRequest www = UnityWebRequest.Post(url, form);
        yield return www.SendWebRequest();
    }
}
