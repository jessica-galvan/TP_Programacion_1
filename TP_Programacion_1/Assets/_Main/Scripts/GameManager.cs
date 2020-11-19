using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerController player = null;
    [SerializeField] private GameObject victoryScreen = null;
    [SerializeField] private GameObject gameOverScreen = null;
    [SerializeField] private GameObject restartButton = null;
    //[SerializeField] private int points; 
    private bool gameEnd = false;
    private float restartTimer = 2f;
    private float restartCooldown = 0f;
    private int enemyCounterLevel;
    private int enemyCounter;
    private Animator gameOverAnimator;
    public bool isFreeze;
    public UnityEvent OnChangeCurrentEnemies = new UnityEvent();

    void Start()
    {
        player.lifeController.OnDie.AddListener(OnPlayerDieListener);
        victoryScreen.SetActive(false);
        gameOverScreen.SetActive(false);
        restartButton.SetActive(false);
        gameOverAnimator = gameOverScreen.GetComponent<Animator>();
        enemyCounter = enemyCounterLevel;
        isFreeze = false;
    }

    void Update()
    {
        if(enemyCounter == 0 && !gameEnd)
        {
            Victory();
        }

        if(isFreeze && gameEnd && restartCooldown < Time.time)
        {
            restartButton.SetActive(true);
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

    public void Victory()
    {
        gameEnd = true;
        isFreeze = true;
        victoryScreen.SetActive(true);
    }


    public void GameOver()
    {
        Debug.Log("dead1");
        gameEnd = true;
        isFreeze = true;
        gameOverScreen.SetActive(true);
        gameOverAnimator.SetBool("isDead", true);
        Debug.Log("dead2");
        restartCooldown = Time.time + restartTimer;
    }

    public void RestartLastCheckpoint()
    {
        Debug.Log("LastCheckpoint");
        gameOverScreen.SetActive(false);
        gameOverAnimator.SetBool("isDead", false);
        //LOAD LAST CHECKPOINT
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
