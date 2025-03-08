using UnityEngine;

public class SpiderSpawn : MonoBehaviour
{
    public GameObject spiderPrefab;
    public Transform xrRig;
    public float spawnRadius = 5f;
    public int maxSpiders = 10;
    public int spidersPerSpawn = 3;
    public float spawnInterval = 5f;

    private int currentSpiderCount = 0;

    void Start()
    {
        InvokeRepeating("SpawnSpiders", 2f, spawnInterval);
    }

    void SpawnSpiders()
    {
        if (currentSpiderCount >= maxSpiders) return;

        for (int i = 0; i < spidersPerSpawn; i++)
        {
            if (currentSpiderCount >= maxSpiders) break;

            Vector3 spawnPosition = GetRandomSpawnPosition();
            GameObject spider = Instantiate(spiderPrefab, spawnPosition, Quaternion.identity);
            currentSpiderCount++;

            SpiderMovement movement = spider.GetComponent<SpiderMovement>();
            if (movement)
            {
                movement.enabled = true;
            }
        }
    }

    Vector3 GetRandomSpawnPosition()
    {
        Vector3 randomDirection = Random.insideUnitSphere * spawnRadius;
        randomDirection.y = 0;
        return xrRig.position + randomDirection;
    }
}
