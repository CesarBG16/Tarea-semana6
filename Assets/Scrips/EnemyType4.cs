using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyType4 : EnemyBase
{
    private float angle = 0f;

    protected override void Move()
    {
        if (playerRef == null) return;

        
        Vector3 offset = transform.position - playerRef.position;
        float currentDist = offset.magnitude;
        
        Vector3 radialDir = offset.normalized;
        if (currentDist < 4f)
            rb.velocity = -radialDir * Speed; 
        else if (currentDist > 6f)
            rb.velocity = radialDir * -Speed; 
        else
        {
            
            angle += Speed * Time.deltaTime;
            float x = Mathf.Cos(angle) * 5f;
            float z = Mathf.Sin(angle) * 5f;
            Vector3 targetPos = new Vector3(playerRef.position.x + x, transform.position.y, playerRef.position.z + z);
            Vector3 dir = (targetPos - transform.position).normalized;
            rb.velocity = dir * Speed;
        }
    }
}
