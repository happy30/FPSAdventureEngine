using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TeleportTrigger
{
    public Transform TeleportedObject;
    public Transform TargetLocation;
    public Vector3 RelativeLocation;
    public bool Relative;

    public float Delay;
    public bool CopyRotation;
}
