using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class ScoreSystem : MonoBehaviour
{
    public GameObject scoreText;
    public int scoreToWin = 3;
    public AudioSource collectScoreSound;
    public GameOverScreen GameOverScreen;

    public void GameOver()
    {
        GameOverScreen.Setup();
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        PlayerInfo player = other.GetComponent<PlayerInfo>();
        collectScoreSound.Play();
        player.score += 50;
        scoreText.GetComponent<Text>().text = "SCORE: " + player.score;
        Destroy(gameObject);
        if (player.score >= scoreToWin)
        {
            GameOver();
        }
    }
}
