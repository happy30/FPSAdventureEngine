using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldPositionRotation : MonoBehaviour
{
    public Transform HoldTransform;
    public float FollowSpeed;

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, HoldTransform.position, FollowSpeed * Time.deltaTime);
    }
}
