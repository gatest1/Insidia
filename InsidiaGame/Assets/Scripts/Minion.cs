using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[RequireComponent(typeof(AICharMovement))]
public class Minion : MonoBehaviour {

    public enum State { Follow, Attack, Regroup }
    public State state = State.Follow;
    public GameCharacter attackTarget = null;

    public MinionSquad Squad { get; private set; }
    private AICharMovement _charMovement;

    //Jacob Ressler
    public static Action<bool> Thing;
    private bool EnemyAlone = true;

    //public event Action<Minion, GameCharacter> Attacked;

    //private void OnTakeDamage()
    //{
    //    if (Attacked != null)
    //        Attacked(this, other)
    //}

    public void ChangeSquad(MinionSquad newSquad)
    {
        if (newSquad.minions.Count < newSquad.capacity)
        {
            //Leave current squad code
            if (Squad != null)
            {
                Squad.minions.Remove(this);
                //unsub delegates
            }

            //Join new squad code
            Squad = newSquad;
            Squad.minions.Add(this);
            //subscr to delegates
        }
    }

    public void Start()
    {
        _charMovement = GetComponent<AICharMovement>();
        Thing += canAttackPlayer;
    }

    public void OnEnable()
    {
        StartCoroutine(this.UpdateCoroutine(20f, MinionUpdate));
    }

    public void OnDisable()
    {
        StopAllCoroutines();
    }

    public void MinionUpdate()
    {
        switch (state)
        {
            case State.Attack:
                AttackUpdate();
                break;
            case State.Follow:
                FollowUpdate();
                break;
            case State.Regroup:
            default:
                RegroupUpdate();
                break;
        }
    }

    public void AttackUpdate()
    {
        //Check if out of squad range, if so change to Regroup state and return;

        //Do all attack logic
        //Find new best target?

        //set goal to attack target
    }

    public void FollowUpdate()
    {
        //Check if out of squad range, if so change to Regroup state and return;

        //if can find something to attack, go to attack state and set that thing as our target

        //Don't set any movement goals here, it's being managed by the Squad.
    }

    private GameObject GetAttackTarget()
    {
        if (Squad.targets.Count > 0)
        {
            return Squad.targets[0].gameObject;
        }

        return null;
    }

    public void RegroupUpdate()
    {
        if (Vector3.Distance(transform.position, Squad.transform.position) < Squad.range)
        {
            state = State.Follow;
        }
        else
        {
            _charMovement.Goal = Squad.transform.position;
        }
    }

    //Jacob Ressler
    //from attackMinion Script
    private void OnTriggerStay(Collider other)
    {
        PlayerSquadManager.alone();
        if (EnemyAlone)
        {
            attackEnemyPlayer();
        }
        else
        {
            attackEnemyMinion();
        }
    }

    void attackEnemyPlayer()
    {
        print("Attacked");

    }
    void attackEnemyMinion()
    {

    }

    void canAttackPlayer(bool t)
    {
        EnemyAlone = t;
    }
}
