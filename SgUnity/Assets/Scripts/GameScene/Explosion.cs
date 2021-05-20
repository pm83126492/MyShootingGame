using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour, IPoolObject
{
    public ParticleSystem ExploseEffect;
    public AudioSource audioSource;

    public void OnObjectSpawn()
    {
        ExploseEffect.Play();
        audioSource.Play();
    }

}
