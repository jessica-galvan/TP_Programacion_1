using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDamage : MonoBehaviour
{
    [SerializeField] int damage;
    [SerializeField] float timeDamageCooldown;
    private float timer;
    private bool canDamage;
    private bool isPlayerThere;
    private LifeController playerLifeController = null;

    private void Start()
    {
        canDamage = true;
    }

    private void Update()
    {
        if(!canDamage && Time.time > timer)
        {
            canDamage = true;
        }

        if (canDamage && playerLifeController != null && isPlayerThere)
        {
            canDamage = false;
            timer = Time.time + timeDamageCooldown;
            playerLifeController.TakeDamage(damage);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerLifeController = collision.GetComponent<LifeController>();
            isPlayerThere = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerThere = false;
        }
    }
}
