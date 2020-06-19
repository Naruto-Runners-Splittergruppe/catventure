using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestMovement : MonoBehaviour {

    // movement variables
    public float movementSpeed = 1f;
    private Vector2 movement;
    private Rigidbody2D rb2d;
    public float jumpVelocity = 200f;
    private bool rotate = true;

    // fall amplifier
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    // Water behaviour
    private bool inWater = false;
    public float timeToBreath = 0.5f;
    private float timeLeftUnderwater;
    // slowed movement
    private bool justEntered = false;
    public float waterResistance = 0.92f;
    private Vector2 gravityInWater;
    // Water colour
    public SpriteRenderer mySprite;
    public Color underWaterColor;
    public Color regularColor;

    private Lifes lifes;
    private GameObject player;
    bool touchingGround = false;
    Vector2 regularGravity;

    public CircleCollider2D cc2d;

    // Collectibles
    private int score = 0;

    // Start is called before the first frame update
    void Start() {

        player = GameObject.FindGameObjectWithTag("Player");
        lifes = player.GetComponent<Lifes>();
        rb2d = this.GetComponent<Rigidbody2D>();
        regularGravity = Physics2D.gravity;

        gravityInWater = new Vector2(0, -3);
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
                movementSpeed /= 2;
                Physics2D.gravity = gravityInWater;
                justEntered = true;
            }

            if (timeLeftUnderwater > 0) { // if Player still has breath left
                timeLeftUnderwater -= Time.deltaTime; // breath gets reduced by elapsed Time.deltaTime
            }
            else {
                timeLeftUnderwater = timeToBreath; // Player got damaged, breath restored
                lifes.TakeDamage(1); // Player has drowned, takes 1 damage
            }

            rb2d.velocity *= waterResistance; // reduces fallspeed by waterResistance * velocity every frame.
        }
        else if (!inWater) {

            if (justEntered) {
                mySprite.color = regularColor;
                movementSpeed *= 2;
                Physics2D.gravity = regularGravity;

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

        // rotation
        if (movement.x < 0 && rotate) {
            transform.Rotate(0, 180, 0);
            rotate = false;
        }
        else if (movement.x > 0 && !rotate) {
            transform.Rotate(0, 180, 0);
            rotate = true;
        }

        // prevents getting stuck on Slopes
        if (rb2d.velocity.y < 0 && touchingGround) {
            rb2d.velocity *= -1;
        }

    }
    // runs every few frames, more efficient, used 
    void FixedUpdate() {

        rb2d.position += movement * movementSpeed * Time.fixedDeltaTime;
    }

    void OnTriggerStay2D(Collider2D col) {
        if (col.CompareTag("Ground") && cc2d.IsTouching(col)) {
            touchingGround = true;
        }
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.CompareTag("Water")) {
            inWater = true;
        }

        if (col.tag == "Collectible") {
            score++;
            Destroy(col.gameObject);
        }

        if (col.tag == "DeathPit") {
            player.GetComponent<TestMovement>().resetToNormal();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    void OnTriggerExit2D(Collider2D col) {
        if (col.CompareTag("Ground") && !cc2d.IsTouching(col)) {
            touchingGround = false;
        }
        if (col.CompareTag("Water")) {
            inWater = false;
        }
    }

    public void resetToNormal()
    {
        Physics2D.gravity = regularGravity;
        inWater = false;
    }
}
