using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeActiveStateComponent : MonoBehaviour, IInteractComponent
{
    public GameObject[] Objects;
    public bool ToInactive;
    public float Delay;

    public void OnActivate()
    {
        StartCoroutine(ChangeObjectsAfterDelay());
    }

    IEnumerator ChangeObjectsAfterDelay()
    {
        yield return new WaitForSeconds(Delay);
        foreach (var obj in Objects)
        {
            obj.SetActive(!ToInactive);
        }
    }

    public void OnUpdate()
    {
        
    }

    public void OnDeactivate()
    {
        
    }
}
