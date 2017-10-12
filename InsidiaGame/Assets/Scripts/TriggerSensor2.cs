using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class TriggerSensor2 : MonoBehaviour {

    private ISensorListener _listener;
    private MonoBehaviour _listenerBehaviour;
    public MonoBehaviour Listener { get { return _listenerBehaviour; } }

    List<GameObject> sensedObjects = new List<GameObject>();
    public Predicate<GameObject> Filter = (gameObject => true);
    public bool notifyIfInactive = false;
    
    
    public void Setup<T>(T listener) where T : MonoBehaviour, ISensorListener
    {
        Setup(listener, Filter);
    }

    public void Setup<T>(T listener, Predicate<GameObject> filter) where T : MonoBehaviour, ISensorListener
    {
        _listener = listener;
        _listenerBehaviour = listener;
        Filter = filter;
    }

    public void OnTriggerEnter(Collider target)
    {
        if (Filter(target.gameObject))
        {
            sensedObjects.Add(target.gameObject);
            if (_listener != null && (notifyIfInactive || _listenerBehaviour.enabled))
                _listener.OnSensorEnter(target.gameObject);
        }
    }


    public void OnTriggerExit(Collider target)
    {
        if (sensedObjects.Remove(target.gameObject))
            if (_listener != null && (notifyIfInactive || _listenerBehaviour.enabled))
                _listener.OnSensorExit(target.gameObject);
    }

}























    

