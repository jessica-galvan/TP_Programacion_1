using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject player;
    //[SerializeField] private List<EnemyController> enemies;
    [SerializeField] private List<GameObject> enemies = new List<GameObject>();
    [SerializeField] private List<EnemyStaticController> staticEnemies;
    //[SerializeField] private List<EnemyPatrolController> patrolEnemies;
    [SerializeField] private int enemyCounter;
    //[SerializeField] private EnemyController [] enemiesController;

    void Awake()
    {
        //Para saber cuantos hijos (enemigos) hay. Y Agregamos la info al GameManager
        foreach (var item in this.transform)
        {
            enemyCounter++;
            gameManager.addOneInEnemyCounter();
        }

        //y acá los agregamos a la lista
        for (int i = 0; i < enemyCounter; i++)
        {
            enemies.Add(transform.GetChild(i).gameObject);
        }

        //acá se setea el game manager y player para cada item
        for (int i = 0; i < enemies.Count; i++)
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

            EnemyPatrolController enemyPatrol = enemies[i].GetComponent<EnemyPatrolController>();
            if(enemyPatrol != null)
            {
                enemyPatrol.SetPlayer(player);
                enemyPatrol.SetGameManager(gameManager);
            }
        }
    }

}
