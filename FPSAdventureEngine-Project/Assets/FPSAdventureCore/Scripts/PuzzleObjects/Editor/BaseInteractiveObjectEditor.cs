using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.TerrainAPI;
using UnityEngine;

[CanEditMultipleObjects]
[CustomEditor(typeof(BaseInteractiveObject))]
public class BaseInteractiveObjectEditor : Editor
{
    private BaseInteractiveObject _interactiveObject;
    
    [SerializeField] public SpriteCollection SpriteCollection;
    
    private void OnEnable()
    {
        _interactiveObject = (BaseInteractiveObject) target;
        SpriteCollection = (SpriteCollection)Resources.Load("Cursors");
        _interactiveObject.InteractComponentsList = (InteractComponents) Resources.Load("InteractComponents");
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        
        if (SpriteCollection != null)
        {
            _interactiveObject.InteractSprite = SpriteCollection.Sprites[(int)_interactiveObject.Cursor];
        }
        else
        {
            
            GUILayout.Label("Cannot find Cursor Sprites!!. Put SpriteCollection in Resources/Cursors.asset");
        }
        
        GUILayout.Space(15);
        GUILayout.Label("Click to add component...");

        if (_interactiveObject.InteractComponentsList != null)
        {
            foreach(var component in _interactiveObject.InteractComponentsList.MonoBehaviours)
            {
                if(component == null) continue;
                
                EditorGUI.BeginDisabledGroup(_interactiveObject.GetComponent(component.GetClass()));
                if (GUILayout.Button("Add " + component.name))
                {
                    _interactiveObject.gameObject.AddComponent(component.GetClass());
                }
                EditorGUI.EndDisabledGroup();
            }
        }
    }
}
