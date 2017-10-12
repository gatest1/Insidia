using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class TriggerSensor : MonoBehaviour {

    List<GameObject> sensedObjects = new List<GameObject>();
    public Func<GameObject, bool> Filter = (gameObject => true);
    public Action<TriggerSensor, GameObject> OnEnter;
    public Action<TriggerSensor, GameObject> OnExit;
    
    public void OnTriggerEnter(Collider target)
    {
        if (Filter(target.gameObject))
        {
            sensedObjects.Add(target.gameObject);
            if (OnEnter != null)
                OnEnter(this, target.gameObject);
        }
    }


    public void OnTriggerExit(Collider Target)
    {
        if (sensedObjects.Remove(Target.gameObject))
            if (OnExit != null)
                OnExit(this, Target.gameObject);
    }

}























    

