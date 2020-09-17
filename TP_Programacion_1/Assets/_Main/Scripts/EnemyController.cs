using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Health Settings")]
    public LifeController lifeController = null;
    private Animator animatorController;

    [Header("Attack Settings")]
    //[SerializeField] private float speed = 0f;
    [SerializeField] private Vector3 offset = Vector3.zero;
    [SerializeField] private float cooldown = 0f;
    [SerializeField] private int bodyDamage = 5;
    private float cooldownTimer = 0f;
    public bool canAttack = false;
    private bool canShoot = true;
    
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
        if (canAttack && Time.time > cooldownTimer && canShoot)
        {
            canShoot = false;
            Shoot();
        }
    }

    void Shoot()
    {
        shootingSound.Play();
        animatorController.SetTrigger("IsShooting");
        Instantiate(bullet, transform.position + offset, transform.rotation);
        cooldownTimer += cooldown;
        canShoot = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        LifeController life = collision.gameObject.GetComponent<LifeController>();
        if (life != null)
        {
            life.TakeDamage(bodyDamage);
        }
    }
}
