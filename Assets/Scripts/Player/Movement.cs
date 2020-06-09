using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed;
    public float jumpPower;
    float normalJumpPower;
    float loweredJumpPower;
    private bool touchingGround;
    private Rigidbody2D rb2d;
    private bool rotate;
    private bool inWater;
    private float slowedSpeed;
    private float normalSpeed;
    public Lifes lifes;
    Vector2 normalGravity;
    Vector2 loweredGravity;
    float breathInSec = 3;
    float time;

    public SpriteRenderer mySprite;
    public Color underWaterColor;
    public Color regualColor;

    Vector2 grav;

    public bool MovementLocked { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();        
        MovementLocked = false;
        slowedSpeed = speed/2;
        normalSpeed = speed;


        grav = Physics2D.gravity;

        normalJumpPower = jumpPower;
        loweredJumpPower = jumpPower / 2;
        normalGravity = Physics2D.gravity;
        loweredGravity = new Vector2(0, -0.75f);
        time = breathInSec;

    }


    void Update()
    {
        if (!MovementLocked)
        {
            HorizontalMovement();
            VerticalMovement();
        }
        if (inWater)
        {
            mySprite.color = underWaterColor;
            jumpPower = loweredJumpPower; 
            speed = slowedSpeed;
            Physics2D.gravity = loweredGravity;

            if(time > 0)
            {
                time -= Time.deltaTime;
            }
            else
            {
                lifes.TakeDamage(1);
                time = breathInSec;
            }   
        }
        else
        {
            jumpPower = normalJumpPower;
            speed = normalSpeed;
            Physics2D.gravity = normalGravity;
            time = breathInSec;
            mySprite.color = regualColor;
        }
    }

    void HorizontalMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");

        if (horizontal == 0 && touchingGround && !Input.GetButtonDown("Jump")) {
            rb2d.velocity = Vector2.zero;
            Physics2D.gravity = Vector2.zero;
        }
        else {
            Physics2D.gravity = grav;
        }

        if (horizontal > 0)
        {
            rb2d.velocity = new Vector2(horizontal * speed, rb2d.velocity.y);
            if (rotate)
            {
                transform.Rotate(0, 180, 0);
                rotate = false;
            }

        } else if (horizontal < 0)
        {
            rb2d.velocity = new Vector2(horizontal * speed, rb2d.velocity.y);
            if (!rotate)
            {
                transform.Rotate(0, 180, 0);
                rotate = true;
            }
        }
    }

    void VerticalMovement()
    {
        if (Input.GetButtonDown("Jump") && touchingGround)
        {
            rb2d.AddForce(Vector2.up * jumpPower);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {        
        if (col.CompareTag("Water"))
        {
            inWater = true;
            rb2d.velocity = loweredGravity;
        }
    }
    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Ground"))
        {
            touchingGround = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Ground"))
        {
            touchingGround = false;
        }
        if (col.CompareTag("Water"))
        {
            inWater = false;
            rb2d.velocity = normalGravity;
        }
    }
    public void resetToNormal()
    {
        speed = normalSpeed;
        jumpPower = normalJumpPower;
        Physics2D.gravity = normalGravity;
        inWater = false;
        breathInSec = 3;
    }
}
