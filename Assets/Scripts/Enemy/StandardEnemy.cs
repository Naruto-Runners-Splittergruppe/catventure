using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardEnemy : MonoBehaviour
{
    private Lifes lifes;
    private GameObject player;

    public int health;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        lifes = player.GetComponent<Lifes>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D colider)
    {
        if (colider.tag == "Player" && lifes.Invicible == false)
        {
            //Gameobject player = Gameobject.FindGameobjectWithTag("Player");
            //PolygonCollider2D pc2d = player.GetComponent<PolygonCollider2D>();
            lifes.Invicible = true;
            lifes.Health--;            
            StartCoroutine("WaitSeconds", 3);           
        }
    } 
    
    private IEnumerator WaitSeconds(int i)
    {
        yield return new WaitForSeconds(i);
        lifes.Invicible = false;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }
}
