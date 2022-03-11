using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class ScoreSystem : MonoBehaviour
{
    public GameObject scoreText;
    public int score;
    public AudioSource collectScoreSound;

    void OnTriggerEnter(Collider other)
    {
        collectScoreSound.Play();
        score += 50;
        scoreText.GetComponent<Text>().text = "SCORE: " + score;
        Destroy(gameObject);
    }
}
