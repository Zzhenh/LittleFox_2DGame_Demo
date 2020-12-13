using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_eagle : enemy
{
    public Transform topPoint, buttomPoint;
    private bool isUp = true;
    public float speed;
    private float top_y, buttom_y;

    protected override void Start()
    {
        base.Start();
        transform.DetachChildren();
        top_y = topPoint.position.y;
        buttom_y = buttomPoint.position.y;
        Destroy(topPoint.gameObject);
        Destroy(buttomPoint.gameObject);
    }

    void Update()
    {
        Movement();
    }

    void Movement()
    {
        if (isUp)
        {
            rb.velocity = new Vector2(rb.velocity.x, speed);
            if (transform.position.y > top_y)
            {
                isUp = false;
            }
        }
        else
        {
            rb.velocity = new Vector2(rb.velocity.x, -speed);
            if (transform.position.y < buttom_y)
            {
                isUp = true;
            }
        }
    }

   
}
