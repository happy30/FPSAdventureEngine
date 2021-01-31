using System.Collections;
using System.Collections.Generic;
using EventObjects;
using UnityEngine;

public class ControlMonobehaviours : MonoBehaviour
{
    public BoolWithEvent InSettings;
    public BoolWithEvent InInspectMode;
    
    private bool setting;

    private bool disable;


    private bool valueChanged;

    public List<MonoBehaviour> MonoBehaviours = new List<MonoBehaviour>();

    private void Awake()
    {
        InSettings.Value = false;
        InInspectMode.Value = false;
    }

    void Update()
    {
        disable = InSettings.Value || InInspectMode.Value;
        
        
        
        if (disable != setting)
        {
            foreach (var beh in MonoBehaviours)
            {
                beh.enabled = !disable;
                setting = disable;
            }
        }

    }
}
    
    
    

