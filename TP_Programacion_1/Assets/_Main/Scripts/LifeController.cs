using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeController : MonoBehaviour
{
    [Header("Life Stats")]
    [SerializeField] private int maxLife = 0;
    [SerializeField] private int currentLife = 0;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource deathSound = null;
    [SerializeField] private AudioSource damageSound = null;

    void Start()
    {
        //Si currentLife esta vacia.. igualamelo a MaxLife
        currentLife = maxLife;
    }
    public void TakeDamage(int damage)
    {
        currentLife -= damage;
        damageSound.Play();

        if (currentLife <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        currentLife = 0;
        deathSound.Play();
        //animatorController.SetTrigger("IsDead");
        gameObject.SetActive(false);
        //Destroy(gameObject);
    }
}
