using System.Collections;
using System.Collections.Generic;
using EventObjects;
using UnityEngine;

public class InspectModeController : MonoBehaviour
{
    public BoolWithEvent InInspectMode;

    public GameObject InInspectView;
    public BoolWithEvent CanEnterInspectMode;
    public GameObjectWithEvent GameObjectWithEvent;

    void Awake()
    {
        CanEnterInspectMode.Value = false;
        InInspectMode.Value = false;
        GameObjectWithEvent.Init();
    }
    
    void Update()
    {
        InInspectView.SetActive(InInspectMode.Value);
    }

    public void Close()
    {
        InInspectMode.Value = false;
        CanEnterInspectMode.Value = true;
    }
}
