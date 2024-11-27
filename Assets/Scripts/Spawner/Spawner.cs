using UnityEngine;

[System.Serializable]
public class EnemySpawner
{
    public EnemyAbstractFactory factory;
    public float spawnInterval;
    public float startDelay;
    public bool isEnabled = true;
    public bool shouldStopSpawning;
    [HideInInspector] public float currentTimer;
}

public class Spawner : MonoBehaviour
{
    [SerializeField] private EnemySpawner[] enemyFactory;

    private float timer;

    private void Awake()
    {
        foreach (var spawner in enemyFactory)
        {
            spawner.currentTimer = spawner.startDelay;
        }
    }

    private void Update()
    {
        foreach (var spawner in enemyFactory)
        {
            if (!spawner.isEnabled || spawner.shouldStopSpawning) continue;

            spawner.currentTimer -= Time.deltaTime; // Reduce CurrentTimer
            if (spawner.currentTimer <= 0)
            {
                SpawnEnemy(spawner);                      // Spawn enemy
                spawner.currentTimer = spawner.spawnInterval; // Reset timer
            }
        }
    }

    private void SpawnEnemy(EnemySpawner spawner)
    {
        Vector3 offset = new Vector3(0, 2, 0);
        //Gets an enemy from factory
        if (spawner.isEnabled && spawner.factory != null)
        {
            GameObject enemy = spawner.factory.CreateEnemy(transform.position + offset); // spawns at Game Object position + offset
        }
    }
}
