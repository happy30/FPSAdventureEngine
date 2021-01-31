using System.Collections;
using System.Collections.Generic;
using EventObjects;
using UnityEngine;

[RequireComponent(typeof(HoldComponent))]
public class InspectModeComponent : MonoBehaviour, IInteractComponent
{
    public GameObjectWithEvent InspectedObjectEvent;
    //public InspectModeComponentWithEvent InspectModeComponentWithEvent;
    public BoolWithEvent InDialogue;
    public BoolWithEvent CanEnterInspectMode;
    public bool InspectTextOnce;
    private bool _inspected;
    
    public string[] Lines;
    
    
    public GameObject InspectObject;

    private BaseInteractiveObject _baseInteractiveObject;

    private Renderer[] rends;
    private Renderer[] childRends;

    [Header("Optional Point of interest set value true")]
    public BoolWithEvent PointOfInterest;

    public BoolWithEvent SetBoolFalse;

    

    private bool Active;

    void Awake()
    {
        _baseInteractiveObject = GetComponent<BaseInteractiveObject>();
        rends = GetComponents<Renderer>();
        childRends = GetComponentsInChildren<Renderer>();
    }

    public void OnActivate()
    {
        Active = true;
        CanEnterInspectMode.Value = true;

    }

    private void Update()
    {
        OnUpdate();
    }

    public void OnUpdate()
    {
        if (Input.GetKeyDown(KeyCode.F) && _baseInteractiveObject.GrabbingObject.Value && Active && !_baseInteractiveObject.InInspectMode.Value)
        {
            EnterInspectMode();
        }

        if (!_baseInteractiveObject.InInspectMode.Value)
        {
            if (rends.Length > 0)
            {
                if (!rends[0].enabled)
                {
                    foreach (var rend in rends)
                    {
                        rend.enabled = true;
                    }
                }

            }
            if (childRends.Length > 0)
            {
                if (!childRends[0].enabled)
                {
                    foreach (var rend in childRends)
                    {
                        rend.enabled = true;
                    }
                }

            }
        }
    }

    public void EnterInspectMode()
    {
        if (SetBoolFalse) SetBoolFalse.Value = false;
        
        InspectedObjectEvent.Value = InspectObject;
        _baseInteractiveObject.InInspectMode.Value = true;

        CanEnterInspectMode.Value = false;

        foreach (var rend in rends)
        {
            rend.enabled = false;
        }
            
        foreach (var rend in childRends)
        {
            rend.enabled = false;
        }
            
        if (InspectTextOnce)
        {
            if (_inspected) return;
        }
        //InspectModeComponentWithEvent.Value = this;
        InDialogue.Value = true;
    }
    
    public void OnTextDeactivate()
    {
        if (PointOfInterest != null)
        {
            PointOfInterest.Value = true;
        }
        _inspected = true;
        //InspectModeComponentWithEvent.Value = null;
        StartCoroutine(DelayBool());
        


    }

    public IEnumerator DelayBool()
    {
        yield return new WaitForSeconds(0.1f);
        InDialogue.Value = false;
    }

    public void OnDeactivate()
    {
        Active = false;
        _baseInteractiveObject.InInspectMode.Value = false;
        InspectedObjectEvent.Value = null;
        CanEnterInspectMode.Value = false;
    }

}
