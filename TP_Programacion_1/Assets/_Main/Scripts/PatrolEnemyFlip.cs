using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolEnemyFlip : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyPatrol2 enemy = collision.GetComponent<EnemyPatrol2>();
        if (enemy != null)
        {
            enemy.BackFlip();
        }
    }
}
