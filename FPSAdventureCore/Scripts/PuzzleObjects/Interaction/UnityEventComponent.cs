using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UnityEventComponent : MonoBehaviour, IInteractComponent
{
    public UnityEvent Event;
    
    public void OnActivate()
    {
        Event.Invoke();
    }

    public void OnUpdate()
    {
        
    }

    public void OnDeactivate()
    {
        
    }
}
