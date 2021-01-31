using System.Collections;
using System.Collections.Generic;
using EventObjects;
using UnityEngine;

[System.Serializable]
public class BoolWithEventTrigger
{
    [SerializeField] public BoolWithEvent Bool;
    [SerializeField] public bool NewState;
}
