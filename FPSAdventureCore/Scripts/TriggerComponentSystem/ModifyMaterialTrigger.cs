using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ModifyMaterialTrigger
{
    public Renderer Renderer;
    public Shader Shader;
    
    public Texture2D NewAlbedo;
    public Texture2D NewMetallic;
    public Texture2D NewNormal;

    public Color AlbedoColor = Color.white;
    public Color EmissionColor = new Color(1,1,1,0);
    

    public float Delay;
}
