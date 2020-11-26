using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject player;

    void Awake()
    {
        for (int i = 0; i < transform.childCount; i++) //conta y agrega a la lista, pero solo si el objeto esta activo.
        {
            if (transform.GetChild(i).gameObject.activeSelf) //si el hijo esta activo
            {
                gameManager.addOneInEnemyCounter(); //Agregalo al contador de enemigos 

                //y acá se setean los gameManagers y players.
                EnemyController enemyController = transform.GetChild(i).gameObject.GetComponent<EnemyController>();
                enemyController.SetPlayer(player);
                enemyController.SetGameManager(gameManager);

                LifeController life = transform.GetChild(i).gameObject.GetComponent<LifeController>();
                life.SetGameManager(gameManager);


                //y dependiendo del tipo de controller que tenga, se le setea la data. 
                EnemyStaticController enemyStatic = transform.GetChild(i).gameObject.GetComponent<EnemyStaticController>();
                if (enemyStatic != null)
                {
                    enemyStatic.SetPlayer(player);
                    enemyStatic.SetGameManager(gameManager);
                }

                EnemyFly enemyFly = transform.GetChild(i).gameObject.GetComponent<EnemyFly>();
                if (enemyFly != null)
                {
                    enemyFly.SetPlayer(player);
                    enemyFly.SetGameManager(gameManager);
                }

                EnemyPatrol2 enemyPatrol = transform.GetChild(i).gameObject.GetComponent<EnemyPatrol2>();
                if (enemyPatrol != null)
                {
                    enemyPatrol.SetGameManager(gameManager);
                }
            }
        }

    }
}
