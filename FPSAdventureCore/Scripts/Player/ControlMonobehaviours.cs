using System.Collections;
using System.Collections.Generic;
using EventObjects;
using UnityEngine;

public class ControlMonobehaviours : MonoBehaviour
{
    public BoolWithEvent InSettings;
    
    private bool setting;

    private bool disable;


    private bool valueChanged;

    public List<MonoBehaviour> MonoBehaviours = new List<MonoBehaviour>();

    private void Awake()
    {
        InSettings.Value = false;
    }

    void Update()
    {
        disable = InSettings.Value;
        
        
        
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
    
    
    

