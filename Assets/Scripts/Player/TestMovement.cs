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
    private bool justEntered = false;
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
    }

    // Update is called once per frame
    void Update() {

        movement.x = Input.GetAxisRaw("Horizontal");

        // Water movement
        if (inWater) {
            // will only run once, when entering the Water not every Frame
            if (!justEntered) {
                mySprite.color = underWaterColor;
                Physics2D.gravity = waterResistance;
                movementSpeed /= 2;
                jumpVelocity /= 2;

                justEntered = true;
            }

            if (timeLeftUnderwater > 0) { // if Player still has breath left
                timeLeftUnderwater -= Time.deltaTime; // breath gets reduced by elapsed Time.deltaTime
            }
            else {
                timeLeftUnderwater = timeToBreath; // Player got damaged, breath restored
                lifes.TakeDamage(1);
            }
        }
        else if (!inWater) {

            if (justEntered) {
                mySprite.color = regularColor;
                Physics2D.gravity = regularGravity;
                movementSpeed *= 2;
                jumpVelocity *= 2;
                timeLeftUnderwater = timeToBreath; // Player out of Water, breath restored

                justEntered = false;
            }
            
            // prevents Player from Sliding down Slopes
            if (movement.x == 0 && touchingGround && !Input.GetButtonDown("Jump")) {
                rb2d.velocity = Vector2.zero;
                Physics2D.gravity = Vector2.zero;
            }
            else {
                Physics2D.gravity = regularGravity;
            }
        }
        
        // to Jump
        if (Input.GetButtonDown("Jump") && touchingGround) {
            rb2d.AddForce(Vector2.up * jumpVelocity);
        }

        // high- and low- Jump
        if (rb2d.velocity.y < 0) {
            rb2d.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb2d.velocity.y > 0 && !Input.GetButton("Jump")) {
            rb2d.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }
    // runs every few frames, more efficient
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
            rb2d.velocity = rb2d.velocity * 0.8f;
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
