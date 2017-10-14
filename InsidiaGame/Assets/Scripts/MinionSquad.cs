﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionSquad : MonoBehaviour, ISensorListener {
    public int teamNum = 0;

    public List<Minion> minions = new List<Minion>();
    public int capacity = 10;
    public List<GameCharacter> targets;
    public TriggerSensor2 targetSensor;
    public SquadFormation formation;
    public float range = 20f;

    private void Start()
    {
        targetSensor.Setup(this, TargetFilter);
    }

    protected virtual bool TargetFilter(GameObject other)
    {
        if (Vector3.Distance(transform.position, other.transform.position) > range)
            return false;

        //check if it's on another team
        Minion otherMinion = other.GetComponent<Minion>();
        if (otherMinion)
        {
            return (otherMinion.Squad != null) && (otherMinion.Squad.teamNum != this.teamNum);
        }
        else if (other.tag == "Player")
        {
            MinionSquad otherSquad = other.GetComponent<MinionSquad>();
            return (otherSquad && otherSquad.teamNum != this.teamNum);
        }
        return false;
    }

    private void OnEnable()
    {
        foreach(var minion in minions)
        {
            minion.ChangeSquad(this);
        }

        StartCoroutine(this.UpdateCoroutine(30f, SquadUpdate));
    }

    private List<Minion> GetMinionsInState(Minion.State state)
    {
        return minions.FindAll(minion => minion.state == state);
    }

    private void SquadUpdate()
    {
        formation.SetMinionGoalsToPositions(transform, GetMinionsInState(Minion.State.Follow));
    }

    public void OnSensorEnter(TriggerSensor2 sensor, GameObject other)
    {
        GameCharacter gameCharacter = other.GetComponent<GameCharacter>();
        if (gameCharacter)
            targets.Add(gameCharacter);
    }

    public void OnSensorExit(TriggerSensor2 sensor, GameObject other)
    {
        GameCharacter gameCharacter = other.GetComponent<GameCharacter>();
        if (gameCharacter)
            targets.Remove(gameCharacter);
    }
}
