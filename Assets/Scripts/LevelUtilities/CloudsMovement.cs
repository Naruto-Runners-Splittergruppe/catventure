using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CloudMovement { Left, Right }

public class CloudsMovement : MonoBehaviour
{
    public float minRandomStopIntervall = 1;
    public float maxRandomStopIntervall = -1;
    public float despawnWhenReachedX = 0;
    public float movementSpeed = 5;
    public float maxMovementSpeed = 5;
    public CloudMovement movement;

    private Rigidbody2D rb2d;
    private int moveTo = 1;

    // Start is called before the first frame update
    void Start()
    {
        //Flip Sprite
        if(Random.Range(0,2) >= 1)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }

        if(minRandomStopIntervall < 0)
        {
            minRandomStopIntervall = 0;
            maxRandomStopIntervall = 0;
        }

        if(maxRandomStopIntervall < minRandomStopIntervall)
        {
            maxRandomStopIntervall = minRandomStopIntervall;
        }

        if(movement == CloudMovement.Left)
        {
            moveTo = -1;
        }

        rb2d = this.GetComponent<Rigidbody2D>();

        StartCoroutine("MoveCloud");
    }

    private IEnumerator MoveCloud()
    {
        while(transform.position.x > despawnWhenReachedX)
        {
            float intervall = Random.Range(minRandomStopIntervall, maxRandomStopIntervall);

            if (Mathf.Abs(rb2d.velocity.x) < maxMovementSpeed)
            {
                rb2d.AddForce(new Vector2(movementSpeed * moveTo, 0));
            }
            else
            {
                rb2d.velocity = new Vector2(maxMovementSpeed * moveTo,0);
            }

            yield return new WaitForSeconds(intervall*100);
        }

        Destroy(gameObject);
    }
}
