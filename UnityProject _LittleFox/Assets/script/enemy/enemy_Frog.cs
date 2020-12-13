using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_Frog : enemy
{

    public Transform liftPoint, rightPoint;
    private bool faceLeft = true;
    public float speed,jumpForce;
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
        switchAnim();
    }

    void Movement()
    {
        if (faceLeft)
        {
            if (col2.IsTouchingLayers(ground))
            {
                anim.SetBool("jumping", true);
                rb.velocity = new Vector2(-speed, jumpForce);
            }
            
            if (transform.position.x < lift_x)
            {
                rb.velocity = new Vector2(0, 0);
                transform.localScale = new Vector3(-1, 1, 1);
                faceLeft = false;
            }
        }
        else
        {
            if (col2.IsTouchingLayers(ground))
            {
                anim.SetBool("jumping", true);
                rb.velocity = new Vector2(speed, jumpForce);
            }
            if (transform.position.x > right_x)
            {
                rb.velocity = new Vector2(0, 0);
                transform.localScale = new Vector3(1, 1, 1);
                faceLeft = true;
            }
        }
    }

    void switchAnim()
    {
        if (anim.GetBool("jumping"))
        {
            if (rb.velocity.y < 0.1f)
            {
                anim.SetBool("jumping", false);
                anim.SetBool("falling", true);
            }
            
        }
        if (col2.IsTouchingLayers(ground) && anim.GetBool("falling"))
        {
            anim.SetBool("falling", false);
           
        }
       
    }

    
}
