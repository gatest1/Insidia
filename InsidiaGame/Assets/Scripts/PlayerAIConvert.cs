using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAIConvert : MonoBehaviour {

    private GameObject player;
    //public TriggerSensor sensor;
    

    //private void Start()
    //{
    //    sensor.Filter = FilterForPlayer;
    //}

    private bool FilterForPlayer(GameObject other)
    {
        if (gameObject.tag == "Player")
        {
            MinionSquad squad = gameObject.GetComponent<MinionSquad>();
            return squad != null && squad.minions.Count < squad.capacity;
        }

        return false;
    }

    private void OnEnable()
    {
        //sensor.OnEnter += OnSensorEnter;
    }

    private void OnDisable()
    {
        //sensor.OnEnter -= OnSensorEnter;
    }

    private void OnSensorEnter(TriggerSensor sensor, GameObject other)
    {

    }

    private IEnumerator ConvertNeutralAI()
    {
        yield return new WaitForSeconds(3f);

        //Check to make sure Neutral AI is not in the attack state every half a second or so.

        Convert();
    }

    private void Convert()
    {
        gameObject.tag = "AI Friendly";

        gameObject.GetComponent<Renderer>().material.color = Color.green;

        GetComponent<DinoTeam>().teamNum = player.GetComponent<DinoTeam>().teamNum;

        //change AI's squad to our squad.

        GetComponent<AIBrainFollowPlayer>().enabled = true;

        this.enabled = false; // Disable script to stop converting.
    }
}