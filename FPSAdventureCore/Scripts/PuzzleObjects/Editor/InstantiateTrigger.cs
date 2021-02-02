using UnityEngine;
using UnityEditor;

public class InstantiateTrigger : EditorWindow
{
    [MenuItem("FPS Adventure Tools/Create Trigger %&n", priority = 10)]
    static void CreateAPrefab()
    {
        //Parent
        GameObject prefab =
            (GameObject)PrefabUtility.InstantiatePrefab((GameObject) Resources.Load("NewTrigger"));
        prefab.name = "New Trigger";

        if (Selection.activeTransform != null)
        {
            prefab.transform.SetParent(Selection.activeTransform, false);
        }

        prefab.transform.localPosition = SceneView.lastActiveSceneView.camera.transform.position + SceneView.lastActiveSceneView.camera.transform.forward * 3;
        prefab.transform.localEulerAngles = Vector3.zero;
        prefab.transform.localScale = Vector3.one;
        Selection.activeGameObject = prefab;
    }
}