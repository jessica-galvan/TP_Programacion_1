using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LifeController : MonoBehaviour
{
    [SerializeField] private GameManager gameManager = null;

    [Header("Life Stats")]
    [SerializeField] private int maxLife = 0;
    [SerializeField] private int currentLife = 0;
    private bool isDead = false;

    [Header("Death")]
    [SerializeField] private GameObject death = null;

    //LISTENERS
    public UnityEvent OnDie = new UnityEvent();
    public UnityEvent OnChangeCurrentLife = new UnityEvent();
    public UnityEvent OnRespawnLife = new UnityEvent();
    public Action<int, int> OnTakeDamage;

    void Start()
    {
        currentLife = maxLife;
    }

    public void TakeDamage(int damage)
    {
        if(currentLife > 0 && !gameManager.isFreeze)
        {
            currentLife -= damage;
            OnTakeDamage.Invoke(currentLife, damage);
            OnChangeCurrentLife.Invoke();
        }

        if (currentLife <= 0 && !isDead) //isDead es para que no siga ejecutando la muerte si su vida es menor o igual a 0 y aun asi sigue en escena. Al final, con la instanciación de muerte ya esta. 
        {
            isDead = true;
            Die();
        }
    }

    public void TakeHeal(int heal)
    {
        if(currentLife < maxLife && !gameManager.isFreeze)
        {
            currentLife += heal;
            if (currentLife > maxLife)
            {
                currentLife = maxLife;
            }
        }
        OnChangeCurrentLife.Invoke();
    }

    public void Respawn(int heal)
    {
        isDead = false;
        currentLife = heal;
        OnRespawnLife.Invoke();
    }

    public void Die()
    {
        OnDie.Invoke();
        Instantiate(death, transform.position, transform.rotation);
        var player = GetComponent<PlayerController>();
        if(player != null)
        {
            player.PlayerActive(false);
        } else
        {
            Destroy(gameObject);
        }
    }

    public float GetCurrentLifePercentage()
    {
        return (float)currentLife/maxLife;
    }

    public int GetMaxLife()
    {
        return maxLife;
    }

    public int GetCurrentLife()
    {
        return currentLife;
    }

    public bool CanHeal()
    {
        bool response = false;
        if (currentLife < maxLife)
        {
            response = true;
        }
        return response;
    }
    
    public void SetGameManager(GameManager _gameManager)
    {
        gameManager = _gameManager;
    }
}
