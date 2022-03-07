using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = System.Random;

public class AIEnemyBehaviour : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    //Path for patrolling

    public Vector3 walkPoint;
    public bool walkPointSet;
    public float walkPointDisRange;

    //attack

    public float attackTime;
    private bool alreadyAttacked;
    public GameObject projectile;


    //AIStates
    public float reachRange, attackRange;
    public bool playerInSight, playerInAttack;

    private void Awake()
    {
        player = GameObject.Find("airPlane").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        var position = transform.position;
        playerInSight = Physics.CheckSphere(position, reachRange, whatIsPlayer);
        playerInAttack = Physics.CheckSphere(position, attackRange, whatIsPlayer);

        if(!playerInSight && !playerInAttack) Patrolling();
        if(playerInSight && !playerInAttack) ChasePlayer();
        if(playerInAttack && playerInSight) AttackPlayer();

    }

    private void Patrolling()
    {
        if (!walkPointSet)
        {
            searchWalkPoint();
        }

        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;
      
        //walkpoint reach
        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }
    }

    public void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointDisRange, walkPointDisRange);
        float randomX = Random.Range(-walkPointDisRange, walkPointDisRange);

        var position = transform.position;
        walkPoint = new Vector3(position.x + randomX, position.y, position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
        }
    }
    
    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }
    private void AttackPlayer()
    {
        //enemy not moving;

        agent.SetDestination(transform.position);
        
        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rb.AddForce(transform.up * 8f, ForceMode.Impulse);

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), attackTime);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }
    
}
