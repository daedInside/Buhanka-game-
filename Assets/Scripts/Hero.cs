using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Hero : MonoBehaviour
{
    [SerializeField] private float speed = 3f; // �������� ��������
    [SerializeField] private int lives = 5; // �������� ��������
    [SerializeField] private AudioSource Jumpsound;
    [SerializeField] private AudioSource damagesound;
    [SerializeField] private AudioSource Attacksound;
    public Rigidbody2D physic;

    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator anim;

    private bool isGrounded;
    private bool isSecondJump = false;
    private float groundRadius = 0.3f;
    public int currentJump = 0;


    public bool isAttacking = false;
    public bool isRecarged = true;

    public Transform groundCheck;//������� ��� ���������
    public LayerMask groundMask;
    public Transform attackPos;
    public float attackRange;
    public LayerMask enemy;

    public static Hero Instance { get; set; }

    private States State
    {
        get { return (States)anim.GetInteger("state"); }
        set { anim.SetInteger("state", (int)value); }
    }

    private void Awake()
    {
        Instance = this;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        isRecarged = true;
    }

    private void Update()
    {
        if (isGrounded && !isAttacking) State = States.idle;

        if (Input.GetButton("Horizontal"))
            Run();

        if (Input.GetButtonDown("Fire1"))
            Attack();

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
        Jumpsound.Play();
    }

    private void Attack()
    {
        if(isGrounded && isRecarged)
        {
            State = States.attack;
            isAttacking = true;
            isRecarged = false;

            StartCoroutine(AttackAnimation());
            Attacksound.Play();
            StartCoroutine(AttackCoolDown());
        }
    }

    
    private IEnumerator AttackAnimation()
    {
        yield return new WaitForSeconds(0.4f);
        isAttacking = false;
    }

    private IEnumerator AttackCoolDown()
    {
        yield return new WaitForSeconds(0.5f);
        isRecarged = true;
    }

    private void OnAttack()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemy);

        for(int i=0;i<colliders.Length;i++)
        {
            colliders[i].GetComponent<Entity>().GetDamage();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }


    public void GetDamage()
    {
        lives -= 1;
        damagesound.Play();
        Debug.Log(lives);
    }

    public void GetDamageFire()
    {
        lives -= 5;
        damagesound.Play();
    }
    public virtual void DieHero()
    {
        if(lives<=0)
            
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

public enum States
{
    idle,
    run,
    jump,
    attack
   
}
