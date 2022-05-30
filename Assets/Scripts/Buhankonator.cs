using System.Collections;

using System.Collections.Generic;
using UnityEngine;

public class Buhankonator : Entity
{
    [SerializeField] private AudioSource Indiansound;
    private SpriteRenderer sprite;
    private Animator anim;
    private Rigidbody2D physic;
    public Transform Hero;
    public float speed;
    public float agroDistance;
    Animator animator;
    

    private void Start()
    {
        animator = GetComponent<Animator>();
        physic = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }
   
    private void Update()
    {
        float distToHero = Vector2.Distance(transform.position, Hero.position);
        if(distToHero < agroDistance)
        {
            StartHunting();
            
        }
        else
        {
            StopHunting();
        }
        anim = GetComponent<Animator>();
       
    }
    
    void StartHunting()
    {

      
        if (Hero.position.x < transform.position.x)
        {
            physic.velocity = new Vector2(-speed, 0);
            sprite.flipX = transform.position.x < 0.0f;

        }
        else if (Hero.position.x > transform.position.x)
        {
            physic.velocity = new Vector2(speed, 0);
            sprite.flipX = transform.position.x > 0.0f;
        }

       
    }
    void StopHunting()
    {
        physic.velocity = new Vector2(0, 0); Indiansound.Play();
    }
}

