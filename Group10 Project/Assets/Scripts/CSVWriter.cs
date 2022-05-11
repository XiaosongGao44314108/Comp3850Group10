using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CSVWriter : MonoBehaviour
{
    string filename = "";

    [System.Serializable]
    public class Player
    {
        public string id;
        public int progress;
    }

    [System.Serializable]
    public class PlayerList
    {
        public Player[] players;
    }

    public PlayerList myPlayerList = new PlayerList();
    // Start is called before the first frame update
    void Start()
    {
        filename = Application.dataPath + "/statData.csv";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void WriteCSV()
    {
        if(myPlayerList.players.Length > 0)
        {
            TextWriter tw = new StreamWriter(filename, false);
            tw.WriteLine("ID, Progress");
            tw.Close();

            tw = new StreamWriter(filename, true);
            for(int i = 0; i<myPlayerList.players.Length; i++)
            {
                tw.WriteLine(myPlayerList.players[i].id + ',' + myPlayerList.players[i].progress);
            }
            tw.Close();
        }
    }
}
