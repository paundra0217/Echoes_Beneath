using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunctionKosong : MonoBehaviour
{
    AudioSource audioSource;
    ParticleSystem particle;
    void Start()
    {
        audioSource = GetComponentInChildren<AudioSource>();
        particle = GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame

    public void Deactivate()
    {
        audioSource.Stop();
        particle.Stop();
    }
}
