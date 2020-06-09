using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMovement : MonoBehaviour {

    // movement variables
    public float movementSpeed = 1f;
    private Vector2 movement;
    private Rigidbody2D rb2d;
    public float jumpVelocity = 200f;
    
    // checks for Ground Layer
    public LayerMask groundLayer;

    // fall amplifier
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    // Water behaviour
    private bool inWater = false;
    private Vector2 waterResistance;
    public float timeToBreath = 3f;
    private float timeLeftUnderwater;
    // slowed movement
    private float slowedJumpVelocity;
    private float slowedMovement;
    private float normalJumpVelocity;
    private float normalMovement;
    // Water colour
    public SpriteRenderer mySprite;
    public Color underWaterColor;
    public Color regularColor;

    private Lifes lifes;
    bool touchingGround = false;
    Vector2 regularGravity;

    // Start is called before the first frame update
    void Start() {

        rb2d = this.GetComponent<Rigidbody2D>();
        regularGravity = Physics2D.gravity;

        waterResistance = new Vector2(0, -0.75f);
        timeLeftUnderwater = timeToBreath;

        normalJumpVelocity = jumpVelocity;
        normalMovement = movementSpeed;
        slowedJumpVelocity = jumpVelocity / 2;
        slowedMovement = movementSpeed / 2;
    }

    // Update is called once per frame
    void Update() {

        movement.x = Input.GetAxisRaw("Horizontal");

        // Water movement
        if (inWater) {
            mySprite.color = underWaterColor;
            jumpVelocity = slowedJumpVelocity;
            movementSpeed = slowedMovement;
            Physics2D.gravity = waterResistance;

            if (timeLeftUnderwater > 0) {
                timeLeftUnderwater -= Time.deltaTime;
            }
            else {
                timeLeftUnderwater = timeToBreath;
                lifes.TakeDamage(1);
            }
        }
        else if (!inWater) {
            mySprite.color = regularColor;
            jumpVelocity = normalJumpVelocity;
            movementSpeed = normalMovement;
            Physics2D.gravity = regularGravity;
            timeLeftUnderwater = timeToBreath;
        }

        // prevents Player from Sliding down Slopes
        if (movement.x == 0 && touchingGround && !Input.GetButtonDown("Jump")) {
            rb2d.velocity = Vector2.zero;
            Physics2D.gravity = Vector2.zero;
        }
        else {
            Physics2D.gravity = regularGravity;
        }
        
        // to Jump
        if (Input.GetButtonDown("Jump") && touchingGround) {
            rb2d.AddForce(Vector2.up * jumpVelocity);
        }

        // high- and low- Jump
        if (rb2d.velocity.y < 0) {
            rb2d.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            //Debug.Log("Jumped");
        }
        else if (rb2d.velocity.y > 0 && !Input.GetButton("Jump")) {
            rb2d.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
            //Debug.Log("Released");
        }
    }

    void FixedUpdate() {

        rb2d.position += movement * movementSpeed * Time.fixedDeltaTime;
    }

    void OnTriggerStay2D(Collider2D col) {
        if (col.CompareTag("Ground")) {
            touchingGround = true;
        }
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.CompareTag("Water")) {
            inWater = true;
        }
    }

    void OnTriggerExit2D(Collider2D col) {
        if (col.CompareTag("Ground")) {
            touchingGround = false;
        }
        if (col.CompareTag("Water")) {
            inWater = false;
        }
    }
}
