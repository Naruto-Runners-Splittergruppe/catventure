using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform player;
    public Vector2 offset;

    private Vector3 originalPosition;

    private void Start()
    {
        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        originalPosition = transform.position;
    }

    // Update is called once per frame
    void Update() {

        transform.position = new Vector3(player.position.x + offset.x, player.position.y + offset.y, originalPosition.z);
    }
}