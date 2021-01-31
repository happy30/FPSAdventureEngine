using UnityEngine;
using UnityEditor;

public class InstantiateTrigger : EditorWindow
{
    [MenuItem("GameObject/Parcel/Create Trigger %&n")]
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