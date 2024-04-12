using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{

    public int lesson; // Last lesson accessed by player
    public float[] position; // Last location of player in scene


    public PlayerData(Player player)
    {

        lesson = player.lesson;

        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;
    }
}
