using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public Vector2 minSpawnPosition;
    public Vector2 maxSpawnPosition;
    [Header("Spawn interval in Seconds. For fixed time leave maySpawnInterval blank")]
    public float minSpawnInterval = 1;
    public float maxSpawnInterval;
    public GameObject objectToSpawn;
    public float spawnFixedAmount = -1;
    public float spawnNotMoreThan = -1;

    private bool stopSpawning = false;

    public List<GameObject> ListOfSpawnedObjects { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        ListOfSpawnedObjects = new List<GameObject>();

        if (minSpawnPosition == null)
        {
            minSpawnPosition = new Vector2(0, 0);
        }

        if(maxSpawnPosition == null)
        {
            maxSpawnPosition = minSpawnPosition;
        }

        if (maxSpawnInterval == 0 || maxSpawnInterval < minSpawnInterval)
        {
            maxSpawnInterval = minSpawnInterval;
        }

        if(spawnFixedAmount <= 0)
        {
            spawnFixedAmount = -1;
        }
        else
        {
            spawnNotMoreThan = spawnFixedAmount;
        }

        if(objectToSpawn == null)
        {
            Debug.LogError("No spawn object set, reverting to default");
            objectToSpawn = Resources.Load("Buttons/E") as GameObject;
        }
        else
        {
            StartCoroutine("Spawn");
        }
    }

    private IEnumerator Spawn()
    {
        int itemsSpawned = -2;

        if(spawnFixedAmount > 0)
        {
            itemsSpawned = 0;
        }

        while(!stopSpawning && itemsSpawned < spawnFixedAmount)
        {
            float spawnPositionX = Random.Range(minSpawnPosition.x, maxSpawnPosition.x);
            float spawnPositionY = Random.Range(minSpawnPosition.y, maxSpawnPosition.y);

            ListOfSpawnedObjects.RemoveAll(x => x == null);

            if (spawnNotMoreThan <= 0 || spawnNotMoreThan > ListOfSpawnedObjects.Count)
            {
                GameObject newObject = Instantiate(objectToSpawn, new Vector2(spawnPositionX, spawnPositionY), objectToSpawn.transform.rotation, transform);

                if (!newObject.activeSelf)
                {
                    newObject.SetActive(true);
                }

                ListOfSpawnedObjects.Add(newObject);

                if (spawnFixedAmount > 0)
                {
                    itemsSpawned++;
                }
            }

            float spawnAfterSeconds = Random.Range(minSpawnInterval,maxSpawnInterval);
            yield return new WaitForSeconds(spawnAfterSeconds);
        }
    }
}
