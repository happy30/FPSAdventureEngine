using UnityEngine;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Legacy
/// </summary>
namespace CodingJar.AdditiveScenes
{
    /// <summary>
    /// This script is part of AdditiveScenes, which was removed from the project. It is here so you can still find former AdditiveScenes.
    /// </summary>
	[ExecuteInEditMode]
	public class SubScene : MonoBehaviour
	{
        public void Awake()
        {
            Debug.LogWarning("Legacy Additive Scene detected; please convert to Advanced MultiScene: " + gameObject.name, gameObject);
        }
    }
} 