using System.Collections;
using System.Collections.Generic;
using EventObjects;
using UnityEngine;

public class ConstrainHoldPosition : MonoBehaviour
{
   public float zOffsetCamera;
   public float yOffsetCamera;
   public Transform Camera;


  

   void Update()
   {
      transform.position = Camera.position + (Camera.forward * zOffsetCamera) + (Camera.up * yOffsetCamera);
   }
 
   
}
