﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lifes : MonoBehaviour
{
    public GameObject heart1, heart2;
    public Color flashColor;
    public Color regualColor;
    public float flashDur;
    public SpriteRenderer mySprite;
<<<<<<< HEAD
=======
    public static Lifes lifes;
    public static GameObject player;
>>>>>>> 83cced1c9a55d65f3c82dce39ea251efb0c62608

    private int health;
    public int Health
    {
        get { return health; }
        set { health = value; }
    }

    public bool invicible;
    public bool Invicible
    {
        get { return invicible; }
        set { invicible = value; }
    }



    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        lifes = player.GetComponent<Lifes>();
        health = 2;
        invicible = false;

    }

    // Update is called once per frame
    void Update()
    {
<<<<<<< HEAD
        if (health == 2)
=======
        //TODO Handle if heart1 || heart2 == null
        if(health == 2)
>>>>>>> 83cced1c9a55d65f3c82dce39ea251efb0c62608
        {
            heart1.gameObject.SetActive(true);
            heart2.gameObject.SetActive(true);
        }
        else if (health == 1)
        {
            heart1.gameObject.SetActive(true);
            heart2.gameObject.SetActive(false);
        }
        else if (health == 0)
        {
            heart1.gameObject.SetActive(false);
            heart2.gameObject.SetActive(false);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        StartCoroutine(Flashing());
    }

    private IEnumerator Flashing()
    {
        while (invicible)
        {
            mySprite.color = flashColor;
            yield return new WaitForSeconds(flashDur);
            mySprite.color = regualColor;
            yield return new WaitForSeconds(flashDur);
        }
    }

    public IEnumerator WaitSeconds(int i)
    {
        yield return new WaitForSeconds(i);
        lifes.Invicible = false;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        StartCoroutine("WaitSeconds", 3);
    }


}
