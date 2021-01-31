using System;
using System.Collections;
using System.Collections.Generic;
using EventObjects;
using UnityEngine;
using UnityEngine.Events;


[System.Serializable]
public class BaseTriggerComponent : MonoBehaviour
{
    public bool Enabled;
    public bool OneTimeEvent;
    public bool TriggerExit;
    public BoolWithEvent Condition;
    


    
    [HideInInspector]
    public List<AudioTrigger> AudioTriggers = new List<AudioTrigger>();

    [HideInInspector]
    public List<SceneObjectTrigger> SceneObjectTriggers = new List<SceneObjectTrigger>();

    [HideInInspector] 
    public List<TeleportTrigger> TeleportTriggers = new List<TeleportTrigger>();
    
    [HideInInspector] 
    public List<ModifyMaterialTrigger> MaterialTriggers = new List<ModifyMaterialTrigger>();
    
    [HideInInspector] 
    public List<RigidbodyTrigger> RigidbodyTriggers = new List<RigidbodyTrigger>();
    [HideInInspector] 
    public List<PlayAnimationTrigger> AnimationTriggers = new List<PlayAnimationTrigger>();

    [HideInInspector] public List<SceneEventTrigger> SceneEventTriggers = new List<SceneEventTrigger>();
    [HideInInspector] public List<BoolWithEventTrigger> BoolEvents = new List<BoolWithEventTrigger>();
    [HideInInspector] public TriggerUnityEvent UnityTriggerEvent;
    

    void OnDrawGizmos()
    {
        // Draw a semitransparent blue cube at the transforms position
        Gizmos.color = TriggerExit? new Color(0.36f, 0.62f, 1f, 0.5f) : new Color(1, 0, 0, 0.5f);
        Gizmos.DrawWireCube(transform.position, transform.localScale);
    }

