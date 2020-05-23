using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardEnemy : MonoBehaviour
{

    private int health;
    public int Health
    {
        get { return health; }
        set { health = value; }
    }
    // Start is called before the first frame update
    void Start()
    {
        health = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            this.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D colider)
    {
        if (colider.tag == "Player")
        {
           if(Lifes.lifes.Invicible == false && Vector2.Angle(Lifes.player.transform.position - this.transform.position, this.transform.up) >= 50)
            {
                Lifes.lifes.Invicible = true;
                Lifes.lifes.TakeDamage(1);
                
            }
            else if(Vector2.Angle(Lifes.player.transform.position - this.transform.position, this.transform.up) < 50)
            {
                colider.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 200);
                health--;
            }
        }
        if (colider.tag == "Bullet")
            {
            health--;
            }
        
    }

<<<<<<< HEAD
    public void TakeDamage()
    {
        Destroy(gameObject);
        GetComponent<Collider2D>().enabled = false; // disable BoxCollider
        this.enabled = false;                       // disable enemy script
    }
=======
    

    

    
>>>>>>> 83cced1c9a55d65f3c82dce39ea251efb0c62608
}
