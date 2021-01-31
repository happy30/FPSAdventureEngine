using System.Collections;
using System.Collections.Generic;
using EventObjects;
using UnityEngine;

public class ObjectInteractor : MonoBehaviour
{
   public LayerMask ObjectLayer;
   private GameObject GrabbedObject;
   public BoolWithEvent GrabbingObject;
   public BoolWithEvent RaycastingObject;
   public SpriteWithEvent CursorObject;
   public float RaycastLength;
   
   public Transform HoldTransform;
   public Transform Camera;

   public MeshCollider HoldTransformMeshCollider;

   public ConstrainHoldPosition ContrainHoldPosition;

   public BoolWithEvent InDialogue;

   

 


   void Start()
   {
      GrabbingObject.Value = false;
      QualitySettings.vSyncCount = 0;
   }
   
   void Update()
   {
      if (!GrabbingObject.Value && !InDialogue.Value)
      {
         CheckForObject(RaycastLength);
      }
      else
      {
         RaycastingObject.Value = false;
      }
   }


   private void CheckForObject(float range)
   {

      
      
      RaycastHit hit;
      //Ray ray = Camera.ScreenPointToRay (Input.mousePosition);
      if (Physics.Raycast(transform.position, transform.forward, out hit, range, ObjectLayer))
      {

         if (!CheckLayer(range))
         {
            return;
         }
            
         
         if (hit.collider.GetComponent<BaseInteractiveObject>())
         {
            var baseInteractiveObject = hit.collider.GetComponent<BaseInteractiveObject>();
            if (!baseInteractiveObject.Enabled)
            {
               RaycastingObject.Value = false;
               return;
            }

            CursorObject.Value = baseInteractiveObject.InteractSprite;
            RaycastingObject.Value = true;
            if (Input.GetMouseButtonDown(0))
            {
               baseInteractiveObject.Interact(HoldTransform, Camera);
               

               if (hit.collider.gameObject.GetComponent<HoldComponent>())
               {
                  GrabbingObject.Value = true;
                  if (hit.collider.gameObject.GetComponent<MeshFilter>())
                  {
                     HoldTransformMeshCollider.sharedMesh = hit.collider.gameObject.GetComponent<MeshFilter>().mesh;
                     HoldTransform.localScale = hit.collider.transform.localScale * 1.1f;
                     HoldTransform.rotation = hit.collider.gameObject.transform.rotation;
                  }
                  else if (hit.collider.gameObject.GetComponentInParent<MeshFilter>())
                  {
                     HoldTransformMeshCollider.sharedMesh =
                        hit.collider.gameObject.GetComponentInParent<MeshFilter>().mesh;
                     HoldTransform.localScale = hit.collider.transform.parent.localScale * 1.1f;
                     HoldTransform.rotation = hit.collider.gameObject.transform.parent.rotation;
                  }
                  
                  
                  ContrainHoldPosition.yOffsetCamera =
                     hit.collider.gameObject.GetComponent<HoldComponent>().HoldOffsetY;
                  ContrainHoldPosition.zOffsetCamera =
                     hit.collider.gameObject.GetComponent<HoldComponent>().HoldOffsetZ;

               }
            }


         }

         if (hit.collider.GetComponentInParent<BaseInteractiveObject>())
         {
            var baseInteractiveObject = hit.collider.GetComponentInParent<BaseInteractiveObject>();
            if (!baseInteractiveObject.Enabled)
            {
               RaycastingObject.Value = false;
               return;
            }

            CursorObject.Value = baseInteractiveObject.InteractSprite;
            RaycastingObject.Value = true;
            if (Input.GetMouseButtonDown(0))
            {
               baseInteractiveObject.Interact(HoldTransform, Camera);
               

               if (hit.collider.gameObject.GetComponentInParent<HoldComponent>())
               {
                  GrabbingObject.Value = true;
                  if (hit.collider.gameObject.GetComponent<MeshFilter>())
                  {
                     HoldTransformMeshCollider.sharedMesh = hit.collider.gameObject.GetComponent<MeshFilter>().mesh;
                     HoldTransform.localScale = hit.collider.transform.localScale * 1.1f;
                     HoldTransform.rotation = hit.collider.gameObject.transform.rotation;
                  }
                  else if (hit.collider.gameObject.GetComponentInParent<MeshFilter>())
                  {
                     HoldTransformMeshCollider.sharedMesh =
                        hit.collider.gameObject.GetComponentInParent<MeshFilter>().mesh;
                     HoldTransform.localScale = hit.collider.transform.parent.localScale * 1.1f;
                     HoldTransform.rotation = hit.collider.gameObject.transform.parent.rotation;
                  }
                  
                  
                  
                  ContrainHoldPosition.yOffsetCamera =
                     hit.collider.gameObject.GetComponentInParent<HoldComponent>().HoldOffsetY;
                  ContrainHoldPosition.zOffsetCamera =
                     hit.collider.gameObject.GetComponentInParent<HoldComponent>().HoldOffsetZ;

               }


            }
         }





      }
         else
         {
            RaycastingObject.Value = false;
         }
      }

      bool CheckLayer(float range)
      {
         RaycastHit hit;
         return Physics.Raycast(transform.position, transform.forward, out hit, range) && (hit.collider.isTrigger || hit.collider.gameObject.layer == LayerMask.NameToLayer("GrabObject"));
      }


   }
