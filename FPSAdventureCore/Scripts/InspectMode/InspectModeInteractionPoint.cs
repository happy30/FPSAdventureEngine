using System.Collections;
using System.Collections.Generic;
using EventObjects;
using UnityEngine;

public class InspectModeInteractionPoint : MonoBehaviour
{
    public BoolWithEvent MouseOverInspectPoint;
    private BaseInteractiveObject _baseInteractiveObject;
    public InspectModeInteractionPointWithEvent _InspectModeInteractionPointEvent;
    public BoolWithEvent InDialogue;

    public string[] Lines;
    public Vector3 OffsetChange;
    public SceneEvent EventOnPress;

    [Header("Optional set point of interest true")]
    public BoolWithEvent PointOfInterest;



    
    void OnMouseOver()
    {
        MouseOverInspectPoint.Value = true;
    }

    private void OnMouseExit()
    {
        MouseOverInspectPoint.Value = false;
    }

    private void OnMouseDown()
    {
        _InspectModeInteractionPointEvent.Value = this;
        InDialogue.Value = true;

        if (EventOnPress != null)
        {
            EventOnPress.Raise();
        }
        

    }
    
    public void OnTextDeactivate()
    {
        _InspectModeInteractionPointEvent.Value = null;
        if (PointOfInterest != null)
        {
            PointOfInterest.Value = true;
        }
        StartCoroutine(DelayBool());
    }

    public IEnumerator DelayBool()
    {
        yield return new WaitForSeconds(0.1f);
        InDialogue.Value = false;
        Cursor.visible = true;
    }
}
