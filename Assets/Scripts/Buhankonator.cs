using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Buhankonator : Entity
{

    private float speed = 5.5f;
    private Vector3 dir;
    private SpriteRenderer sprite;

    private void start()
    {
        dir = transform.right;

    }
    private void update()
    {
        Ride();
    }
    private void Ride()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position + transform.up * 0.1f + transform.right * dir.x * 0.7f, 0.1f);

        if (colliders.Length > 0)
            dir *= -1f;
        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, Time.deltaTime);
    }
}

