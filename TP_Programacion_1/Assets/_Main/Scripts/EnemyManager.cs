using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject player;
    [SerializeField] private List<EnemyController> enemies;
    [SerializeField] private List<EnemyStaticController> staticEnemies;
    //[SerializeField] private List<EnemyPatrolController> patrolEnemies;
    [SerializeField] private int enemyCounter;

    void Start()
    {
        foreach (var enemy in enemies)
        {
            enemy.SetPlayer(player);
            enemy.SetGameManager(gameManager);
            enemyCounter++;
        }

        if(staticEnemies != null)
        {
            foreach (var staticEnemy in staticEnemies)
            {
                staticEnemy.SetPlayer(player);
            }
        }
    }

}
