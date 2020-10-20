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
    private bool facingRight = false;
    
    [Header("Prefabs Settings")]
    [SerializeField] private GameObject player = null; 
    [SerializeField] private GameObject bullet = null;
    [SerializeField] private GameManager gameManager = null;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource shootingSound = null;
    [SerializeField] private AudioSource damageSound = null;

    void Start()
    {
        animatorController = GetComponent<Animator>();
        lifeController = GetComponent<LifeController>();
        lifeController.OnTakeDamage += OnTakeDamageListener;
        lifeController.OnDie.AddListener(OnDieListener);
        gameManager.addOneInEnemyCounter();
    }

    void Update()
    {
        if (canAttack) //Si el enemigo puede atacar es porque el player esta dentro de al trigger zone
        {
            if(player.transform.position.x > transform.position.x && !facingRight) //estoy a la derecha
            {
                Flip();
            } else if(player.transform.position.x < transform.position.x && facingRight) //estoy a la izquierda
            {
                Flip();
            }

            if (Time.time > cooldownTimer && canShoot) //cooldown y que ataque
            {
                canShoot = false;
                Shoot();
            }
        }
    }

    private void Flip()
    {
        transform.Rotate(0f, 180f, 0f);
        facingRight = !facingRight;
    }

    private void Shoot()
    {
        shootingSound.Play();
        animatorController.SetTrigger("IsShooting");
        Instantiate(bullet, transform.position + offset, transform.rotation);
        cooldownTimer += cooldown;
        canShoot = true;
    }

    private void OnTakeDamageListener(int currentLife, int damage)
    {
        damageSound.Play();
    }

    private void OnDieListener()
    {
        gameManager.takeOneEnemy();
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
