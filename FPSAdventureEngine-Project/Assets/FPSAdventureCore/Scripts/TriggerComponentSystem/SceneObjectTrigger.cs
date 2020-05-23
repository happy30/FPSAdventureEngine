using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SceneObjectTrigger
{
    public GameObject SceneObject;
    public bool SetObjectActive = true;
    public float Delay;
    public bool ToInitialStateAfterDuration;
    public float Duration;
}
