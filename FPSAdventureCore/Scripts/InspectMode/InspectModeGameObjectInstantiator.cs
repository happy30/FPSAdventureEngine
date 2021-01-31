using System.Collections;
using System.Collections.Generic;
using EventObjects;
using UnityEngine;

public class InspectModeGameObjectInstantiator : MonoBehaviour
{
    public GameObjectWithEvent InspectModeGameObject;
    InstantiateInspectObject spawnedObject;


    void OnEnable()
    {
        if (InspectModeGameObject.Value != null)
        {
            spawnedObject = InspectModeGameObject.Value.GetComponent<InstantiateInspectObject>().Copy(this.transform);
        }
        else
        {
            Debug.Log(" iots null;");
        }
            
            
    }

    private void OnDisable()
    {
        if (spawnedObject != null)
        {
            spawnedObject.DestroyObject();
        }

    }
}
