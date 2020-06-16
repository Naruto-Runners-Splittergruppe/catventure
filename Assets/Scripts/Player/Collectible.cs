using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public int score;

    void Start() {

        score = 0;
    }

    void OnTriggerEnter(Collider other) {
        
        if (other.tag == "Collectible") {

            score++;
            Destroy(other.gameObject);
        }
    }
}
