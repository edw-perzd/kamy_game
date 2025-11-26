using UnityEngine;

public class BossFinal : MonoBehaviour
{
    public int bossHealth = 10;               // Vida del jefe
    public float patrolSpeed = 2f;            // Velocidad patrullando
    public float chaseSpeed = 4f;             // Velocidad persiguiendo
    public float attackRange = 1.5f;          // Distancia para golpear
    public float detectionRange = 6f;         // Distancia para empezar a perseguir
    public int attackDamage = 2;              // Daño del ataque del jefe
    public float knockbackForce = 20f;        // Empuje fuerte del jefe
    public float invincibilityTime = 1f;

    [Header("Puntos de patrullaje")]
    public Transform pointA;
    public Transform pointB;

    // COOLdown para evitar ataques instantáneos
    public float attackCooldown = 1f;
    private float lastAttackTime = 0f;

    private Transform player;
    private Vector3 targetPosition;
    private Rigidbody2D rb;
    private bool chasing = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        if (pointA == null || pointB == null)
        {
            Debug.LogError("El jefe necesita puntos A y B");
            enabled = false;
            return;
        }

        targetPosition = pointB.position;
    }

    void Update()
    {
        float distToPlayer = Vector3.Distance(transform.position, player.position);

        // Si el jugador está cerca → perseguir
        if (distToPlayer < detectionRange)
        {
            chasing = true;
        }
        // Si el jugador se alejó → volver a patrullar
        else if (distToPlayer > detectionRange + 2)
        {
            chasing = false;
        }
    }

    void FixedUpdate()
    {
        if (chasing)
        {
            ChasePlayer();
        }
        else
        {
            Patrol();
        }
    }

    // ============================================
    //                 PATRULLAJE
    // ============================================
    void Patrol()
    {
        float distA = Vector3.Distance(transform.position, pointA.position);
        float distB = Vector3.Distance(transform.position, pointB.position);

        if (Vector3.Distance(transform.position, targetPosition) < 0.3f)
        {
            targetPosition = (targetPosition == pointA.position) ? pointB.position : pointA.position;
        }

        MoveTo(targetPosition, patrolSpeed);
    }

    // ============================================
    //                PERSEGUIR JUGADOR
    // ============================================
    void ChasePlayer()
    {
        MoveTo(player.position, chaseSpeed);

        // Si está lo suficientemente cerca, atacar
        if (Vector3.Distance(transform.position, player.position) < attackRange)
        {
            AttackPlayer();
        }
    }

    // ============================================
    //               ATAQUE DEL JEFE
    // ============================================
    void AttackPlayer()
    {
        // Evitar golpear demasiado rápido
        if (Time.time < lastAttackTime + attackCooldown)
            return;

        Player p = player.GetComponent<Player>();

        if (p != null && !p.isInvincible)
        {
            p.TakeDamage(attackDamage);
            p.Knockback(transform.position, knockbackForce);
            p.StartInvincibility(invincibilityTime);

            // Guardar el tiempo del último ataque
            lastAttackTime = Time.time;
        }
    }

    // ============================================
    //              FUNCION MOVER
    // ============================================
    void MoveTo(Vector3 position, float speed)
    {
        Vector2 direction = (position - transform.position).normalized;
        rb.velocity = new Vector2(direction.x * speed, rb.velocity.y);

        // Voltear hacia el movimiento
        transform.localScale = new Vector3(Mathf.Sign(direction.x) * Mathf.Abs(transform.localScale.x), transform.localScale.y, 1);
    }

    // ============================================
    //          RECIBIR DAÑO DEL JUGADOR
    // ============================================
    public void TakeDamage(int amount)
    {
        bossHealth -= amount;

        if (bossHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
        Debug.Log("JEFE DERROTADO");
    }
}
