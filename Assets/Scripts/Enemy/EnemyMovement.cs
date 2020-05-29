using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    public float speed;
    public bool MoveLeft;
    private float temp;

    private Transform target;


    void Start() {

        temp = speed;
    }


    // Update is called once per frame
    void Update()
    {
       if (MoveLeft)
        {
            transform.Translate(-2 * Time.deltaTime * speed, 0, 0);
            transform.localScale = new Vector2(2, 2);
        } 
       else
        {
            transform.Translate(2 * Time.deltaTime * speed, 0, 0);
            transform.localScale = new Vector2(-2, 2);
        }
    }

    void OnTriggerEnter2D(Collider2D trig)
    {
        if (trig.gameObject.CompareTag("turn"))
        {
            if (MoveLeft)
            {
                MoveLeft = false;
            }
            else
            {
                MoveLeft = true;
            }
        }

        if (trig.gameObject.CompareTag("Player")) {
            speed = 0;
        }
    }

    void OnTriggerExit2D(Collider2D trig) {
        
        if (trig.gameObject.CompareTag("Player")) {
            speed = temp;
        }
    }
}
