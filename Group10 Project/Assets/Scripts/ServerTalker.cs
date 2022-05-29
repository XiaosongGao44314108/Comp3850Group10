using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;

public class ServerTalker : MonoBehaviour
{
    [SerializeField]
    private string url = "https://docs.google.com/forms/u/0/d/e/1FAIpQLSfqqvlP2qtQXM3wk8PtYhsE8JJgnDodWwXVpYfQzyaEzPF5Aw/formResponse";
    // Start is called before the first frame update
    void Start()
    {
        // StartCoroutine(GetWebData("http://localhost:8000/user/", "banana"));   
        StartCoroutine(Post());    
        
    }

    void ProcessServerResponse( string rawResponse)
    {
        JSONNode node = JSON.Parse( rawResponse);
        // Debug.Log( node["username"]);
    }

    IEnumerator GetWebData(string address, string myID)
    {
        UnityWebRequest www = UnityWebRequest.Get(address + myID);
        yield return www.SendWebRequest();

        if(www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("bad");
        }
        else
        {
            Debug.Log(www.downloadHandler.text);

            ProcessServerResponse(www.downloadHandler.text);
        }
    }

    IEnumerator Post()
    {
        WWWForm form = new WWWForm();
        form.AddField("entry.77009095", (Random.Range(0,1000000).ToString()));
        UnityWebRequest www = UnityWebRequest.Post(url, form);
        yield return www.SendWebRequest();
    }
}
