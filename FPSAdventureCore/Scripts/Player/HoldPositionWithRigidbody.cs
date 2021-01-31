
using EventObjects;
using UnityEngine;

public class HoldPositionWithRigidbody : MonoBehaviour
{
    public Transform HoldTransform;
    public Transform Player;
    public float FollowSpeed;
    public float MaxDistance;
    private bool _isColliding;
    public BoolWithEvent GrabbingObject;
    public Collider Collider;
    private Rigidbody _rb;
    private bool _setRotation;
    private Quaternion _rotationOffset;
    private Quaternion _objectRot;
    private Quaternion _inverseRot;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (GrabbingObject.Value)
        {
            Collider.enabled = true;
            if (!_setRotation)
            {
                _inverseRot = Quaternion.Inverse(Player.rotation);
                _objectRot = transform.rotation;
                _setRotation = true;
            }

            var deltaRotation = Player.rotation * _inverseRot;
            var rot = deltaRotation * _objectRot;
            transform.rotation = rot;

            //var relativePos = transform.position - Player.transform.position;
        }
        else
        {
            Collider.enabled = false;
            _setRotation = false;
            _isColliding = false;
        }

        _rb.velocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;
        var slowSpeed = 2f;
        var speed = _isColliding ? slowSpeed : FollowSpeed;
        transform.position = Vector3.Lerp(transform.position, HoldTransform.position, speed * Time.deltaTime);
        var distance = Vector3.Distance(transform.position, HoldTransform.position);
        if (distance >= MaxDistance && _isColliding) GrabbingObject.SetValue(false);
        if (transform.position.y < Player.transform.position.y - 1.1f) GrabbingObject.SetValue(false);
    }

    private void OnCollisionEnter()
    {
        _isColliding = true;
    }

    private void OnCollisionExit()
    {
        _isColliding = false;
    }
}
