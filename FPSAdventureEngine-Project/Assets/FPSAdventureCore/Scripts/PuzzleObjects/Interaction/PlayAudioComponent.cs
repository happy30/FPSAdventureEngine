using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioComponent : MonoBehaviour, IInteractComponent
{
    public AudioSource AudioSource;
    public AudioClip AudioClip;

    public float Volume = 1;
    public float Delay;

    public void OnActivate()
    {
        StartCoroutine(PlayAudioAfterDelay(Delay, AudioSource, AudioClip));
    }

    public void OnUpdate()
    {
       
    }

    public void OnDeactivate()
    {
        
    }

    IEnumerator PlayAudioAfterDelay(float delay, AudioSource source, AudioClip clip)
    {
        yield return new WaitForSeconds(delay);

        if (clip == null)
        {
            AudioSource.Stop();
            
        }
        else
        {
            AudioSource.PlayOneShot(AudioClip, Volume);
        }
        
        
    }
}
