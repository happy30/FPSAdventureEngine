using System;
using System.Collections;
using UnityEngine;

namespace Packages.FPSAdventureEngine.FPSAdventureCore.Scripts.Audio
{
    public class FadeAudio : MonoBehaviour
    {
        private AudioSource _source;
        public float Volume;

        private float initialVolume;

        void Awake()
        {
            _source = GetComponent<AudioSource>();

            if (Math.Abs(initialVolume) < 0.1f)
            {
                initialVolume = Volume;
            }
            else
            {
                initialVolume = _source.volume;
            }
        
        
        
        
        }

        public void FadeIn()
        {
            StartCoroutine(FadeVolumeIn());
        }

        public void FadeOut()
        {
            StartCoroutine(FadeVolumeOut());
        }


        IEnumerator FadeVolumeIn()
        {
            while (_source.volume < initialVolume)
            {
                _source.volume += Time.deltaTime * 0.5f;
                yield return null;
            }
        }
    
        IEnumerator FadeVolumeOut()
        {
            while (_source.volume > 0)
            {
                _source.volume -= Time.deltaTime * 0.5f;
                yield return null;
            }
        }
    }
}
