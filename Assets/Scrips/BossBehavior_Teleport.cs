using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehavior_Teleport : IBossBehavior
{
    private Transform bossTransform;
    private float timer = 0f;
    private float teleportInterval;
    private float spawnRadius;

    public BossBehavior_Teleport(Transform boss, float interval, float radius)
    {
        bossTransform = boss;
        teleportInterval = interval;
        spawnRadius = radius;
    }

    public void ExecuteBehavior()
    {
        timer += Time.deltaTime;
        if (timer >= teleportInterval)
        {
            timer = 0f;
            Vector2 rnd = Random.insideUnitCircle * spawnRadius;
            bossTransform.position = new Vector3(rnd.x, bossTransform.position.y, rnd.y);
        }
        
        bossTransform.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
}