    void OnTriggerEnter (Collider col)
    {
        if (!TriggerExit)
        {
            ActivateComponents(col);
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (TriggerExit)
        {
            ActivateComponents(col);
        }
    }

    public void SetEnabled()
    {
        Enabled = true;
    }


    void ActivateComponents(Collider col)
    {
        if (col.gameObject.CompareTag("Player") && Enabled)
        {
            Trigger();
        }

    }

    public void Trigger()
    { 
        if (Condition != null)
        {
            if (!Condition.Value) return;
        }
            
        foreach (var audioTrigger in AudioTriggers)
        {
            PlaySound(audioTrigger);
        }

        foreach (var sceneObjectTrigger in SceneObjectTriggers)
        {
            SceneObjectEffect(sceneObjectTrigger);
        }

        foreach (var teleportTrigger in TeleportTriggers)
        {
            if (teleportTrigger.Relative)
            {
                TeleportObjectRelative(teleportTrigger.TeleportedObject, teleportTrigger.RelativeLocation, teleportTrigger.Delay);
            }
            else
            {
                TeleportObject(teleportTrigger.TeleportedObject, teleportTrigger.TargetLocation, teleportTrigger.CopyRotation, teleportTrigger.Delay);
            } 
        }

        foreach (var materialTrigger in MaterialTriggers)
        {
            StartCoroutine(ChangeMaterialAfterDelay(materialTrigger));
        }
            
        foreach (var rigidbodyTrigger in RigidbodyTriggers)
        {
            StartCoroutine(ChangeRigidbodyAfterDelay(rigidbodyTrigger));
        }

        foreach (var animationTrigger in AnimationTriggers)
        {
            StartCoroutine(TriggerAnimationAfterDelay(animationTrigger));
        }

        foreach (var sceneEventTrigger in SceneEventTriggers)
        {
            StartCoroutine(SceneEffectAfterDelay(sceneEventTrigger));
        }

        foreach (var boolEvent in BoolEvents)
        {
            boolEvent.Bool.Value = boolEvent.NewState;
        }
            
        UnityTriggerEvent.Invoke();

        if (OneTimeEvent) Enabled = false;
    }
        
    


    #region Methods
    
    void TriggerAnimation(PlayAnimationTrigger animationTrigger)
    {
        if (animationTrigger.TriggerString != "")
        {
            animationTrigger.Animator.SetTrigger(animationTrigger.TriggerString);
        }

        if (animationTrigger.Bool != "")
        {
            animationTrigger.Animator.SetBool(animationTrigger.Bool, animationTrigger.SetBool);
        }
        
        
    }

    void ChangeRigidbody(RigidbodyTrigger rigidbodyTrigger)
    {
        var _rb = rigidbodyTrigger.Rigidbody;


        _rb.useGravity = rigidbodyTrigger.UseGravity;
        _rb.mass = rigidbodyTrigger.Mass;
        _rb.drag = rigidbodyTrigger.Drag;

        _rb.constraints = RigidbodyConstraints.FreezeAll;

        if (!rigidbodyTrigger.PositionConstraints.x) _rb.constraints &= ~RigidbodyConstraints.FreezePositionX;
        if (!rigidbodyTrigger.PositionConstraints.y) _rb.constraints &= ~RigidbodyConstraints.FreezePositionY;
        if (!rigidbodyTrigger.PositionConstraints.z) _rb.constraints &= ~RigidbodyConstraints.FreezePositionZ;
        if (!rigidbodyTrigger.RotationConstraints.x) _rb.constraints &= ~RigidbodyConstraints.FreezeRotationX;
        if (!rigidbodyTrigger.RotationConstraints.x) _rb.constraints &= ~RigidbodyConstraints.FreezeRotationY;
        if (!rigidbodyTrigger.RotationConstraints.x) _rb.constraints &= ~RigidbodyConstraints.FreezeRotationZ;

    }
    

    void ChangeMaterial(ModifyMaterialTrigger materialTrigger)
    {

        var shader = materialTrigger.Renderer.material.shader;
        var newMat = new Material(shader);
        newMat.CopyPropertiesFromMaterial(materialTrigger.Renderer.material);

        if (materialTrigger.NewAlbedo != null)
        {
            newMat.mainTexture = materialTrigger.NewAlbedo;
        }
        
        if (materialTrigger.NewNormal != null)
        {
            newMat.EnableKeyword ("_NORMALMAP");
            newMat.SetTexture("_BumpMap", materialTrigger.NewNormal);
        }

        if (materialTrigger.NewMetallic != null)
        {
            newMat.EnableKeyword ("_METALLICGLOSSMAP");
            newMat.SetTexture ("_MetallicGlossMap", materialTrigger.NewMetallic);
        }
        
       //newMat.EnableKeyword("_EMISSION");
        
        //newMat.SetColor("_EmissionColor", materialTrigger.EmissionColor);


        newMat.color = materialTrigger.AlbedoColor;
        materialTrigger.Renderer.material = newMat;
        
    }

    void PlaySound(AudioTrigger audioTrigger)
    {
        audioTrigger.Source.clip = audioTrigger.Clip;
        audioTrigger.Source.volume = audioTrigger.Volume;
        audioTrigger.Source.loop = audioTrigger.Loop;
        audioTrigger.Source.PlayDelayed(audioTrigger.Delay);
    }

    void SceneObjectEffect(SceneObjectTrigger sceneObjectTrigger)
    {
        if (sceneObjectTrigger.Delay > 0)
        {
            StartCoroutine(ChangeStateAfterDelay(sceneObjectTrigger.SceneObject, sceneObjectTrigger.SetObjectActive,
                sceneObjectTrigger.Delay));
        }
        else
        {
            sceneObjectTrigger.SceneObject.SetActive(sceneObjectTrigger.SetObjectActive);
        }

        if (sceneObjectTrigger.ToInitialStateAfterDuration)
        {
            StartCoroutine(ChangeStateAfterDelay(sceneObjectTrigger.SceneObject, !sceneObjectTrigger.SetObjectActive,
                sceneObjectTrigger.Delay + sceneObjectTrigger.Duration));
        }
    }

    void TeleportObjectRelative(Transform from, Vector3 to, float delay)
    {
        if (delay > 0)
        {
            StartCoroutine(TeleportTransformRelativeAfterDelay(from, to, delay));
        }
        else
        {
            from.position += to;
        }
    }

    void TeleportObject(Transform from, Transform to, bool rotation, float delay)
    {
        
        
        if (delay > 0)
        {
            StartCoroutine(TeleportTransformAfterDelay(from, to, rotation, delay));
        }
        else
        {
            Debug.Log("Changed pos");
            
            from.position = to.position;
            if (rotation)
            {
                from.rotation = to.rotation;
            }
        }
    }
    
    #endregion
    
    #region IEnums

    IEnumerator SceneEffectAfterDelay(SceneEventTrigger sceneEffectTrigger)
    {
        yield return new WaitForSeconds(sceneEffectTrigger.Delay);
        sceneEffectTrigger.SceneEvent.Raise();
    }
    
    IEnumerator TriggerAnimationAfterDelay(PlayAnimationTrigger animationTrigger)
    {
        yield return new WaitForSeconds(animationTrigger.Delay);
        TriggerAnimation(animationTrigger);
        
    }
    
    IEnumerator ChangeRigidbodyAfterDelay(RigidbodyTrigger rigidbodyTrigger)
    {
        yield return new WaitForSeconds(rigidbodyTrigger.Delay);
        ChangeRigidbody(rigidbodyTrigger);
    }

    
    IEnumerator ChangeMaterialAfterDelay(ModifyMaterialTrigger materialTrigger)
    {
        yield return new WaitForSeconds(materialTrigger.Delay);
        ChangeMaterial(materialTrigger);
    }

    IEnumerator ChangeStateAfterDelay(GameObject sceneObject, bool state, float delay)
    {
        yield return new WaitForSeconds(delay);
        sceneObject.SetActive(state);
    }

    IEnumerator TeleportTransformAfterDelay(Transform from, Transform to, bool rotation, float Delay)
    {
        yield return new WaitForSeconds(Delay);
        
        from.position = to.position;
        if (rotation)
        {
            from.rotation = to.rotation;
        }
    }
    
    IEnumerator TeleportTransformRelativeAfterDelay(Transform from, Vector3 to, float Delay)
    {
        yield return new WaitForSeconds(Delay);

        from.position += to;

    }
    
    #endregion
    
    
}

[System.Serializable]
public class TriggerUnityEvent : UnityEvent {}
