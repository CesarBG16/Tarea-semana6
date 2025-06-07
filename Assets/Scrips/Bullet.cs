using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float lifeTime = 2f;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Enemy") || other.CompareTag("Boss"))
        {
            
            Destroy(gameObject);
        }
    }
}

