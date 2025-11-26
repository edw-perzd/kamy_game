using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : MonoBehaviour
{
    public int scoreValue = 1;
    public AudioSource audioSource;
    public AudioClip soundRecoger;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            audioSource.PlayOneShot(soundRecoger);
            GameManager.Instance.AddScore(scoreValue);
            Destroy(this.gameObject, soundRecoger.length);
        }
    }
}
