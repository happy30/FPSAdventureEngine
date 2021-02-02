using System.Collections;
using System.Collections.Generic;
using EventObjects;
using UnityEngine;

public class BaseInteractiveObject : MonoBehaviour
{
    public bool Enabled = true;
    
    Transform _holdTransform;
    Transform _camera;

    

    public Transform HoldTransform => _holdTransform;
    public Transform Camera => _camera;

    [HideInInspector]
    public InteractComponents InteractComponentsList;

    public CursorImagesList Cursor;

    private IInteractComponent[] _manipulators;
    
    [HideInInspector]
    public BoolWithEvent GrabbingObject;

    [HideInInspector] public FloatWithEvent MovementSpeedReductionObject;
    
    public Sprite InteractSprite;
    public bool Once;
    

    void Awake()
    {
        var components = GetComponents<IInteractComponent>();
        _manipulators = components;
        GrabbingObject = (BoolWithEvent) Resources.Load("GrabbingObject");
        MovementSpeedReductionObject = (FloatWithEvent) Resources.Load("EventObjects/MovementReduction");

        if (transform.localScale != Vector3.one)
        {
            if (transform.GetChild(0) != null)
            {
                transform.GetChild(0).localScale = transform.localScale;
                transform.localScale = Vector3.one;
                Debug.LogWarning("Please set the scale of this Interactive Object to (1,1,1)!", transform);
            }
        }
    }

    public void Interact(Transform holdTransform, Transform cam)
    {
        _holdTransform = holdTransform;
        _camera = cam;
        
        foreach (IInteractComponent component in _manipulators)
        {
            component.OnActivate();
        }
        
        if(Once) Enabled = false;
    }

    public void Interact()
    {
        foreach (IInteractComponent component in _manipulators)
        {
            component.OnActivate();
        }
    }

    public void Release()
    {
        
        GrabbingObject.SetValue(false);
        foreach (IInteractComponent component in _manipulators)
        {
            component.OnDeactivate();
        }

        Enabled = false;
        
        if(!Once)StartCoroutine(DelayBeforeEnabled());
    }

    private IEnumerator DelayBeforeEnabled()
    {
        yield return new WaitForSeconds(1);
        Enabled = true;
    }

    public void SetEnabled()
    {
        Enabled = true;
    }
}
