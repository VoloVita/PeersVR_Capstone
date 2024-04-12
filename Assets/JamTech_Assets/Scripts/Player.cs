using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int lesson;

    void Start()
    {
     //   LoadPlayer();
    }
    void OnDestroy()
    {
     //   SavePlayer();
    }
    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);

    }

    /// <summary>
    /// This method receives a PlayerData object from SaveSystem.LoadPlayer()
    /// and updates This game object's parameters.
    /// </summary>
    public void LoadPlayer()
    {
        // Receives PlayerData object created from save file
        PlayerData data = SaveSystem.LoadPlayer();

        // Check if PlayerData is null
        if (data != null)
        {
            // update players current lesson
            lesson = data.lesson;
            // create new Vector based on save file position
            Vector3 saved_pos = new Vector3(data.position[0], data.position[1], data.position[2]);
            // set this GameObject's transform to saved position
            transform.position = saved_pos;
        }
        else
        {
            // Handle the case where data is null (e.g., log a warning or take appropriate action)
            Debug.LogWarning("Failed to load player data: Data is null.");
        }
    }
}
