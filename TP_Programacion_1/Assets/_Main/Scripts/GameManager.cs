using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerController player = null;
    //[SerializeField] private int points; 
    private bool gameEnd = false;
    private int enemyCounterLevel;
    private int enemyCounter;
    public bool isFreeze;
    public UnityEvent OnChangeCurrentEnemies = new UnityEvent();

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

    public bool CheckIfTheyAreEnemies()
    {
        bool response = false;
        if(enemyCounter > 0)
        {
            response = true;
        }
        return response;
    }

    public void GameOver()
    {
        //Debug.Log("El jugador murio");
        gameEnd = true;
        SceneManager.LoadScene("GameOver"); 
    }

    public void Victory()
    {
        //Debug.Log("GANASTE");
        gameEnd = true;
        SceneManager.LoadScene("Victory"); //O deberia cargar el siguiente nivel
    }

    private void OnPlayerDieListener()
    {
        GameOver();  //Por ahora, mori
    }

    public void addOneInEnemyCounter()
    {
        enemyCounterLevel++;
    }

    public void takeOneEnemy()
    {
        enemyCounter--;
        OnChangeCurrentEnemies.Invoke();
    }

    public int GetEnemiesAmount()
    {
        return enemyCounter;
    }

}
