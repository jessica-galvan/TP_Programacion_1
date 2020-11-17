using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDEnemieCounter : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    private Text info;
    private int enemyCounter = 0;

    private void Awake()
    {
        gameManager.OnChangeCurrentEnemies.AddListener(OnChangeCurrentEnemiesHandler);
        info = gameObject.GetComponent<Text>();
    }

    void Start()
    {
        enemyCounter = gameManager.GetEnemiesAmount();
        info.text = "Enemies: " + enemyCounter;
        if (enemyCounter == 0)
        {
            info.text = "Enemies: ???";
        }

    }

    private void OnChangeCurrentEnemiesHandler()
    {
        enemyCounter = gameManager.GetEnemiesAmount();
        info.text = "Enemies: " + enemyCounter;
    }
}
