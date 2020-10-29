﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStaticController : MonoBehaviour
{
    // Start is called before the first frame update

    [Header("Attack Settings")]
    [SerializeField] private Vector3 offset = Vector3.zero;
    [SerializeField] private float cooldown = 2f;
    private float cooldownTimer = 0f;
    private bool canShoot = true;

    [Header("Prefabs Settings")]
    [SerializeField] private GameObject bullet = null;
    private EnemyController enemyController = null;
    private Animator animatorController = null;
    private GameObject player = null;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource shootingSound = null;

    void Start()
    {
        animatorController = GetComponent<Animator>();
        enemyController = GetComponent<EnemyController>();
    }

    void Update()
    {
        if (enemyController.canAttack) //Si el enemigo puede atacar es porque el player esta dentro de al trigger zone
        {
            if (player.transform.position.x > transform.position.x && !enemyController.facingRight) //estoy a la derecha
            {
                enemyController.BackFlip();
            }
            else if (player.transform.position.x < transform.position.x && enemyController.facingRight) //estoy a la izquierda
            {
                enemyController.BackFlip();
            }

            if (Time.time > cooldownTimer && canShoot) //cooldown y que ataque
            {
                canShoot = false;
                Shoot();
            }
        }
    }

    private void Shoot()
    {
        shootingSound.Play();
        animatorController.SetTrigger("IsShooting");
        Instantiate(bullet, transform.position + offset, transform.rotation);
        cooldownTimer += cooldown;
        canShoot = true;
    }

    public void SetPlayer(GameObject _player)
    {
        player = _player;
    }
}