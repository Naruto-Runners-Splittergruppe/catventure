using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour {

    public float speed;
    public float distance;

    private bool movingRight = false;

    public Transform groundCheck;

    // Update is called once per frame
    void Update() {

        transform.Translate(Vector2.right * speed * Time.deltaTime);

        RaycastHit2D groundInfo = Physics2D.Raycast(groundCheck.position, Vector2.down, distance);
        if (!groundInfo.collider) {
            if (movingRight) {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
            }
            else {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
            }
        }
    }
}
