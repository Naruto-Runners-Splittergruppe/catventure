using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : MonoBehaviour
{
    public float speed = 0.5f;
    public Rigidbody2D rb;



    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed / 2 + transform.up * speed * 3 / 4;
        StartCoroutine(WaitSeconds(3));
    }

    public IEnumerator WaitSeconds(int i)
    {
        yield return new WaitForSeconds(i);
        this.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D colider)
    {
        if (colider.tag == "Player" || colider.tag == "Ground")
        {
            this.gameObject.SetActive(false);
        }
    }
}
