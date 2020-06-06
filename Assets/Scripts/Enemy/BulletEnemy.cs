using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : MonoBehaviour
{
    public float speed = 0.5f;
    public Rigidbody2D rb;
    GameObject target;
    Lifes lifes;
    Vector2 moveDir;



    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitSeconds(3));
        target = GameObject.FindGameObjectWithTag("Player");
        lifes = target.GetComponent<Lifes>();
        moveDir = (target.transform.position - transform.position).normalized * speed;
        rb.velocity = new Vector2(moveDir.x, moveDir.y);

    }

    public IEnumerator WaitSeconds(int i)
    {
        yield return new WaitForSeconds(i);
        this.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D colider)
    {
        if (colider.tag == "Ground")
        {
            this.gameObject.SetActive(false);
        }
        if(colider.tag == "Player")
        {
            lifes.TakeDamage(1);
            this.gameObject.SetActive(false);              
        }
    }
}
