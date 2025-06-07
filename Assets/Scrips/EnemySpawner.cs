using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemySpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    [Tooltip("List of enemy prefabs (4 tipos)")]
    [SerializeField] private GameObject[] enemyPrefabs; 

    [Tooltip("Zona de spawn (radio alrededor del centro)")]
    [SerializeField] private float spawnRadius = 15f;

    [Tooltip("Tiempo inicial entre oleadas")]
    [SerializeField] private float initialWaveInterval = 10f;

    private List<int> fibonacciNumbers = new List<int> { 1, 1 };
    private int fibIndex = 0;
    private float waveTimer = 0f;
    private float currentWaveInterval;

    private void Start()
    {
        currentWaveInterval = initialWaveInterval;
        waveTimer = currentWaveInterval;
    }

    private void Update()
    {
        waveTimer -= Time.deltaTime;
        if (waveTimer <= 0f)
        {
            SpawnWave();
            
            currentWaveInterval = Mathf.Max(2f, currentWaveInterval - 0.5f);
            waveTimer = currentWaveInterval;
        }
    }

    private void SpawnWave()
    {
        
        if (fibIndex + 1 >= fibonacciNumbers.Count)
        {
            int next = fibonacciNumbers[fibonacciNumbers.Count - 1] + fibonacciNumbers[fibonacciNumbers.Count - 2];
            fibonacciNumbers.Add(next);
        }

        int spawnCount = fibonacciNumbers[fibIndex];
        fibIndex++;

        for (int i = 0; i < spawnCount; i++)
        {
            
            int typeIndex = Random.Range(0, enemyPrefabs.Length);
            Vector3 spawnPos = GetRandomSpawnPosition();
            GameObject e = Instantiate(enemyPrefabs[typeIndex], spawnPos, Quaternion.identity);

            
            e.tag = "Enemy";

            
            IHealth healthComp = e.GetComponent<IHealth>();
            if (healthComp != null)
            {
                healthComp.OnDeath += () => { };
            }
        }

        
        if (GameManager.Instance.CurrentLevel > 1 && GameManager.Instance.CurrentLevel % 2 == 0)
        {
            
            SpawnBoss();
        }
    }

    private Vector3 GetRandomSpawnPosition()
    {
        Vector2 circle = Random.insideUnitCircle * spawnRadius;
        return new Vector3(circle.x, 0.5f, circle.y); 
    }

    private void SpawnBoss()
    {
        Vector3 spawnPos = GetRandomSpawnPosition();
        GameObject bossObj = Instantiate(enemyPrefabs[0], spawnPos, Quaternion.identity);
       
        Destroy(bossObj.GetComponent<EnemyBase>());
        bossObj.AddComponent<Boss>();
        bossObj.tag = "Boss";
    }
}