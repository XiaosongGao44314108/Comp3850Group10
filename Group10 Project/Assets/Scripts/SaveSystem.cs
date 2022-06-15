using UnityEngine;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
   public static void SaveData()
   {
       BinaryFormatter formatter = new BinaryFormatter();
       string path = Application.persistentDataPath + "/player.bfm";
       FileStream stream = new FileStream(path, FileMode.Create);

       PlayerData data = new PlayerData(GameManager.Instance);

       formatter.Serialize(stream,data);
       stream.Close(); 
   }

   public static PlayerData LoadData()
   {
       string path = Application.persistentDataPath + "/player.bfm";
       if(File.Exists(path))
       {
           BinaryFormatter formatter = new BinaryFormatter();
           FileStream stream = new FileStream(path, FileMode.Open);

           PlayerData data = formatter.Deserialize(stream) as PlayerData;
           stream.Close();
           return data;
       }
       else
       {
           return null;
       }
   }
}
