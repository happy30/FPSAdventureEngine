using System.Collections;
using EventObjects;

using UnityEngine;

public class HoldComponent : MonoBehaviour, IInteractComponent
{
    private BaseInteractiveObject _baseInteractiveObject;
    public float FollowSpeed = 10f;

    [Range(0.1f,3)]
    public float HoldOffsetZ = 1.6f;
    
    [Range(-1,1)]
    public float HoldOffsetY = 0.2f;

    [Range(0, 1)]
    public float MovementSpeedReduction;
    
    private bool active;
    private Rigidbody _rb;
    private bool _isColliding;

    private bool canDrop;

    private Transform Object;

    public bool IsParcel;
    public BoolWithEvent GrabbingParcel;


    

    void Awake()
    {
        _baseInteractiveObject = GetComponent<BaseInteractiveObject>();
        if (GetComponent<Rigidbody>())
        {
            _rb = GetComponent<Rigidbody>();
            Object = transform;
        }
        else if (GetComponentInChildren<Rigidbody>())
        {
            _rb = GetComponentInChildren<Rigidbody>();
            Object = transform.GetChild(0);
        }
        else
        {
            Debug.LogError("No Rigidbody on Object", this);
        }
        _rb.detectCollisions = true;
    }


    void Update()
    {
        OnUpdate();
    }

    public void OnUpdate()
    {
        if (!active) return;
        MoveObject();

        if (canDrop)
        {
            CheckForRelease();
        }
        
    }


    void MoveObject()
    {
        //_rb.position = _baseInteractiveObject.HoldTransform.position;

        Object.position = Vector3.Lerp(Object.position, _baseInteractiveObject.HoldTransform.position, FollowSpeed * Time.deltaTime);

        
        
        _rb.velocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;
        Object.rotation = _baseInteractiveObject.HoldTransform.rotation;
    }

    void CheckForRelease()
    {
        if (Input.GetButtonDown("Fire2") && _baseInteractiveObject.GrabbingObject.Value && !_baseInteractiveObject.InInspectMode.Value)
        {
            _baseInteractiveObject.Release();
        }
        
        
        if (!_baseInteractiveObject.GrabbingObject.Value)
        {
            _baseInteractiveObject.Release();
        }
    }
    
    
    public void OnActivate()
    {
        active = true;
        _rb.useGravity = false;
        _baseInteractiveObject.MovementSpeedReductionObject.Value = MovementSpeedReduction;

        if (IsParcel)
        {
            GrabbingParcel.Value = true;
        }


        var colliders = GetComponents<Collider>();
        var childColliders = GetComponentsInChildren<Collider>();

        foreach (Collider col in colliders)
        {
            col.enabled = false;
        }

        foreach (Collider col in childColliders)
        {
            col.enabled = false;
        }
        
        StartCoroutine(DropDelay());
        

    }

    public void OnDeactivate()
    {
        active = false;
        _rb.useGravity = true;
        _rb.isKinematic = false;
        _baseInteractiveObject.MovementSpeedReductionObject.Value = 0;
        
        if (IsParcel)
        {
            GrabbingParcel.Value = false;
        }
        
        var colliders = GetComponents<Collider>();
        var childColliders = GetComponentsInChildren<Collider>();

        foreach (Collider col in colliders)
        {
            col.enabled = true;
        }

        foreach (Collider col in childColliders)
        {
            col.enabled = true;
        }
        
        
        canDrop = false;
    }

    void OnCollisionEnter()
    {
        _isColliding = true;
    }

    void OnCollisionExit()
    {
        _isColliding = false;
    }

    IEnumerator DropDelay()
    {
        yield return new WaitForSeconds(0.1f);
        canDrop = true;
    }
}
