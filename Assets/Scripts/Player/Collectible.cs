using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Collectible : MonoBehaviour
{
    public int score;
    public Text text;
    public GameObject catnip;
    public BoxCollider2D colider;
    bool differentCol;

    void Start() {

        score = 0;
        differentCol = true;
    }

    private void Update()
    {
        if(score == 0)
        {
            text.gameObject.SetActive(false);
            catnip.gameObject.SetActive(false);            
        }
        if(score >= 1)
        {
            catnip.SetActive(true);
            text.gameObject.SetActive(true);
            text.text = ": " + score;
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        
        if (other.CompareTag("Collectible") && differentCol) {
            Destroy(other);
            score++;
            differentCol = false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Collectible"))
        {
            differentCol = true;
        }
    }
}
