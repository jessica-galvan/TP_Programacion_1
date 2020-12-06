using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantDeath : MonoBehaviour
{
    [SerializeField] int damage = 1;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.GetComponent<PlayerController>();
        LifeController life = collision.GetComponent<LifeController>();
        if(life != null)
        {
            life.TakeDamage(damage);

            if (player) 
            {
                if (life.GetCurrentLife() > 0)
                {
                    player.PlayerActive(false);
                    player.SetCurrentPosition(gameManager.GetCurrentCheckpoint());
                    player.PlayerActive(true);
                    life.Respawn(life.GetCurrentLife());
                }
            } else 
            {
                Destroy(gameObject);
            }
        }
    }
}
