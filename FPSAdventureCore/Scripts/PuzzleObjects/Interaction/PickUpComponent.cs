using System.Collections;
using System.Collections.Generic;
using EventObjects;
using UnityEngine;

public class PickUpComponent : MonoBehaviour, IInteractComponent
{
    public BoolWithEvent ItemBoolean;
    public GameObject MeshGameObject;

    private InspectModeComponent _inspectModeComponent;

    void Awake()
    {
        if (GetComponent<InspectModeComponent>())
        {
            _inspectModeComponent = GetComponent<InspectModeComponent>();
        }
    }
    
    public void OnActivate()
    {
        if (ItemBoolean != null) ItemBoolean.Value = true;
        MeshGameObject.SetActive(false);

        if (_inspectModeComponent != null)
        {
            _inspectModeComponent.EnterInspectMode();
        }
    }

    public void OnUpdate()
    {
        
    }

    public void OnDeactivate()
    {
        
    }
}
