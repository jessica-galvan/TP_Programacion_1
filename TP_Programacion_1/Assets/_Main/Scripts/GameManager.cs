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
    [SerializeField] private float restartTimer = 2f;
    [SerializeField] private int lifesRespawn = 2;
    [SerializeField] private int collectable; 
    public bool isFreeze;

    //Extras
    private bool gameOver = false;
    private bool victory = false;
    private float restartCooldown = 0f;
    private int enemyCounterLevel;
    private int enemyCounter;
    private Vector2 playerSpawnPosition;
    private Vector2 playerCurrentCheckpoint;
    private Animator gameOverAnimator;
    public UnityEvent OnChangeCurrentEnemies = new UnityEvent();
    public UnityEvent OnChangeCollectable = new UnityEvent();
    public UnityEvent OnPlayerRespawn = new UnityEvent();

    void Start()
    {
        player.lifeController.OnDie.AddListener(OnPlayerDieListener);
        playerSpawnPosition = player.GetComponent<Transform>().position;
        playerCurrentCheckpoint = playerSpawnPosition;
        victoryScreen.SetActive(false);
        gameOverScreen.SetActive(false);
        gameOverAnimator = gameOverScreen.GetComponent<Animator>();
        enemyCounter = enemyCounterLevel;
        isFreeze = false;
        victory = false;
    }

    void Update()
    {
        if(enemyCounter == 0 && !gameOver)
        {
            Victory();
        }

        if(isFreeze && gameOver && restartCooldown < Time.time)
        {
            RestartLastCheckpoint();
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
        if (!victory)
        {
            victory = true;
            isFreeze = true;
            victoryScreen.SetActive(true);
        }
    }

    public void GameOver()
    {
        gameOver = true;
        isFreeze = true;
        gameOverScreen.SetActive(true);
        gameOverAnimator.SetBool("isDead", true);
        restartCooldown = Time.time + restartTimer;
    }

    public void ChangeSpawnPosition(Vector2 checkpoint)
    {
        playerCurrentCheckpoint = checkpoint;
    }

    public Vector2 GetCurrentCheckpoint()
    {
        return playerCurrentCheckpoint;
    }

    public void RestartLastCheckpoint()
    {
        gameOver = false;
        isFreeze = false;
        OnPlayerRespawn.Invoke();
        //playerCurrentCheckpoint.y += 1; //para que tenga un offset de cuando vuelve, pero 1 en int es muuy grande la caida
        player.SetCurrentPosition(playerCurrentCheckpoint);
        player.PlayerActive(true);
        player.lifeController.Respawn(lifesRespawn);
        gameOverScreen.SetActive(false);
        gameOverAnimator.SetBool("isDead", false);
    }

    private void OnPlayerDieListener()
    {
        GameOver();
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

    public void PickUpCollectable()
    {
        collectable++;
        OnChangeCollectable.Invoke();
    }
}
