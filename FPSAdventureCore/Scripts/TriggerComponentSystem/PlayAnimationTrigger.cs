using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayAnimationTrigger
{
    [SerializeField] public Animator Animator;
    [SerializeField]public string TriggerString;
    [SerializeField]public string Bool;
    [SerializeField]public bool SetBool;
    [SerializeField]public float Delay;
    
}
