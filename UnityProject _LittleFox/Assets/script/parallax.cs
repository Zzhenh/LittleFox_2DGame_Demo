using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parallax : MonoBehaviour
{
    public Transform cam;
    private float s_X,s_Y;
    public float rate;
    public bool lockY;
    void Start()
    {
        s_X = transform.position.x;
        s_Y = transform.position.y;
    }

    void Update()
    {
        if (lockY)
        {
            transform.position = new Vector2(s_X + cam.position.x * rate, transform.position.y);
        }
        else
        {
            transform.position = new Vector2(s_X + cam.position.x * rate, s_Y + cam.position.y * rate);
        }
    }
}
