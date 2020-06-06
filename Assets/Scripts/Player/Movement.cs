using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed;
    public float jumpPower;
    private bool touchingGround;
    private Rigidbody2D rb2d;
    private bool rotate;
    private bool inWater;
    private float slowedSpeed;
    private float normalSpeed;
    public Lifes lifes;

    public bool MovementLocked { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();        
        MovementLocked = false;
        slowedSpeed = speed/2;
        normalSpeed = speed;
    }


    void Update()
    {
        if (!MovementLocked)
        {
            HorizontalMovement();
            VerticalMovement();
        }
        if (inWater && !lifes.Invicible)
        {
            speed = slowedSpeed;
            StartCoroutine(WaitSeconds(3));
        }
        else
        {
            speed = normalSpeed;
        }
    }

    void HorizontalMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
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
        if (col.CompareTag("Ground"))
        {
            touchingGround = true;
        }
        if (col.CompareTag("Water"))
        {
            inWater = true;
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Ground"))
        {
            touchingGround = true;
        }
        if (col.CompareTag("Water"))
        {
            inWater = true;
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
        }
    }

    public IEnumerator WaitSeconds(int i)
    {
        yield return new WaitForSeconds(i);
        if (inWater)
        {
            lifes.TakeDamage(1);
        }
    }
}
