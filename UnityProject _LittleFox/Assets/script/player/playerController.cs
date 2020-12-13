using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class playerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    public float speed;
    public float jumpForce;
    public LayerMask ground;
    public Collider2D col2,col3;
    public int cherry,gem;
    public Text c_num,g_num;
    private bool isHurt;
    public AudioSource jumpAudio,hurtAudio,getAudio;
    public Transform cellingCheck;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isHurt)
        {
            Movemont();
        }
        switchAnim();
    }

    private void Update()
    {
        JumpAndCrouch();
    }
    //角色移动
    void Movemont()
    {
        float flagOfHorizontal = Input.GetAxis("Horizontal");
        float facedDirection = Input.GetAxisRaw("Horizontal");

        if (flagOfHorizontal != 0)
        {
            rb.velocity = new Vector2(flagOfHorizontal * speed*Time.fixedDeltaTime, rb.velocity.y);
           
            anim.SetFloat("running", Mathf.Abs(flagOfHorizontal * speed * Time.fixedDeltaTime));
            
        }
        else
        {
            anim.SetFloat("running", 0);
        }

        if (facedDirection != 0)
        {
            transform.localScale = new Vector3(facedDirection, 1, 1);
        }

       
    }
    //动画切换
    void switchAnim()
    {
        if (rb.velocity.y < 0.1f && !col2.IsTouchingLayers(ground))
        {
            anim.SetBool("falling", true);
        }
        if (anim.GetBool("jumping"))
        {
            if (rb.velocity.y < 0)
            {
                anim.SetBool("jumping", false);
                anim.SetBool("falling", true);
            }
        }
        else if (isHurt)
        {
            anim.SetBool("hurting", true);
            anim.SetFloat("running", 0);
            if (Mathf.Abs(rb.velocity.x) < 1f)
            {
                isHurt = false;
                anim.SetBool("hurting", false);
            }

        }
        else if (col2.IsTouchingLayers(ground))
        {
            anim.SetBool("falling", false);
        }
    }
    //收集物品
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "deadline")
        {
            GetComponent<AudioSource>().enabled = false;
            Invoke("dead", 2f);
        }
        else {
            collection cle = collision.gameObject.GetComponent<collection>();
            if (collision.tag == "cherry")
                {
                    cle.isGet();
                    cherry = +1;
                    c_num.text = cherry.ToString();
                }
                if (collision.tag == "gem")
                {
                    cle.isGet();
                    gem = +1;
                    g_num.text = gem.ToString();
                }
        }
        
    }
    //碰到敌人
    private void OnCollisionEnter2D(Collision2D collision)
    {
        enemy en = collision.gameObject.GetComponent<enemy>();
        if (collision.gameObject.tag == "enemy")
        {
            if (anim.GetBool("falling") && (transform.position.y - collision.gameObject.transform.position.y >= 1f))
            {
                en.jumpOn();
                rb.velocity = new Vector2(rb.velocity.x, jumpForce *0.8f *Time.fixedDeltaTime);
                anim.SetBool("jumping", true);
            }else if (transform.position.x<collision.gameObject.transform.position.x)
            {
                rb.velocity = new Vector2(-5, rb.velocity.y);
                hurtAudio.Play();
                isHurt = true;
            }else if (transform.position.x > collision.gameObject.transform.position.x)
            {
                rb.velocity = new Vector2(5, rb.velocity.y);
                hurtAudio.Play();
                isHurt = true;
            }
        }
    }

    void JumpAndCrouch()
    {
        if (Input.GetButtonDown("Jump") && col2.IsTouchingLayers(ground) && !Input.GetButton("Crouch"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce * Time.fixedDeltaTime);
            jumpAudio.Play();
            anim.SetBool("jumping", true);
        }

        if (!Physics2D.OverlapCircle(cellingCheck.position, 0.1f, ground))
        {
            if (Input.GetButton("Crouch"))
            {
                anim.SetBool("crouching", true);
                col3.enabled = false;
            }
            else
            {
                anim.SetBool("crouching", false);
                col3.enabled = true;
            }
        }
    }
    void dead()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
