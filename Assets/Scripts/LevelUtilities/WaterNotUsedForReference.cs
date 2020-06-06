using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{

    private GameObject player;
    Movement movement;
    bool stillInWater;
    Lifes lifes;
    // Start is called before the first frame update
    void Start()
    {
        stillInWater = false;
        player = GameObject.FindGameObjectWithTag("Player");
        movement = player.GetComponent<Movement>();
        lifes = player.GetComponent<Lifes>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D colider)
    {
        if (colider.tag == "Player")
        {
            stillInWater = true;
            movement.speed = movement.speed / 2;
            StartCoroutine(WaitSeconds(3));
        }
    }

    private void OnTriggerExit2D (Collider2D colider)
    {
        if (colider.tag == "Player")
        {
            stillInWater = false;
            movement.speed = movement.speed * 2;
        }
    }
    public IEnumerator WaitSeconds(int i)
    {
        yield return new WaitForSeconds(i);
        if (stillInWater)
        {
            lifes.TakeDamage(1);
        }
    }
}

