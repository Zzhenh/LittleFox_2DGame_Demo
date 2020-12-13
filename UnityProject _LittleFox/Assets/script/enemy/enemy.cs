using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    protected Animator anim;
    protected Rigidbody2D rb;
    protected Collider2D col2;
    protected AudioSource audioSource;
    protected virtual void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        col2 = GetComponent<Collider2D>();
        audioSource = GetComponent<AudioSource>();
    }

    public void death()
    {
        GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject);
    }

    public void jumpOn()
    {
        audioSource.Play();

        anim.SetTrigger("death");
    }
}
