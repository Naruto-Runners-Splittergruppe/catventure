using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FollowPlayer : MonoBehaviour {

    public Transform player;
    public float walkingDistance = 10.0f; // Distance it will follow player
    public float smoothTime = 10.0f; // Time it takes to get to player
    
    private Vector3 smoothVelocity = Vector3.zero; // stores velocity of enemy
    
    void Update() {
        //Look at the player
        Quaternion rotation = Quaternion.LookRotation(player.transform.position - transform.position, transform.TransformDirection(Vector3.up));
        transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);
        //Calculate distance between player
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance < walkingDistance) {
            //Move the enemy towards the player with smoothdamp
            transform.position = Vector3.SmoothDamp(transform.position, player.position, ref smoothVelocity, smoothTime);
        }
    }
}