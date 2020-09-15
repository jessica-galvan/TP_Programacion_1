using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Health Settings")]
    public LifeController lifeController = null;
    private Animator animatorController;

    [Header("Movement Settings")]
    //[SerializeField] private float speed = 0f;
    [SerializeField] private Vector3 offset = Vector3.zero;
    [SerializeField] private float cooldown = 0f;
    private float cooldownTimer = 0f;
    public bool canAttack = false;

    [Header("Prefabs Settings")]
    //[SerializeField] private GameObject Player = null; 
    [SerializeField] private GameObject bullet = null;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource shootingSound = null;


    void Start()
    {
       animatorController = GetComponent<Animator>();

    }

    void Update()
    {
        if (canAttack && Time.time > cooldownTimer)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        shootingSound.Play();
        animatorController.SetTrigger("IsShooting");
        Instantiate(bullet, transform.position + offset, transform.rotation);
        cooldownTimer += cooldown;
    }
}
