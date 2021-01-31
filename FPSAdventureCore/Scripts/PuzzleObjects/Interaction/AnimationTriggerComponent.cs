using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTriggerComponent : MonoBehaviour, IInteractComponent
{
    public Animator Animator;
    public string Trigger;
    public string Bool;

    
    
    
    public void OnActivate()
    {
        if (!enabled) return;
        
        if (Trigger != "")
        {
            Animator.SetTrigger(Trigger);
        }

        if (Bool != "")
        {
            Animator.SetBool(Bool, !Animator.GetBool(Bool));
        }
    }

    public void OnUpdate()
    {
        
    }

    public void OnDeactivate()
    {
       
    }
}
