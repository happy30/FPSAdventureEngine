
using UnityEngine;

public class RotateScript : MonoBehaviour
{
    public float rotateX;
    public float rotateY;
    public float rotateZ;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotateX * Time.deltaTime, rotateY * Time.deltaTime, rotateZ * Time.deltaTime);
    }
}
