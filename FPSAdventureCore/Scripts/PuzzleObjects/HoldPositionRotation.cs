using System.Collections;
using System.Collections.Generic;
using Klak.Math;
using UnityEngine;

public class HoldPositionRotation : MonoBehaviour
{
    public Transform HoldTransform;
    public float FollowSpeed;

    void Update()
    {
        transform.position = ETween.Step(transform.position, HoldTransform.position, FollowSpeed);
    }
}
