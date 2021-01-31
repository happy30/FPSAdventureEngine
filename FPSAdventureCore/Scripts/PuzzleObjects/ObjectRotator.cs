using UnityEngine;
using System.Collections;
using EventObjects;

public class ObjectRotator : MonoBehaviour
{

//    public InspectModeComponentWithEvent InspectModeComponentWithEvent;
    public BoolWithEvent MouseOver;
    
    private float _sensitivity;
    private Vector3 _mouseReference;
    private Vector3 _mouseOffset;
    private bool _isRotating;


    private float XaxisRotation;
    private float YaxisRotation;

    private bool MouseDown;
    
    void Start ()
    {
        _sensitivity = 0.15f;
    }


    private void OnMouseOver()
    {
        MouseOver.Value = true;
    }

    private void OnMouseExit()
    {
        MouseOver.Value = false;
    }

    void OnMouseDrag()
    {
        //if (InspectModeComponentWithEvent.Value != null) return;
        
        XaxisRotation = Input.GetAxis("Mouse X")*_sensitivity;
        YaxisRotation = Input.GetAxis("Mouse Y")*_sensitivity;

        transform.RotateAround (Vector3.down, XaxisRotation);
        transform.RotateAround (Vector3.right, YaxisRotation);
    }


    
    /*
    void Update()
    {
        if(_isRotating)
        {
            // offset
            _mouseOffset = (Input.mousePosition - _mouseReference);
             
            // apply rotation
            _rotation.y = -(_mouseOffset.x + _mouseOffset.y) * _sensitivity;
             
            // rotate
            transform.Rotate(_rotation);
             
            // store mouse
            _mouseReference = Input.mousePosition;
        }
    }
     
    void OnMouseDown()
    {
        // rotating flag
        _isRotating = true;
         
        // store mouse
        _mouseReference = Input.mousePosition;
    }
     
    void OnMouseUp()
    {
        // rotating flag
        _isRotating = false;
    }
    
    */
     
}