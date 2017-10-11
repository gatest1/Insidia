using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionSquad : MonoBehaviour {
    public int teamNum = 0;

    public List<Minion> minions = new List<Minion>();
    public int capacity = 10;
    public List<GameCharacter> targets;
    public TriggerSensor targetSensor;
    public FormationManager formation;
    public float range = 20f;

    private void Start()
    {
        targetSensor.Filter = CheckTarget;
    }

    protected virtual bool CheckTarget(GameObject other)
    {
        //check if it's on another team
        Minion otherMinion = other.GetComponent<Minion>();
        if (otherMinion)
        {
            return otherMinion.Squad.teamNum != this.teamNum;
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

        StartCoroutine(this.UpdateCoroutine(10f, SquadUpdate));
    }

    private List<Minion> GetMinionsInState(Minion.State state)
    {
        return minions.FindAll(minion => minion.state == state);
    }

    private void SquadUpdate()
    {
        formation.SetMinionGoalsToPositions(GetMinionsInState(Minion.State.Follow));
    }
}
