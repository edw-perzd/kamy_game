using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform player;
    public float detectionRadius = 5.0f;
    public float speed = 3.0f; 
    private Rigidbody2D rb2d;
    private Vector2 movement;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer <= detectionRadius)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            movement = new Vector2(direction.x, 0);
        }
        else
        {
            movement = Vector2.zero;
        }
        rb2d.MovePosition(rb2d.position + movement * speed * Time.deltaTime);
    }
    private void OnColisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector2 direccionDanio = new Vector2(transform.position.x, 0);
            collision.gameObject.GetComponent<Player>().RecibeDanio(direccionDanio, 1);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
