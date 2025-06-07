using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehavior_Chase : IBossBehavior
{
    private Transform bossTransform;
    private Transform playerTransform;
    private float speed;

    public BossBehavior_Chase(Transform boss, Transform player, float moveSpeed)
    {
        bossTransform = boss;
        playerTransform = player;
        speed = moveSpeed;
    }

    public void ExecuteBehavior()
    {
        Vector3 dir = (playerTransform.position - bossTransform.position).normalized;
        bossTransform.GetComponent<Rigidbody>().velocity = dir * speed;
    }
}
