using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour, IHealth
{
    [Header("Stats")]
    public int Health = 2;
    public float Speed = 3f;

    [Header("Optional: Points awarded")]
    public int PointsOnDeath = 1;

    public event System.Action OnDeath;

    protected Rigidbody rb;
    protected Transform playerRef;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerRef = GameObject.FindGameObjectWithTag("Player").transform;
    }

    protected virtual void Update()
    {
        Move();
    }

    protected abstract void Move();

    public virtual void TakeDamage(int amount)
    {
        Health -= amount;
        if (Health <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
       
        OnDeath?.Invoke();
        
        GameManager.Instance.OnEnemyKilled(PointsOnDeath);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
          
            IHealth h = this as IHealth;
            h?.TakeDamage(1);
            
            Destroy(other.gameObject);
        }
    }
}
