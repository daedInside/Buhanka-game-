using System.Collections;

using System.Collections.Generic;
using UnityEngine;

public class Buhankonator : Entity
{
    
    private Rigidbody2D physic;
    public Transform Hero;
    public float speed;
    public float agroDistance;

    private void Start()
    {
        physic = GetComponent<Rigidbody2D>();

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
        Debug.Log("Distance: " + distToHero);
    }
    void StartHunting()
    {


        if (Hero.position.x < transform.position.x)
        {
            physic.velocity = new Vector2(-speed, 0);
            transform.localScale = new Vector2(1, 1);
        }
        else if (Hero.position.x > transform.position.x)
        {
            physic.velocity = new Vector2(speed, 0);
            transform.localScale = new Vector2(-1, 1);
        }
                }
    void StopHunting()
    {
        physic.velocity = new Vector2(0, 0);
    }
}

