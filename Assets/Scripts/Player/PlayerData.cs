using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int currentLevel;
    public int lives;
    public int damage;
    public int soul;
    public float[] position;

    public PlayerData(Player player)
    {
        currentLevel = player.currentLevel;
        lives = player.lives;
        damage = player.damage;
        soul = player.soul;

        position = new float[2];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
    }

}
