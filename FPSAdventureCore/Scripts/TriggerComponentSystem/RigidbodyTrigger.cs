using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RigidbodyTrigger
{
    public Rigidbody Rigidbody;
    
    public float Mass;
    public float Drag;
    public bool UseGravity;
    public Bool3 PositionConstraints;
    public Bool3 RotationConstraints;
    
    

    public float Delay;


}

[System.Serializable]
public struct Bool3
{
    public bool x, y, z;
}