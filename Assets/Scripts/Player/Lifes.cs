using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lifes : MonoBehaviour
{
    public GameObject heart1, heart2;
    
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
        health = 2;
        invicible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(health == 2)
        {
            heart1.gameObject.SetActive(true);
            heart2.gameObject.SetActive(true);
        } else if(health == 1)
        {
            heart1.gameObject.SetActive(true);
            heart2.gameObject.SetActive(false);
        } else if(health == 0)
        {
            heart1.gameObject.SetActive(false);
            heart2.gameObject.SetActive(false);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }     
}
