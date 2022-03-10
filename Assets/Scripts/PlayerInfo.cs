using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{

    public int playerHealth;
    public void playerHit(int damage)
    {
        playerHealth -= damage;
        Debug.Log("health" + playerHealth);
    }
}
