using UnityEngine;

public class SlimeSpawnPoint : MonoBehaviour
{
    [SerializeField] private GameObject slimePrefab;
    
    private float spawnInterval = 5f;
    private float spawnTimer = 0f;

    private void Start()
    {
        InvokeRepeating(nameof(ReduceSpawnInterval), 3, 3);
    }

    private void Update()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnInterval)
        {
            SpawnSlime();
            spawnTimer = 0f;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            spawnInterval = 5f;
        }
    }

    private void SpawnSlime()
    {
        Instantiate(slimePrefab, transform.position, Quaternion.identity);
    }

    private void ReduceSpawnInterval()
    {
        if (spawnInterval > 0.6f)
        {
            spawnInterval -= 0.2f;
        }
    }
}