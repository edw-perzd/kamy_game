using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int damage = 1;
    public GameObject hitbox; // Objeto HitboxAtaque asignado desde el Inspector

    void Start()
    {
        hitbox.SetActive(false);
    }
    void Update()
    {
    }

    public void ActivarHitbox()
    {
        hitbox.SetActive(true);
    }

    public void DesactivarHitbox()
    {
        hitbox.SetActive(false);
    }
}

