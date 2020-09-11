using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionArea : MonoBehaviour
{
    //Se separo el detection area para que si hay más enemigos que lo tengan, se pueda volver a reutilizar en cada uno. 
    //Comparamos el tag del collision, si coincide con player, buscame el controller del enemigo y activame/desactivame su canAttack. 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
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
