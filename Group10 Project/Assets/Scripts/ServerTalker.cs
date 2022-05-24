using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;

public class ServerTalker : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetWebData("http://localhost:8000/user/", "banana"));
        
    }

    void ProcessServerResponse( string rawResponse)
    {
        JSONNode node = JSON.Parse( rawResponse);
        Debug.Log( node["username"]);
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
}
