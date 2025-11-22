using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float speed = 5f;
    private Animator anim;
    private Rigidbody2D rb2d;
    private float move;
    public float jumForce = 4.0f;
    private bool isGrounded;
    public Transform groundCheck;
    public float groundRadius = 0.1f;
    public LayerMask groundLayer;
    private int diamonds;
    public TMP_Text textDiamonds;

    // ===============================
    // VARIABLES DE COMBATE
    // ===============================
    public bool isInvincible = false;
    public bool recibeDanio;
    private bool isNockbacking;
    public int health;
    public int vidaMax = 3;

    // ===============================
    // REINICIO / RESPAWN
    // ===============================
    public float fallLimit = -10f;
    private Vector3 respawnPoint;   // NUEVO

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        health = vidaMax;
        recibeDanio = false;
        isNockbacking = false;

        respawnPoint = transform.position; // Se guarda posición inicial
    }

    void Update()
    {
        move = Input.GetAxisRaw("Horizontal");

        anim.SetBool("walking", move != 0);

        if (!isNockbacking)
            rb2d.velocity = new Vector2(move * speed, rb2d.velocity.y);

        if (move != 0)
            transform.localScale = new Vector3(Mathf.Sign(move) * 5, 5, 1);

        anim.SetBool("inFloor", isGrounded);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumForce);
        }

        anim.SetBool("recibeDanio", recibeDanio);
        // =============================================
        // CAÍDA AL VACÍO → PIERDE 1 VIDA Y RESPAWNEA
        // =============================================
        if (transform.position.y < fallLimit)
        {
            FallDamage();
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            anim.SetTrigger("Attack");
        }
    }

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);
    }

    // ===============================================
    //  MÉTODO DE RECIBIR DAÑO POR ENEMIGOS
    // ===============================================
    public void TakeDamage(int damage)
    {
        if (!isInvincible)
        {
            recibeDanio = true;
            health -= damage;
            Debug.Log("Daño recibido. Vida actual: " + health);

            if (health <= 0)
            {
                Die();
            }
            else
            {
                StartInvincibility(1f);
            }
            
        }
    }
    public void StopReceivingDamage()
    {
        recibeDanio = false;
    }

    // ===============================================
    //  KNOCKBACK
    // ===============================================
    public void Knockback(Vector3 sourcePosition, float force)
    {
        isNockbacking = true;
        Vector2 direction = (transform.position - sourcePosition).normalized;
        rb2d.velocity = new Vector2(direction.x * force, force / 2f);
        StartCoroutine(EndKnockback(0.3f));
    }

    private IEnumerator EndKnockback(float duration)
    {
        yield return new WaitForSeconds(duration);
        isNockbacking = false;
    }

    // ===============================================
    //  INVENCIBILIDAD TEMPORAL
    // ===============================================
    public void StartInvincibility(float time)
    {
        StartCoroutine(InvincibilityCoroutine(time));
    }

    private IEnumerator InvincibilityCoroutine(float time)
    {
        isInvincible = true;
        yield return new WaitForSeconds(time);
        isInvincible = false;
    }

    // ===============================================
    //  CAÍDA AL VACÍO — PIERDE VIDA Y RESPAWN
    // ===============================================
    private void FallDamage()
    {
        health -= 1;
        Debug.Log("El jugador cayó. Vida actual: " + health);

        if (health <= 0)
        {
            Die();
        }
        else
        {
            transform.position = respawnPoint; // Reaparece
            rb2d.velocity = Vector2.zero;
        }
    }

    // ===============================================
    //  MUERTE — REINICIAR EL JUEGO
    // ===============================================
    private void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        GameManager.Instance.ResetScore();
    }
}
