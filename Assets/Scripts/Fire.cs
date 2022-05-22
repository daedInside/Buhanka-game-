using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : Entity
{
  
    void Start()
    {
        lives = 1;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == Hero.Instance.gameObject)
        {
            Hero.Instance.GetDamageFire();
            Hero.Instance.DieHero();
           

        }
       

    }
}
