using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeController : MonoBehaviour
{
    [Header("Life Stats")]
    [SerializeField] private int maxLife = 0;
    [SerializeField] private int currentLife = 0;
    private bool isDead = false;


    [Header("Audio Sources")]
    [SerializeField] private AudioSource damageSound = null;

    [Header("Death")]
    [SerializeField] private GameObject death = null;
    [SerializeField] private GameObject playerDeath = null;

    void Start()
    {
        currentLife = maxLife;
    }

    public void TakeDamage(int damage)
    {
        if(currentLife > 0)
        {
            currentLife -= damage;
            damageSound.Play();
        }

        if (currentLife <= 0 && !isDead)
        {
            Die();
        }
    }

    public void TakeHeal(int heal)
    {
        if (currentLife < maxLife)
        {
            currentLife += heal;
            if(currentLife < maxLife)
            {
                currentLife = maxLife;
            }
        } 
    }

    public void Die()
    {
        isDead = true;
        if (gameObject.CompareTag("Player"))
        {
            Instantiate(playerDeath, transform.position, transform.rotation);
            Destroy(gameObject);

        } else 
        {
            Instantiate(death, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
