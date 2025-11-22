using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxAtaque : MonoBehaviour
{
    public int damage = 1;
    public float knockbackForce = 2f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        MaloPatrullaje enemigo = collision.GetComponent<MaloPatrullaje>();

        if (enemigo != null)
        {
            enemigo.TakeDamage(damage);
            enemigo.Knockback(transform.position, knockbackForce);
        }
    }
}

