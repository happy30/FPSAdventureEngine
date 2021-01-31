using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource), typeof(Rigidbody))]
public class CollisionSound : MonoBehaviour
{
    private AudioSource _source;
    public AudioClip ImpactSound;

    void Awake()
    {
        _source = GetComponent<AudioSource>();
        _source.spatialBlend = 1f;
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude > 1)
        {
            _source.PlayOneShot(ImpactSound, collision.relativeVelocity.magnitude * 0.1f );
        }
            
    }
}
