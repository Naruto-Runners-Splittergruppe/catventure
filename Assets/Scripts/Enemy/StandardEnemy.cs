using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardEnemy : MonoBehaviour
{
    private Lifes lifes;
    private GameObject player;
    public SpriteRenderer mySprite;
    public Color flashColor;
    public Color regualColor;
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
        health = 3;
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(Flashing());
    }

    private void OnTriggerEnter2D(Collider2D colider)
    {
        if (colider.tag == "Player" && lifes.Invicible == false)
        {
            //Gameobject player = Gameobject.FindGameobjectWithTag("Player");
            //PolygonCollider2D pc2d = player.GetComponent<PolygonCollider2D>();
            lifes.TakeDamage(1);          
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
        gameObject.GetComponent<FollowPlayer>().enabled = false;
        gameObject.GetComponent<PolygonCollider2D>().enabled = false;
        yield return new WaitForSeconds(i);
        gameObject.GetComponent<EnemyMovement>().enabled = true;
        gameObject.GetComponent<FollowPlayer>().enabled = true;
        gameObject.GetComponent<PolygonCollider2D>().enabled = true;
        hit = false;
        if (health < 1)
        {
            this.gameObject.SetActive(false);
        }
        
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
            mySprite.color = regualColor;
            yield return new WaitForSeconds(flashDur);
        }
    }
}
