using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tire : Entity
{
    [SerializeField] private int lives = 2;
    
    private void OnCollisionEnter2D(Collision2D collision)
    { 
        if(collision.gameObject==Hero.Instance.gameObject)
        {
            Hero.Instance.GetDamage();
            lives--;
            Debug.Log("on tire" + lives);
        }
        if (lives < 1)
            Die();
            
    }
}


