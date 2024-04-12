using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SavePlayer(Player player)
    {
        // create a Binary Formatter object
        BinaryFormatter formatter = new BinaryFormatter();
        // define a data path to save file
        string path = Application.persistentDataPath + "/player.fun";
        //if file exists, overwrite, else create new file 
        FileStream stream = new FileStream(path, FileMode.Create);
        // instantiate new playerData object
        PlayerData data = new PlayerData(player);
        // serialize the player's data and write to file stream
        formatter.Serialize(stream, data);
        // close file stream
        stream.Close();
    }

    /// <summary>
    /// This method reads the serialized content of save file, deserializes it,
    /// puts it into a PlayerData object, and returns that object.
    /// </summary>
    /// <returns></returns>
    public static PlayerData LoadPlayer()
    {
        // define a data path to save file
        string path = Application.persistentDataPath + "/player.fun";
        // if there is a save file...
        if (File.Exists(path))
        {
            // instantiate BinaryFormatter object
            BinaryFormatter formatter = new BinaryFormatter();
            // Open the save file into a file stream
            FileStream stream = new FileStream(path, FileMode.Open);
            // Deserialize the contents of the save file
            // Instantiate new PlayerData object with data from save file
            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            // close file stream
            stream.Close();
            // return the PlayerData object
            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }


}
