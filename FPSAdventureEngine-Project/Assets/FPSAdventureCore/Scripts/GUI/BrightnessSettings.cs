using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EventObjects;

public class BrightnessSettings : MonoBehaviour
{
    public Slider BrightnessSlider;
    public FloatWithEvent Brightness;
    public BoolWithEvent InSettings;

    private bool setSlider;

    public GameObject GameObject;

    private bool ResetInSettings;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (InSettings.Value)
            {
                Deactivate();
            }
            else
            {
                
                Activate();
            }
        }
        
        
    }

    public void ChangeBrightness()
    {
        Brightness.Value = BrightnessSlider.value;
    }


    public void Activate()
    {
        ResetInSettings = InSettings.Value;
        
        Cursor.lockState = CursorLockMode.None;
        GameObject.SetActive(true);
        BrightnessSlider.value = Brightness.Value;
        InSettings.Value = true;
        
    }

    public void Deactivate()
    {
        Cursor.lockState = CursorLockMode.Locked;
        GameObject.SetActive(false);
        InSettings.Value = ResetInSettings;
    }
    
    
}
