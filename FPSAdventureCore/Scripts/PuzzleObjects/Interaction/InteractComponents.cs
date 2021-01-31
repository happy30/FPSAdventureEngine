using System.Collections;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;

[CreateAssetMenu(menuName = "Interactions/Component List" , fileName = "New Component List")]
public class InteractComponents : ScriptableObject
{
#if UNITY_EDITOR
    public List<MonoScript> MonoBehaviours = new List<MonoScript>();
#endif
}
