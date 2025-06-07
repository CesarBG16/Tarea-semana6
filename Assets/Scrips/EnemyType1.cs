using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyType1 : EnemyBase
{
    protected override void Move()
    {
        if (playerRef == null) return;

        Vector3 dir = (playerRef.position - transform.position).normalized;
        rb.velocity = dir * Speed;
    }
}
