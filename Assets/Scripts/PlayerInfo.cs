using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfo : MonoBehaviour
{

    public int playerHealth;
    public GameObject healthText;
    public int score;
    public void playerHit(int damage)
    {
        playerHealth -= damage;
        Debug.Log("health" + playerHealth);
        healthText.GetComponent<Text>().text = "HEALTH:" + playerHealth;
    }
}
