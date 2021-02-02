using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ConvertToInteractiveObject
{
    [MenuItem("FPS Adventure Tools/Convert Selected Object to Interactive Object %e", true)]
    static bool ValidateObjectSelected()
    {
        // Return false if no transform is selected.
        Transform selectedObject = Selection.activeTransform;
        return selectedObject != null;
    }
    
    
    
    [MenuItem("FPS Adventure Tools/Convert Selected Object to Interactive Object %e", priority = 11)]
    static void GenerateInteractiveParent()
    {
        Transform selectedObject = Selection.activeTransform;
        
        if (selectedObject != null)
        {
            if (LayerMask.NameToLayer("GrabObject") > -1)
            {
                GameObject newParent = new GameObject();
                Transform oldParent = selectedObject.parent;
                newParent.transform.SetParent(oldParent);
            
                newParent.transform.position = selectedObject.position;
                newParent.transform.rotation = selectedObject.rotation;
            
                //newParent.transform.localScale = selectedObject.localScale;
                newParent.transform.localScale = Vector3.one;
            
                newParent.name = "I" + selectedObject.name;
                newParent.AddComponent(typeof(BaseInteractiveObject));
                newParent.layer = LayerMask.NameToLayer("GrabObject");
                selectedObject.transform.SetParent(newParent.transform);
            
                if(!selectedObject.GetComponent<Rigidbody>())
                {
                    selectedObject.gameObject.AddComponent(typeof(Rigidbody));
                }
            
                if(!selectedObject.GetComponent<Collider>())
                {
                    selectedObject.gameObject.AddComponent(typeof(BoxCollider));
                }
            
                selectedObject.gameObject.layer = LayerMask.NameToLayer("GrabObject");
            
                Selection.SetActiveObjectWithContext(newParent, newParent);
            
                Undo.RegisterCreatedObjectUndo(newParent, "Create object");
                Undo.SetTransformParent(newParent.transform, oldParent, "Undo Parent");
            }
            else
            {
                Debug.LogError("Please add a Layer called GrabObject before using Interactive Objects");
            }
            
            
            
        }
        
    }
    
 
}
