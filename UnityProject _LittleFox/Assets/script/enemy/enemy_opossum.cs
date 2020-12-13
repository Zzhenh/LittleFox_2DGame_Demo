using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_opossum : enemy
{
    public Transform liftPoint, rightPoint;
    private bool faceLeft = true;
    public float speed;
    private float lift_x, right_x;
    public LayerMask ground;
    protected override void Start()
    {
        base.Start();
        transform.DetachChildren();
        lift_x = liftPoint.position.x;
        right_x = rightPoint.position.x;
        Destroy(liftPoint.gameObject);
        Destroy(rightPoint.gameObject);
    }

    void Update()
    {
        Movement();
    }

    void Movement()
    {
        if (faceLeft)
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);

            if (transform.position.x < lift_x)
            {
                rb.velocity = new Vector2(0, 0);
                transform.localScale = new Vector3(-1, 1, 1);
                faceLeft = false;
            }
        }
        else
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);

            if (transform.position.x > right_x)
            {
                rb.velocity = new Vector2(0, 0);
                transform.localScale = new Vector3(1, 1, 1);
                faceLeft = true;
            }
        }
    }
}
