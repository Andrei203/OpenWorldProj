using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = System.Random;

public class NPCBehaviour : MonoBehaviour
{   
   // public GameObject npc;
    public GameObject player; 
    
    private float targetDistance;
    public float chaseRange;
    public float followSpeed;
    private float fireRate = 1.25f;
    private RaycastHit _shot;

    [SerializeField]private Transform firePoint;

    public GameObject bulletPB;
    private GameObject _bullet;
   
    
  public void Update()
    {
        transform.LookAt(player.transform);
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out _shot))
        {
            targetDistance = _shot.distance;
            if (targetDistance <= chaseRange)
            {
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position,
                    followSpeed * Time.deltaTime);
                
            }

            Ray ray = new Ray(transform.position, transform.forward);

            RaycastHit hit;
            fireRate -= Time.deltaTime;
            if (Physics.SphereCast(ray, 0.75f, out hit))
            {
                GameObject objectHit = hit.transform.gameObject;
                if (objectHit.GetComponent<PlayerInfo>())
                {
                    if (fireRate <= 0)
                    {
                        _bullet = Instantiate(bulletPB) as GameObject;
                      //  _bullet.transform.position = transform.TransformPoint(Vector3.forward * 1.5f);
                        _bullet.transform.position = firePoint.position;
                     /*   _bullet.transform.rotation =
                            Quaternion.LookRotation(player.transform.position - transform.position);*/
                     Vector3 rotation = _bullet.transform.rotation.eulerAngles;
                            _bullet.transform.rotation = Quaternion.Euler(rotation.x, transform.eulerAngles.y, rotation.z);
                        
                            _bullet.GetComponent<Rigidbody>().AddForce(firePoint.forward * 30.0f, ForceMode.Impulse);
                        fireRate = 2.5f;
                    }
                   /* else if (hit.distance < targetDistance)
                    {
                        float angle = UnityEngine.Random.Range(-110, 110);
                        transform.Rotate(0, angle, 0);
                    }*/
                }
            }
            //update
        }
    }
}
