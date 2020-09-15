using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeController : MonoBehaviour
{
    [Header("Life Stats")]
    [SerializeField] private int maxLife = 0;
    [SerializeField] private int currentLife = 0;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource damageSound = null;

    [Header("Death")]
    [SerializeField] private GameObject death = null;

    void Start()
    {
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
        Instantiate(death, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
