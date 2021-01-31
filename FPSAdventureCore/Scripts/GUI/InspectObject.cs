using System.Collections;
using System.Collections.Generic;
using EventObjects;
using TMPro;
using UnityEngine;

public class InspectObject : MonoBehaviour
{
    /*
   // public InspectComponentWithEvent InspectComponentWithEvent;
   // public InspectModeComponentWithEvent InspectModeComponentWithEvent;
   // public InspectModeInteractionPointWithEvent InspectModeInteractionPoint;

    
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
        if (InspectComponentWithEvent.Value != null)
        {
            if (!_setLine)
            {
                _setLine = true;
                fullLine = InspectComponentWithEvent.Value.Lines[currentLine];
                StartCoroutine(FillLine(fullLine, Text, textSpeed));
                
            }
        }
        else if(InspectModeComponentWithEvent.Value == null && InspectModeInteractionPoint.Value == null)
        {
            currentLine = 0;
            Text.text = "";
        }

        if (InspectComponentWithEvent.Value != null)
        {
            if (_setLine && InspectLineCompleted && Input.GetButtonDown("Fire1"))
            {
                if (currentLine > InspectComponentWithEvent.Value.Lines.Length - 1)
                {
                    InspectComponentWithEvent.Value.OnDeactivate();
                    StartCoroutine(DeactivateAfterDelay());
                }
                else
                {
                    _setLine = false;
                }
            }
        }
        
        
        
        if (InspectModeComponentWithEvent.Value != null)
        {
            if (!_setLine)
            {
                _setLine = true;
                fullLine = InspectModeComponentWithEvent.Value.Lines[currentLine];
                StartCoroutine(FillLine(fullLine, Text, textSpeed));
                
            }
        }
        else if (InspectComponentWithEvent.Value == null && InspectModeInteractionPoint.Value == null)
        {
            currentLine = 0;
            Text.text = "";
        }

        if (InspectModeComponentWithEvent.Value != null)
        {
            if (_setLine && InspectLineCompleted && Input.GetButtonDown("Fire1"))
            {
                if (currentLine > InspectModeComponentWithEvent.Value.Lines.Length - 1)
                {
                    currentLine = 0;
                    Text.text = "";
                    InspectModeComponentWithEvent.Value.OnTextDeactivate();
                }

            
                    _setLine = false;
                
            }
        }
        
        if (InspectModeInteractionPoint.Value != null)
        {
            if (!_setLine)
            {
                _setLine = true;
                fullLine = InspectModeInteractionPoint.Value.Lines[currentLine];
                StartCoroutine(FillLine(fullLine, Text, textSpeed));
                
            }
        }
        else if (InspectComponentWithEvent.Value == null && InspectModeComponentWithEvent.Value == null)
        {
            currentLine = 0;
            Text.text = "";
        }

        if (InspectModeInteractionPoint.Value != null)
        {
            if (_setLine && InspectLineCompleted && Input.GetButtonDown("Fire1"))
            {
                if (currentLine > InspectModeInteractionPoint.Value.Lines.Length - 1)
                {
                    currentLine = 0;
                    Text.text = "";
                    InspectModeInteractionPoint.Value.OnTextDeactivate();
                }

                _setLine = false;
                
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
    */

}
