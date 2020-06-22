using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationHandler : MonoBehaviour
{
    private Animator anim;
    private TestMovement playerMovement;

    void Start()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<TestMovement>();
    }

    void FixedUpdate()
    {
        if (!playerMovement.IsJumping)
        {
            anim.SetFloat("speed", Mathf.Abs(playerMovement.Movement.x));
        }

        anim.SetBool("jumping", playerMovement.IsJumping);
    }
}
