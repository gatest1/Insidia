using UnityEngine;
using System;
using System.Collections;

public interface ISensorListener
{
    void OnSensorEnter(GameObject other);

    void OnSensorExit(GameObject other);
}
