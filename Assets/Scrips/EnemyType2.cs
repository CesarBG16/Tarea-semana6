using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyType2 : EnemyBase
{
    private float zigzagTimer = 0f;
    private bool zigLeft = true;

    protected override void Move()
    {
        if (playerRef == null) return;

        zigzagTimer += Time.deltaTime;
        if (zigzagTimer > 0.5f)
        {
            zigzagTimer = 0f;
            zigLeft = !zigLeft;
        }

        Vector3 baseDir = (playerRef.position - transform.position).normalized;
        Vector3 perp = Vector3.Cross(baseDir, Vector3.up).normalized;
        Vector3 moveDir = baseDir + (zigLeft ? perp : -perp) * 0.5f;
        rb.velocity = moveDir.normalized * Speed;
    }
}