using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyFollow : MonoBehaviour {


    public float heroTargetDistance;
    public float enemyLookDistance;
    public float attackDistance;
    public float enemySpeed;
    public float damping;
    public Transform heroTarget;
    Rigidbody theRigidbody;
    Renderer myRender;
    public static Action follow;

    void Start()
    {
        myRender = GetComponent<Renderer>();
        theRigidbody = GetComponent<Rigidbody>();
        StartCoroutine(enemyFollowing());
    }

    IEnumerator enemyFollowing()
    {
        while (true)
        {
            heroTargetDistance = Vector3.Distance(heroTarget.position, transform.position);
            if (heroTargetDistance < enemyLookDistance)
            {
                lookAtPlayer();
                print("Look at the Player");
            }
            if (heroTargetDistance < attackDistance)
            {
                attackPlayer();
                print("ATTACK!");
            }
            yield return null;
        }
       
    }

    void lookAtPlayer()
    {
        Quaternion rotation = Quaternion.LookRotation(heroTarget.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);
    }

    void attackPlayer()
    {
        theRigidbody.AddForce(transform.forward * enemySpeed);
    }
}
