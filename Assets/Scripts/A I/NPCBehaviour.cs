using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class NPCBehaviour : MonoBehaviour
{   
   // public GameObject npc;
    public GameObject player; 
    
    private float targetDistance;
    public float chaseRange;
    public float followSpeed;

    private RaycastHit _shot;
    
    
    void Update()
    {
        transform.LookAt(player.transform);
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out _shot))
        {
            targetDistance = _shot.distance;
            if (targetDistance <= chaseRange)
            {
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, followSpeed * Time.deltaTime);
            }
            
        }
    }
}
