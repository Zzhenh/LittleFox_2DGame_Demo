using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collection : MonoBehaviour
{

    void death()
    {
        Destroy(gameObject);
    }
    public void isGet()
    {
        GetComponent<Animator>().Play("GetItem");
        FindObjectOfType<playerController>().getAudio.Play();
    }
}
