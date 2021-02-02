using System.Collections;
using System.Collections.Generic;
using EventObjects;
using TMPro;
using UnityEngine;

public class InspectObject : MonoBehaviour
{
    
    public InspectComponentWithEvent InspectComponent;


    public TextMeshProUGUI Text;
    private string fullLine;
    private float textSpeed = 0.013f;

    public bool InspectLineCompleted;

    private bool _setLine;

    private int currentLine;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (InspectComponent.Value != null)
        {
            if (!_setLine)
            {
                _setLine = true;
                fullLine = InspectComponent.Value.Lines[currentLine];
                StartCoroutine(FillLine(fullLine, Text, textSpeed));
                
            }
        }
        else 
        {
            currentLine = 0;
            Text.text = "";
        }

        if (InspectComponent.Value != null)
        {
            if (_setLine && InspectLineCompleted && Input.GetButtonDown("Fire1"))
            {
                if (currentLine > InspectComponent.Value.Lines.Length - 1)
                {
                    InspectComponent.Value.OnDeactivate();
                    StartCoroutine(DeactivateAfterDelay());
                }
                else
                {
                    _setLine = false;
                }
            }
        }
        
        
    }
    
    
    IEnumerator FillLine(string fullLine, TextMeshProUGUI displayLine, float textSpeed)
    {
        InspectLineCompleted = false;
        displayLine.text = "";
        for (int i = 0; i < fullLine.Length; i++)
        {
            displayLine.text += fullLine[i];
            yield return new WaitForSeconds(textSpeed);
        }

        currentLine++;
        InspectLineCompleted = true;
    }


   
    
    IEnumerator DeactivateAfterDelay()
    {
        yield return new WaitForSeconds(0.1f);
        currentLine = 0;
        Text.text = "";
        
        _setLine = false;
    }
    

}
