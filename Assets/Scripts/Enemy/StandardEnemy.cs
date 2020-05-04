using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardEnemy : MonoBehaviour
{
    private Lifes lifes;
    private GameObject player;
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
            StartCoroutine("WaitSeconds", 10);           
        }
    } 
    
    private IEnumerator WaitSeconds(int i)
    {
        yield return new WaitForSeconds(i);
        lifes.Invicible = false;
    }
}
