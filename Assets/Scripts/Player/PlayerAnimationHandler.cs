using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationHandler : MonoBehaviour
{
    private Animator anim;
    private TestMovement playerMovement;
    private Rigidbody2D rb2d;

    void Start()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<TestMovement>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (playerMovement.enabled)
        {
            if (!playerMovement.IsJumping)
            {
                anim.SetFloat("speed", Mathf.Abs(playerMovement.Movement.x));
            }

            anim.SetBool("jumping", playerMovement.IsJumping);
        }
        else
        {
            if(Mathf.Abs(rb2d.velocity.y) > 0.1)
            {
                anim.SetBool("jumping", true);
            }
            else
            {
                anim.SetBool("jumping", false);
            }
            anim.SetFloat("speed", Mathf.Abs(rb2d.velocity.x));
        }
    }
}
