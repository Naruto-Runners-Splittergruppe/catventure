using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMovement : MonoBehaviour {

    // movement Variables
    public float movementSpeed = 1f;
    private Vector2 movement;
    private Rigidbody2D rb2d;
    
    // groundCheck
    public LayerMask groundLayer;

    // Jump Variables
    public float jumpVelocity = 1f;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    // RayCast length
    public float rayCastLength = 0.25f;

    // Start is called before the first frame update
    void Start() {

        rb2d = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {

        movement.x = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && IsGrounded()) {
            this.GetComponent<Rigidbody2D>().velocity = Vector2.up * jumpVelocity;
        }

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

    bool IsGrounded() {
        return Physics2D.Raycast(transform.position, Vector2.down, rayCastLength, groundLayer);
        
    }
}
