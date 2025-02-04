﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardEnemy : MonoBehaviour
{
    private Lifes lifes;
    private GameObject player;
    public SpriteRenderer mySprite;
    public Color flashColor;
    public Color regularColor;
    public float flashDur;
    private bool hit;

    public bool Hit
    {
        get { return hit; }
        set { hit = value; }
    }

    public int health;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        lifes = player.GetComponent<Lifes>();
        flashDur = 0.07f;
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(Flashing());
    }

    private void OnTriggerEnter2D(Collider2D colider)
    {
        if(colider.tag == "Player")
        {
            if (Lifes.lifes.Invicible == false && Vector2.Angle(Lifes.player.transform.position - this.transform.position, this.transform.up) >= 50)
            {
                Lifes.lifes.Invicible = true;
                Lifes.lifes.TakeDamage(1);

            }
            else if (Vector2.Angle(Lifes.player.transform.position - this.transform.position, this.transform.up) < 50)
            {
                colider.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 250);
                TakeDamage(1);
            }
        }        
        if(colider.tag == "Bullet")
        {
            TakeDamage(1);
        }
    }   

    public IEnumerator WaitSeconds(float i)
    {
        hit = true;        
        gameObject.GetComponent<EnemyMovement>().enabled = false;
        gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(i);
        if (health < 1)
        {
            this.gameObject.SetActive(false);
        }
        gameObject.GetComponent<EnemyMovement>().enabled = true;
        gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        hit = false;       
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        StartCoroutine("WaitSeconds", .5f);
        
    }


    private IEnumerator Flashing()
    {
        while (hit)
        {
            mySprite.color = flashColor;
            yield return new WaitForSeconds(flashDur);
            mySprite.color = regularColor;
            yield return new WaitForSeconds(flashDur);
        }
    }
}
