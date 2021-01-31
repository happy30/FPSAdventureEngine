using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AudioTrigger : IBaseTrigger
{
     public AudioClip Clip;
     public AudioSource Source;
     public float Delay;
     public float Volume = 100f;
     public bool Loop;

     public void Activate()
     {
          Source.PlayOneShot(Clip);
     }

     public float GetDelay() => Delay;
}
