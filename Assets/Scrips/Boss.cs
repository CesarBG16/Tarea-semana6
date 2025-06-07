using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour, IHealth
{
    [Header("Boss Stats")]
    public int Health = 20;
    public float Speed = 2f;
    public int PointsOnDeath = 10;

    [Header("Behaviors Settings")]
    public float rapidFireRate = 0.5f;
    public float rapidFireBulletSpeed = 12f;
    public float teleportInterval = 5f;
    public float summonInterval = 7f;

    private Rigidbody rb;
    private Transform playerRef;
    private GameObject bulletPrefab;
    private GameObject[] enemyPrefabs;

   
    private List<IBossBehavior> behaviors = new List<IBossBehavior>();

    
    private IBossBehavior currentBehavior;

    public event System.Action OnDeath;

    private void Start()
    {
        rb = gameObject.AddComponent<Rigidbody>();
        rb.useGravity = false;
        SphereCollider col = gameObject.AddComponent<SphereCollider>();
        col.isTrigger = true;
        playerRef = GameObject.FindGameObjectWithTag("Player").transform;

        
        bulletPrefab = Resources.Load<GameObject>("Prefabs/BulletTemplate");
        enemyPrefabs = new GameObject[4];
        enemyPrefabs[0] = Resources.Load<GameObject>("Prefabs/Enemies/EnemyType1");
        enemyPrefabs[1] = Resources.Load<GameObject>("Prefabs/Enemies/EnemyType2");
        enemyPrefabs[2] = Resources.Load<GameObject>("Prefabs/Enemies/EnemyType3");
        enemyPrefabs[3] = Resources.Load<GameObject>("Prefabs/Enemies/EnemyType4");

        
        behaviors.Add(new BossBehavior_Chase(transform, playerRef, Speed));
        behaviors.Add(new BossBehavior_RapidFire(transform, playerRef, bulletPrefab, rapidFireRate, rapidFireBulletSpeed));
        behaviors.Add(new BossBehavior_Teleport(transform, teleportInterval, 10f));
        behaviors.Add(new BossBehavior_SummonMinions(transform, enemyPrefabs, summonInterval));

        
        currentBehavior = behaviors[Random.Range(0, behaviors.Count)];
    }

    private void Update()
    {
      
        if (Time.frameCount % (5 * 60) == 0) 
        {
            currentBehavior = behaviors[Random.Range(0, behaviors.Count)];
        }

        currentBehavior?.ExecuteBehavior();
    }

    public void TakeDamage(int amount)
    {
        Health -= amount;
        if (Health <= 0)
            Die();
    }

    private void Die()
    {
        OnDeath?.Invoke();
        GameManager.Instance.OnEnemyKilled(PointsOnDeath);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            TakeDamage(1);
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Player"))
        {
           
            GameManager.Instance.OnPlayerDeath();
            Destroy(other.gameObject);
        }
    }
}
