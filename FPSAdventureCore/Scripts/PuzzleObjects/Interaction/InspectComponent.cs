using System.Collections;
using EventObjects;

using UnityEngine;
using UnityEngine.Events;

public class InspectComponent : MonoBehaviour, IInteractComponent
{
    [HideInInspector]
    public BoolWithEvent InDialogue;
    
    [HideInInspector]
    public InspectComponentWithEvent InspectComponentWithEvent;
    
    
    public bool InspectOnce;
    private bool _inspected;
    
    public string[] Lines;

    [Header("Optional mark point of interest as seen")]
    public BoolWithEvent PointOfInterest;

    public UnityEvent EventAfterInspect;


    private bool _activated;

    void Awake()
    {
        InDialogue = (BoolWithEvent) Resources.Load("EventObjects/InDialogue");
        InspectComponentWithEvent = (InspectComponentWithEvent) Resources.Load("EventObjects/InspectComponent");
    }

    public void OnUpdate()
    {
    
    }
    
    public void OnActivate()
    {
        if (!enabled) return;
        
        if (InspectOnce)
        {
            if (_inspected) return;
        }
        InspectComponentWithEvent.Value = this;
        InDialogue.Value = true;
        
    }
    
    public void OnDeactivate()
    {
        if (!enabled) return;

        if (EventAfterInspect != null)
        {
            if (InspectOnce)
            {
                if (!_inspected)
                {
                    EventAfterInspect.Invoke();
                }
            }
            else
            {
                EventAfterInspect.Invoke();
            }
            
        }

        StartCoroutine(DelayBool());


    }

    public IEnumerator DelayBool()
    {
        yield return new WaitForSeconds(0.1f);
        InDialogue.Value = false;
        
        if (PointOfInterest != null)
        {
            PointOfInterest.Value = true;
        }

        _inspected = true;
        InspectComponentWithEvent.Value = null;
    }
}
