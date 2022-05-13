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
    private Animator anim;

    private bool isGrounded;
    private bool isSecondJump = false;
    private float groundRadius = 0.3f;
    private static int maxJumps = 2;
    public int currentJump = 0;

    public Transform groundCheck;//позиция ног персонажа
    public LayerMask groundMask;

    private States State
    {
        get { return (States)anim.GetInteger("state"); }
        set { anim.SetInteger("state", (int)value); }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();

    }

    private void FixedUpdate()
    {

    }


    private void Update()
    {
        if (isGrounded) State = States.idle;

        if (Input.GetButton("Horizontal"))
            Run();

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundMask);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
            isSecondJump = true;
        }
        else if (isSecondJump)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
                isSecondJump = false;
            }
        }

    }

    private void Run()
    {
        if (isGrounded) State = States.run;

        Vector3 dir = transform.right * Input.GetAxis("Horizontal");

        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, speed * Time.deltaTime);

        sprite.flipX = dir.x < 0.0f;
    }

    private void Jump()
    {
        physic.AddForce(new Vector2(0, 600));
        if (!isGrounded) State = States.jump;
    }
}

public enum States
{
    idle,
    run,
    jump
}
