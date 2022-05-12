using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Hero : MonoBehaviour
{
    [SerializeField] private float speed = 3f; // скорость движения
    [SerializeField] private int lives = 5; // скорость движения

    public Rigidbody2D physic;

    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private bool isGrounded;
    private float groundRadius = 1.3f;

    public Transform groundCheck;//позиция ног персонажа
    public LayerMask groundMask;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        
    }
   

    private void Update()
    {
        if (Input.GetButton("Horizontal"))
            Run();

        if(Input.GetKeyDown(KeyCode.Space)&& isGrounded)
        {
            physic.AddForce(new Vector2(0, 600));
        }
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundMask);
    }

    private void Run()
    {
        Vector3 dir = transform.right * Input.GetAxis("Horizontal");

        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, speed * Time.deltaTime);

        sprite.flipX = dir.x < 0.0f;
    }

    



}
