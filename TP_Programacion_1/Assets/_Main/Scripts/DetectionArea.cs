using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionArea : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))     //Comparamos el tag del collision, si coincide con player, buscame el controller del enemigo y activame/desactivame su canAttack. 
        {
            EnemyController enemy = transform.parent.GetComponent<EnemyController>(); //Buscalo en el padre del objeto que contiene esto
            enemy.canAttack = true;
        }      
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
       
        if (collision.CompareTag("Player"))
        {
            EnemyController enemy = transform.parent.GetComponent<EnemyController>(); 
            enemy.canAttack = false;
        }
    }
}
