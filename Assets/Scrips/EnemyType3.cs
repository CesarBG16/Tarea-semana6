using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyType3 : EnemyBase
{
    private float moveTimer = 0f;
    private bool moving = true;

    protected override void Move()
    {
        if (playerRef == null) return;

        moveTimer += Time.deltaTime;
        if (moveTimer > 1f)
        {
            moveTimer = 0f;
            moving = !moving;
        }

        if (moving)
        {
            Vector3 dir = (playerRef.position - transform.position).normalized;
            rb.velocity = dir * Speed;
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
    }
}
