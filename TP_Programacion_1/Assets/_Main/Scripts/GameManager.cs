using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerController player = null;
    //[SerializeField] private int points;
    private bool gameEnd = false;
    private int enemyCounterLevel;
    private int enemyCounter;
    public bool isFreeze;

    void Start()
    {
        player.lifeController.OnDie.AddListener(OnPlayerDieListener);
        enemyCounter = enemyCounterLevel;
        isFreeze = false;
    }

    void Update()
    {
        if(enemyCounter == 0 && !gameEnd)
        {
            Victory();
        }
    }

    public void GameOver()
    {
        //Debug.Log("El jugador murio");
        gameEnd = true;
        SceneManager.LoadScene("GameOver"); //Funciona pero lo dejo comentado porque por ahora no se cuando lo vamos a necesitar
    }

    public void Victory()
    {
        //Debug.Log("GANASTE");
        gameEnd = true;
        SceneManager.LoadScene("Victory"); //O deberia cargar el siguiente nivel
    }

    private void OnPlayerDieListener()
    {
        //Por ahora, mori. 
        GameOver();
    }

    public void addOneInEnemyCounter()
    {
        enemyCounterLevel++;
    }

    public void takeOneEnemy()
    {
        enemyCounter--;
        Debug.Log($"Mataste un enemigo, te quedan {enemyCounter}");
    }

}
