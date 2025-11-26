using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundController : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip soundSaltar;
    public AudioClip soundRecibeDanio;
    public AudioClip soundAtacar;
    public AudioClip soundMuerte;
    public AudioClip soundMov1;
    public AudioClip soundMov2;
    public void PlaySaltar()
    {
        audioSource.PlayOneShot(soundSaltar);
    }
    public void PlayRecibeDanio()
    {
        audioSource.PlayOneShot(soundRecibeDanio);
    }
    public void PlayAtacar()
    {
        audioSource.PlayOneShot(soundAtacar);
    }
    public void PlayMuerte()
    {
        audioSource.PlayOneShot(soundMuerte);
    }
    public void PlayMov1()
    {
        audioSource.PlayOneShot(soundMov1);
    }
    public void PlayMov2()
    {
        audioSource.PlayOneShot(soundMov2);
    }
}
