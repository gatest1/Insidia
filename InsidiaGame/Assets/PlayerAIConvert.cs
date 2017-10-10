using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAIConvert : MonoBehaviour {

    private GameObject player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.gameObject;
            StartCoroutine(ConvertNeutralAI());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            player = null;
            StopAllCoroutines();
        }
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