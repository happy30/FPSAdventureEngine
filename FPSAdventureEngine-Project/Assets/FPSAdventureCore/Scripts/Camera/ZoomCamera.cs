
using EventObjects;
using UnityEngine;
using Klak.Math;

public class ZoomCamera : MonoBehaviour
{
    public Camera MainCamera;
    public BoolWithEvent GrabbingObject;
    
    [Range(0.5f,5)]
    public float ZoomSpeed;
    
    [Range(40,60)]
    public float FieldOfViewZoom;
    
    private float _fieldOfViewNormal;
    private float _targetFieldOfView;

    private bool canZoom;
    
    

    void Awake()
    {
        _fieldOfViewNormal = MainCamera.fieldOfView;
        _targetFieldOfView = _fieldOfViewNormal;
    }

    void Update()
    {
        if (GrabbingObject.Value)
        {
            canZoom = false;
            _targetFieldOfView = _fieldOfViewNormal;
        }


        if (Input.GetButtonUp("Fire2"))
        {
            canZoom = true;
        }
        
        if (canZoom)
        {
            SetFieldOfView();
        }
        
        MainCamera.fieldOfView = ETween.Step(MainCamera.fieldOfView, _targetFieldOfView, ZoomSpeed);
        
        
    }
    
    void SetFieldOfView()
    {
        if (Input.GetButton("Fire2"))
        {
            _targetFieldOfView = FieldOfViewZoom;
        }
        else
        {
            _targetFieldOfView = _fieldOfViewNormal;
            
        }
        
    }
}
