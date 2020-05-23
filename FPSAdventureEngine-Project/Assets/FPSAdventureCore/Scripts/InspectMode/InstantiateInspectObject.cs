using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateInspectObject : MonoBehaviour
{
    public float zOffset;
    public Vector3 CustomEulerAngles;
    
    
    public InstantiateInspectObject Copy(Transform parent)
    {
        InstantiateInspectObject _obj = Instantiate(this, Vector3.zero, Quaternion.identity);
        _obj.transform.SetParent(parent);
        _obj.transform.localPosition = new Vector3(0, 0, zOffset);
        _obj.transform.localEulerAngles = CustomEulerAngles;

        return _obj;
    }

    public void DestroyObject()
    {
        Destroy(this.gameObject);
    }
}
