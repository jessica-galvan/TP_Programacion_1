using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floater : MonoBehaviour
{
    [SerializeField] private float amplitud = 0.1f;
    [SerializeField] private float speed = 1f;
    private Vector3 spawnPosition;
    private GameManager gameManager = null;

    void Start()
    {
        spawnPosition = transform.position;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        if (!gameManager.isFreeze)
        {
            Vector2 currentPosition;
            currentPosition.x = spawnPosition.x;
            currentPosition.y = spawnPosition.y + amplitud * Mathf.Sin(speed * Time.time);
            transform.position = currentPosition;
        }
    }
}
