using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using CodingJar;

/// <summary>
/// Legacy
/// </summary>
namespace CodingJar.AdditiveScenes
{
    /// <summary>
    /// This script is part of Additive Scenes, which was removed from the project. It is here to remove the gameobjects AdditiveScenes created.
    /// </summary>
    [ExecuteInEditMode]
    public class SceneData : MonoBehaviour
    {
        public void Awake()
        {
            Debug.LogWarning("Removing obsolete " + gameObject.name + "  This is normal, and won't happen again after you save the scene.");
            DestroyImmediate(gameObject);
        }
    }
}
