using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MaloPatrullaje : MonoBehaviour
{
    public int scoreValue = 5;
    public int damage = 1;
    public float knockbackForce = 3f;
    public float invincibilityTime = 1f;

    [Header("Puntos")]
    public Transform pointA;
    public Transform pointB;
    public float moveSpeed = 2f;
    public float tolerance = 0.3f;
    public int health = 3;
    private bool enMovimiento;
    private bool morido;
    private bool isNockbacking;
    private Vector3 targetPosition;
    private Rigidbody2D rb;
    private Animator anim;

    void Start()
    {
        isNockbacking = false;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

        if (pointA == null || pointB == null)
        {
            Debug.LogError("No hay puntos");
            enabled = false;
            return;
        }

        Vector3 localScale = transform.localScale;
        localScale.x = Mathf.Abs(localScale.x);
        transform.localScale = localScale;

        float distA = Vector3.Distance(transform.position, pointA.position);
        float distB = Vector3.Distance(transform.position, pointB.position);

        targetPosition = (distA < distB) ? pointB.position : pointA.position;

        Flip();
    }
    void Update()
    {
        anim.SetBool("enMovimiento", enMovimiento);
        anim.SetBool("morido", morido);
    }

    void FixedUpdate()
    {
        
        if (rb == null) return;

        Vector3 currentPosition = transform.position;

        // Cuando llega cerca de un punto, cambia al otro
        if (Mathf.Abs(targetPosition.x - currentPosition.x) < tolerance)
        {
            targetPosition = (targetPosition == pointB.position) ? pointA.position : pointB.position;
            Flip();
        }

        // Movimiento hacia el objetivo
        if (!isNockbacking)
        {
            enMovimiento = true;
            float directionX = Mathf.Sign(targetPosition.x - currentPosition.x);
            rb.velocity = new Vector2(directionX * moveSpeed, rb.velocity.y);
        }
    }

    private void Flip()
    {
        enMovimiento = true;
        bool lookLeft = targetPosition.x < transform.position.x;

        Vector3 localScale = transform.localScale;

        if (lookLeft)
            localScale.x = -Mathf.Abs(localScale.x);
        else
            localScale.x = Mathf.Abs(localScale.x);

        transform.localScale = localScale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Player player = collision.collider.GetComponent<Player>();

        if (player != null && !player.isInvincible)
        {
            player.TakeDamage(damage);
            player.Knockback(transform.position, knockbackForce);
            player.StartInvincibility(invincibilityTime);
        }
    }
    public void Knockback(Vector3 sourcePosition, float force)
    {
        isNockbacking = true;
        Vector2 direction = (transform.position - sourcePosition).normalized;
        rb.velocity = new Vector2(direction.x * force, force / 2f);
        StartCoroutine(EndKnockback(0.3f));
    }

    private IEnumerator EndKnockback(float duration)
    {
        yield return new WaitForSeconds(duration);
        isNockbacking = false;
    }

    public void TakeDamage(int damage)
    {

        health -= damage;
        // Debug.Log("Daño al enemigo. Vida actual del enemigo: " + health);

        if (health <= 0)
        {
            // Die();
            morido = true;
        }
        else
        {
            morido = false;
        }

    }

    private void Die()
    {
        // morido = true;
        GameManager.Instance.AddScore(scoreValue);
        Destroy(this.gameObject);
    }
}
