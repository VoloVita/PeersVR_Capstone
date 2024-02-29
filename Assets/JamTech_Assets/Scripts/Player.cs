using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int lesson;

    void Start()
    {
        LoadPlayer();
    }
    void OnDestroy()
    {
        SavePlayer();
    }
    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);

    }
    
    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        lesson = data.lesson;
        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        transform.position = position;
    }
    

}
