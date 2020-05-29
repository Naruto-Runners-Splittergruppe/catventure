using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {

    public float speed = 1f;
    public float minDistance = 0.5f;
    public float maxDistance = 3f;
    private Transform target;

    public float DistanceAboveHead = 1f;
    private Vector2 followAboveHead;

    void Start() {

        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void Update() {

        followAboveHead = new Vector2(target.position.x, target.position.y + DistanceAboveHead);

        if (Vector2.Distance(transform.position, followAboveHead) > minDistance && // if you dont want directly above head
            Vector2.Distance(transform.position, followAboveHead) < maxDistance) {
            transform.position = Vector2.MoveTowards(transform.position, followAboveHead, speed * Time.deltaTime);
            // todo get faster when flying up
        }
    }
}