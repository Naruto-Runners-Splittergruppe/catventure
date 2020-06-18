using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    public float speed;
    public bool MoveLeft;
    private float temp;

    private bool touchingGround = false;

    private Rigidbody2D rb2d;

    void Start() {

        temp = speed;

        rb2d = this.GetComponent<Rigidbody2D>();
    }


    // Update is called once per frame
    void Update()
    {
       if (MoveLeft)
        {
            transform.Translate(-2 * Time.deltaTime * speed, 0, 0);
            transform.localScale = new Vector2(2, 2);
        } 
       else
        {
            transform.Translate(2 * Time.deltaTime * speed, 0, 0);
            transform.localScale = new Vector2(-2, 2);
        }

       if (rb2d.velocity.y < 0 && touchingGround) {
            rb2d.velocity *= -1;
        }
    }

    void OnTriggerStay2D(Collider2D trig) {

        if (trig.CompareTag("Ground")) {
            touchingGround = true;
        }

        if (trig.CompareTag("Player"))
        {
            Lifes player = trig.GetComponent<Lifes>();
            player.TakeDamage(1);
            speed = 0;
            rb2d.velocity = Vector2.zero;
            rb2d.mass = rb2d.mass * 10;
        }
    }

    void OnTriggerEnter2D(Collider2D trig)
    {
        if (trig.CompareTag("turn"))
        {
            if (MoveLeft)
            {
                MoveLeft = false;
            }
            else
            {
                MoveLeft = true;
            }
        }       
    }

    void OnTriggerExit2D(Collider2D trig) {
        
        if (trig.CompareTag("Player")) {
            speed = temp;
        }

        if (trig.CompareTag("Ground")) {
            touchingGround = false;
        }
    }
}
