using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehavior_RapidFire : IBossBehavior
{
    private Transform bossTransform;
    private GameObject bulletPrefab;
    private float fireRate;
    private float bulletSpeed;
    private float timer = 0f;
    private Transform playerTransform;

    public BossBehavior_RapidFire(Transform boss, Transform player, GameObject bullet, float rate, float bSpeed)
    {
        bossTransform = boss;
        playerTransform = player;
        bulletPrefab = bullet;
        fireRate = rate;
        bulletSpeed = bSpeed;
    }

    public void ExecuteBehavior()
    {
        
        bossTransform.GetComponent<Rigidbody>().velocity = Vector3.zero;

      
        timer += Time.deltaTime;
        if (timer >= fireRate)
        {
            timer = 0f;
            
            Vector3 dir = (playerTransform.position - bossTransform.position).normalized;
            GameObject b = GameObject.Instantiate(bulletPrefab, bossTransform.position + dir, Quaternion.LookRotation(dir));
            b.GetComponent<Rigidbody>().velocity = dir * bulletSpeed;
        }
    }
}
