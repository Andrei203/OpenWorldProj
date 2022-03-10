﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCProjectile : MonoBehaviour
{
    public float speed = 10.0f;
    public int damage = 2;

    [SerializeField] private float deathtime = 3.5f;
    //public GameObject playerPB;

    private void Start()
    {
        Destroy(this.gameObject, deathtime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0,0, speed * Time.deltaTime);
    }

    public void OnTriggerEnter(Collider other)
    {
        PlayerInfo player = other.GetComponent<PlayerInfo>();
        if (player != null)
        {
            player.playerHit(damage);
            Destroy(this.gameObject);
            
            if (player.playerHealth <= 0)
            {
                Destroy(player.gameObject);
            }
        }
    }
}
