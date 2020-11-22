using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolEnemyFlip : MonoBehaviour
{
    private bool isPatrol;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isPatrol)
        {
            EnemyPatrol2 enemy = collision.GetComponent<EnemyPatrol2>();
            if (enemy != null)
            {
                enemy.BackFlip();
            }
        } else
        {
            EnemyFly enemy = collision.GetComponent<EnemyFly>();
            if (enemy != null)
            {
                enemy.BackFlip();
            }
        }
        

    }

    public void SetIsPatrol(bool response)
    {
        isPatrol = response;
    }
}
