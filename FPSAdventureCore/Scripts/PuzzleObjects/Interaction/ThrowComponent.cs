using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using EventObjects;

using UnityEngine;

public class ThrowComponent : MonoBehaviour, IInteractComponent
{
    [HideInInspector]
    public BoolWithEvent InDialogue;

    private BaseInteractiveObject _baseInteractiveObject;
    
    
    public float ThrowPower = 5;
    public float ThrowTorgue = 3;
    private bool active;

    private Rigidbody _rb;

    void Awake()
    {
        InDialogue = (BoolWithEvent) Resources.Load("EventObjects/InDialogue");
        _baseInteractiveObject = GetComponent<BaseInteractiveObject>();
        
        if (GetComponent<Rigidbody>())
        {
            _rb = GetComponent<Rigidbody>();
        }
        else if (GetComponentInChildren<Rigidbody>())
        {
            _rb = GetComponentInChildren<Rigidbody>();
        }
        else
        {
            Debug.LogError("No Rigidbody on Object", this);
        }
        
    }

    
    
    public void OnActivate()
    {
        StartCoroutine(ActiveAfterFrame());
    }

    private void Update()
    {
        OnUpdate();
    }

    public void OnUpdate()
    {
        if (!active) return;
        
        if (Input.GetButtonDown("Fire1") && !InDialogue.Value)
        {
            StartCoroutine(ActivateAfterFrame());
        }
    }

    IEnumerator ActivateAfterFrame()
    {
        yield return new WaitForEndOfFrame();
        GetComponent<BaseInteractiveObject>().Release();
        _rb.velocity = GetComponent<BaseInteractiveObject>().Camera.forward * ThrowPower;


        ThrowTorgue = Random.value > 0.5f ? ThrowTorgue : -ThrowTorgue;
            
        _rb.AddTorque(new Vector3(ThrowTorgue, ThrowPower, ThrowTorgue));
    }

    IEnumerator ActiveAfterFrame()
    {
        yield return new WaitForSeconds(0.1f);
        active = true;
    }
    
   

    public void OnDeactivate()
    {
        active = false;
    }
}
