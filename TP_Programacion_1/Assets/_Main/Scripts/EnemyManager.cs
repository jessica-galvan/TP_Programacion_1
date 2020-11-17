using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject player;
    private List<GameObject> enemies = new List<GameObject>();
    private int enemyCounter;

    void Awake()
    {
        foreach (var item in this.transform) //Para saber cuantos hijos (enemigos) hay. Y Agregamos la info al GameManager
        {
            enemyCounter++;
        }

        for (int i = 0; i < enemyCounter; i++) //y acá los agregamos a la lista, pero solo si el objeto esta activo.
        {
            if (transform.GetChild(i).gameObject.activeSelf)
            {
                gameManager.addOneInEnemyCounter();
                enemies.Add(transform.GetChild(i).gameObject);
            }
        }

        for (int i = 0; i < enemies.Count; i++) //acá se setea el game manager y player para cada item
        {
            EnemyController enemyController = enemies[i].GetComponent<EnemyController>();
            enemyController.SetPlayer(player);
            enemyController.SetGameManager(gameManager);

            EnemyStaticController enemyStatic = enemies[i].GetComponent<EnemyStaticController>();
            if(enemyStatic != null)
            {
                enemyStatic.SetPlayer(player);
                enemyStatic.SetGameManager(gameManager);
            }
        }
    }
}
