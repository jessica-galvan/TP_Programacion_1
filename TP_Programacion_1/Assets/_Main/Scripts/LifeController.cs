﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LifeController : MonoBehaviour
{
    [Header("Life Stats")]
    [SerializeField] private int maxLife = 0;
    [SerializeField] private int currentLife = 0;
    private bool isDead = false;

    [Header("Death")]
    [SerializeField] private GameObject death = null;

    //LISTENERS
    public UnityEvent OnDie = new UnityEvent();
    public UnityEvent OnChangeCurrentLife = new UnityEvent();
    public Action<int, int> OnTakeDamage;

    void Start()
    {
        currentLife = maxLife;
    }

    public void TakeDamage(int damage)
    {
        if(currentLife > 0)
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
        if (currentLife < maxLife)
        {
            currentLife += heal;
            if (currentLife > maxLife)
            {
                currentLife = maxLife;
            }
            OnChangeCurrentLife.Invoke();
        } 
    }

    public void Die()
    {
        OnDie.Invoke();
        Instantiate(death, transform.position, transform.rotation);
        Destroy(gameObject);
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
}
