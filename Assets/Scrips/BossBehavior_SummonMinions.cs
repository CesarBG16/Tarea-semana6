using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehavior_SummonMinions : IBossBehavior
{
    private Transform bossTransform;
    private GameObject[] enemyPrefabs;
    private float timer = 0f;
    private float summonInterval;

    public BossBehavior_SummonMinions(Transform boss, GameObject[] enemies, float interval)
    {
        bossTransform = boss;
        enemyPrefabs = enemies;
        summonInterval = interval;
    }

    public void ExecuteBehavior()
    {
        timer += Time.deltaTime;
        if (timer >= summonInterval)
        {
            timer = 0f;
            for (int i = 0; i < 3; i++)
            {
                int idx = Random.Range(0, enemyPrefabs.Length);
                Vector2 rnd = Random.insideUnitCircle * 3f;
                Vector3 spawnPos = new Vector3(bossTransform.position.x + rnd.x, 0.5f, bossTransform.position.z + rnd.y);
                GameObject e = GameObject.Instantiate(enemyPrefabs[idx], spawnPos, Quaternion.identity);
                e.tag = "Enemy";
            }
        }
        
        bossTransform.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
}
