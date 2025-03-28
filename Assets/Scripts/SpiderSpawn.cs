using UnityEngine;

public class SpiderSpawn : MonoBehaviour
{
    public GameObject spiderPrefab;
    public Transform xrRig;
    public float initialSpawnRadius = 5f;
    public int maxSpiders = 10;
    public int spidersPerSpawn = 3;
    public float spawnInterval = 5f;

    private int currentSpiderCount = 0;
    private float spawnRadius;
    private float timeElapsed = 0f;
    private float jumpScareInterval = 15f;
    private float nextJumpScareTime = 15f;

    void Start()
    {
        spawnRadius = initialSpawnRadius;
        InvokeRepeating("SpawnSpiders", 2f, spawnInterval);
    }

    void Update()
    {
        timeElapsed += Time.deltaTime;
        spawnRadius = Mathf.Max(2f, initialSpawnRadius - timeElapsed * 0.05f);

        if (timeElapsed >= nextJumpScareTime)
        {
            SpawnJumpScareSpiders();
            nextJumpScareTime += jumpScareInterval;
        }
    }

    void SpawnSpiders()
    {
        if (currentSpiderCount >= maxSpiders) return;

        for (int i = 0; i < spidersPerSpawn; i++)
        {
            if (currentSpiderCount >= maxSpiders) break;

            Vector3 spawnPosition = GetSpawnPositionAtBase();
            Quaternion randomRotation = Quaternion.Euler(0, Random.Range(0f, 360f), 0);

            GameObject spider = Instantiate(spiderPrefab, spawnPosition, randomRotation);
            currentSpiderCount++;

            RandomSpider movement = spider.GetComponent<RandomSpider>();
            if (movement)
            {
                movement.enabled = true;
            }
        }
    }

    void SpawnJumpScareSpiders()
    {
        if (currentSpiderCount >= maxSpiders) return;

        int jumpScareCount = Random.Range(2, 4);
        for (int i = 0; i < jumpScareCount; i++)
        {
            Vector3 spawnPosition = GetJumpScarePositionAtEyeLevel();
            Quaternion randomRotation = Quaternion.Euler(0, Random.Range(0f, 360f), 0);

            GameObject spider = Instantiate(spiderPrefab, spawnPosition, randomRotation);
            currentSpiderCount++;

            RandomSpider movement = spider.GetComponent<RandomSpider>();
            if (movement)
            {
                movement.enabled = true;
            }
        }
    }

    Vector3 GetSpawnPositionAtBase()
    {
        Transform cameraTransform = Camera.main.transform;
        Vector3 forwardDirection = cameraTransform.forward;
        forwardDirection.y = 0;

        float distance = Random.Range(2f, 3f);
        float offsetX = Random.Range(-2f, 2f);
        float offsetZ = Random.Range(-2f, 2f);
        
        Vector3 spawnPosition = cameraTransform.position + (forwardDirection * distance);
        spawnPosition.y = xrRig.position.y;
        
        spawnPosition.x += offsetX;
        spawnPosition.z += offsetZ;

        return spawnPosition;
    }

    Vector3 GetJumpScarePositionAtEyeLevel()
    {
        Transform cameraTransform = Camera.main.transform;
        Vector3 forwardDirection = cameraTransform.forward;
        forwardDirection.y = 0;

        float closeDistance = Random.Range(0.5f, 1.5f);
        float offsetX = Random.Range(-1f, 1f);
        float offsetZ = Random.Range(-1f, 1f);

        Vector3 spawnPosition = cameraTransform.position + (forwardDirection * closeDistance);
        spawnPosition.y = cameraTransform.position.y;

        spawnPosition.x += offsetX;
        spawnPosition.z += offsetZ;

        return spawnPosition;
    }
}
