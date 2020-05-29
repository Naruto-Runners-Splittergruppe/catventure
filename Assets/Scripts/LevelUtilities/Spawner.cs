using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
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
        while(!stopSpawning)
        {
            float spawnPositionX = Random.Range(minSpawnPosition.x, maxSpawnPosition.x);
            float spawnPositionY = Random.Range(minSpawnPosition.y, maxSpawnPosition.y);

            if (spawnNotMoreThan <= 0 || spawnNotMoreThan <= ListOfSpawnedObjects.Count)
            {
                ListOfSpawnedObjects.Add(Instantiate(objectToSpawn, new Vector2(spawnPositionX, spawnPositionY), objectToSpawn.transform.rotation, null));
            }

            float spawnAfterSeconds = Random.Range(minSpawnInterval,maxSpawnInterval);
            yield return new WaitForSeconds(spawnAfterSeconds);
        }
    }
}
