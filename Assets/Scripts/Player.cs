using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    public float speed = 5f;
    private Animator anim;
    private Rigidbody2D rb2d;
    private float move;
    public float jumForce = 4.0f;
    public float reboteForce = 4.0f;
    private bool isGrounded;
    private bool recibiendoDanio = false;
    public Transform groundCheck;
    public float groundRadius = 0.1f;
    public LayerMask groundLayer;
    private int diamonds;
    public TMP_Text textDiamonds;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        move = Input.GetAxisRaw("Horizontal");
        
        anim.SetBool("walking", move != 0);
        
        rb2d.velocity = new Vector2(move * speed, rb2d.velocity.y);

        if (move != 0)
            transform.localScale = new Vector3(Mathf.Sign(move) * 5, 5, 1);

            
        anim.SetBool("inFloor", isGrounded);
        anim.SetBool("recibeDanio", recibiendoDanio);
        if (Input.GetButtonDown("Jump") && isGrounded && !recibiendoDanio)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumForce);

        }
    }
    public void RecibeDanio(Vector2 direccion, int cantDanio)
    {
        if (!recibiendoDanio)
        {
            recibiendoDanio = true;
            Vector2 rebote = new Vector2(transform.position.x - direccion.x, 1).normalized;
            rb2d.AddForce(rebote * reboteForce, ForceMode2D.Impulse);
        }
    }
    public void DesactivarDanio()
    {
        recibiendoDanio = false;
    }
    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);
    }

}