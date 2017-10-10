/*Brad beachell

how to use. 
first you will need to put this on an object with a desired radius. Then turn off the mesh renderer.
You will need a collider on the minion that can be disabled when the count is completed. 

*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class CountDownScript : MonoBehaviour {

    public static Action Shoot;
    public int AllyConverted = 10;
    public int BaseNumber = 0;

    public List<GameObject> AllyDinosaurs = new List<GameObject>();


    public void OnTriggerEnter(Collider minion)
    {
        Collider Minion = minion;
        StopAllCoroutines();       
           StartCoroutine(AllyChange(Minion));
       
    }

    public void OnTriggerExit()
    {
            StopAllCoroutines();
            //StopCoroutine(AllyChange());         
            if (BaseNumber > 0)
            {
                StartCoroutine(CoolDown());
            }
    }

   
    private IEnumerator CoolDown()
    {
       while (true)
        {            
            if (BaseNumber > 0)
            {
               // IsAlly = true;
                BaseNumber--;
                print(BaseNumber + " I'm not your ally");
            }
            yield return new WaitForSeconds(1);
        }
    }
      

  
    private IEnumerator AllyChange(Collider minion)
    {
        while (true)
        {            
            if (BaseNumber < AllyConverted)
            {
                BaseNumber++;
                print(BaseNumber + " becoming an ally");               
            }
            else StopAllCoroutines();
            yield return new WaitForSeconds(1);
            if (BaseNumber == AllyConverted)
            {
                AllyDinosaurs.Add(minion.gameObject);
                print("a minion has been added");
                // minion.gameObject.SetActive(false);
                minion.enabled = false;
                BaseNumber = 0;
                StopAllCoroutines();
                //this is where you would call the follow script. In the follow script disable this script. 
            }
        }
     
        
        
       
     

    }


    
}


