using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{
    [Header("Health Settings")]
    public LifeController lifeController = null;
    [SerializeField] private GameManager gameManager = null;
    private Animator animatorController = null;
    [SerializeField] private int revive = 2;

    [Header("Movement Settings")]
    [SerializeField] private float speed = 0f;
    [SerializeField] private Vector3 offset = Vector3.zero;
    private float movement = 0f;
    private bool facingRight = true; //facingRight es para chequear en que sentido esta mirando el personaje. 
    private Rigidbody2D myRigidbody;

    [Header("Jump Settings")]
    [SerializeField] private Transform groundDetectionPoint;
    [SerializeField] private LayerMask groundDetectionList;
    [SerializeField] private float groundDetectionDistance = 1f;
    [SerializeField] private float jumpForce;
    private bool isGrounded;
    private bool canJump;

    [Header("Attack Magic Settings")]
    [SerializeField] private int maxMana = 6;
    [SerializeField] private int currentMana;
    [SerializeField] private GameObject bullet = null;
    [SerializeField] private float cooldown = 0f;
    [SerializeField] private float manaCooldown = 3f;
    private float cooldownTimer = 0f;
    private float manaCooldownTimer = 0f;
    private bool canRechargeMana;
    

    [Header("Attack Phisical Settings")]
    [SerializeField] private Transform attackPoint;
    [SerializeField] private LayerMask enemyDetectionList;
    [SerializeField] private float attackRadius = 1f;
    [SerializeField] private int damage = 5;
    [SerializeField] private float slashCooldown = 1f;
    private float slashCooldownTimer = 0f;
    private bool canAttack;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource shootingSound = null;
    [SerializeField] private AudioSource attackSound = null;
    [SerializeField] private AudioSource rechargeAmmoSound = null;
    [SerializeField] private AudioSource negativeActionSound = null;
    [SerializeField] private AudioSource damageSound = null;
    public UnityEvent OnChangeMana = new UnityEvent();

    void Awake()
    {
       myRigidbody = GetComponent<Rigidbody2D>();
       animatorController = GetComponent<Animator>();
       lifeController = GetComponent<LifeController>();
       currentMana = maxMana;
       canAttack = true;
       lifeController.OnTakeDamage += OnTakeDamageListener;
    }

    void Update()
    {
        if (!gameManager.isFreeze)
        {
            //CHECK GROUND
            RaycastHit2D checkGround = Physics2D.Raycast(groundDetectionPoint.position, Vector2.down, groundDetectionDistance, groundDetectionList);
            isGrounded = checkGround; //mientras que este tocando el suelo, va a poder saltar. 

            if (Input.GetKeyDown(KeyCode.Space) && isGrounded) //JUMP
            {
                animatorController.SetTrigger("IsJumping");
                canJump = true;
            }

            if (!canAttack && Time.time > cooldownTimer) //Cooldown Attack Timer
            {
                canAttack = true;
            }

            //MOVEMENT
            movement = Input.GetAxisRaw("Horizontal") * (speed * Time.deltaTime); //El valor va entre -1 (izquierda) y 1 (derecha). 
            transform.Translate(Mathf.Abs(movement), 0, 0); //El Mathf.Abs -> Math Absolute le saca los signos. Esto sirve porque al flippear el personaje siempre se mueve hacia adelante y el Flip me lo rota. 

            if (movement < 0 && facingRight) //Si el movimiento es positivo y esta mirando a la derecha...
            {
                Flip();
            }
            else if (movement > 0 && !facingRight) //Si el movimiento es negativo y no esta mirando a la derecha...
            {
                Flip();
            }

            //Ataque Mágico
            if (Input.GetMouseButtonDown(0) && Time.time > cooldownTimer && currentMana > 0) //Si recibe input de disparo y el cooldown ya no esta y además hay ammo...
            {
                Shoot();
                animatorController.SetTrigger("IsShooting");
            }
            else if (Input.GetMouseButtonDown(0) && Time.time > cooldownTimer || Input.GetMouseButtonDown(0) && currentMana > 0)
            {
                negativeActionSound.Play();
            }

            //Ataque Fisico
            if (Input.GetMouseButtonDown(1) && Time.time > slashCooldownTimer && canAttack)
            {
                Attack();
            } 

            //RecargarMana cuando queda en cero hasta que tenga la mitad
            if (gameManager.CheckIfTheyAreEnemies() && currentMana == 0 & !canRechargeMana)
            {
                canRechargeMana = true;
                manaCooldownTimer = manaCooldown + Time.time;
            }

            if (canRechargeMana && Time.time > manaCooldownTimer && currentMana <= 2)
            {
                manaCooldownTimer = manaCooldown + Time.time;
                RechargeMana(1);

            }
            else if(currentMana > 2)
            {
                canRechargeMana = false;
            }

            //Animaciones
            animatorController.SetBool("IsRunning", movement != 0);

          
        }
    }

    private void FixedUpdate()
    {
        if (canJump && isGrounded)
        {
            
            isGrounded = false;
            canJump = false;
            myRigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

        
    private void Shoot() //Instancia una bala
    {
        Instantiate(bullet, transform.position + offset, transform.rotation);
        shootingSound.Play();
        cooldownTimer += cooldown;
        currentMana--;
        OnChangeMana.Invoke();
    }

    private void Attack()
    {
        canAttack = false;
        animatorController.SetTrigger("IsPhisicalAttacking");
        attackSound.Play();
        Collider2D collider = Physics2D.OverlapCircle((Vector2)attackPoint.position, attackRadius, enemyDetectionList);
        if (collider != null)
        {
            LifeController life = collider.gameObject.GetComponent<LifeController>();
            if (life != null)
            {
                Debug.Log("Daño al enemigo");
                life.TakeDamage(damage);
                RechargeMana(1);
            }
        }
        canAttack = true;
        slashCooldownTimer = slashCooldown + Time.time;  //Comienza el attack cooldown
    }

    void Flip() //Solo flippea al personaje
    {
        transform.Rotate(0f, 180f, 0f);
        facingRight = !facingRight;
    }

    private void OnTakeDamageListener(int currentLife, int damage)
    {
        animatorController.SetTrigger("TakeDamage");
        damageSound.Play();
    }

    public bool CanHeadKill()
    {
        return !isGrounded;
    }

    //MANA
    public float GetManaPercentage()
    {
        return (float)currentMana / maxMana;
    }
     
    public bool CanRechargeMana()
    {
        bool response = false;
        if(currentMana < maxMana)
        {
            response = true;
        }
        return response;
    }

    public int GetMaxMana()
    {
        return maxMana;
    }

    public void RechargeMana(int mana)
    {
        if(currentMana <= (maxMana - mana))
        {
            currentMana += mana;
            OnChangeMana.Invoke();
            rechargeAmmoSound.Play();
        } else if(currentMana < maxMana)
        {
            currentMana = maxMana;
            OnChangeMana.Invoke();
            rechargeAmmoSound.Play();
        }
    }

    public void PlayerActive(bool status)
    {
        this.gameObject.SetActive(status);
    }

    public void SetCurrentPosition(Vector2 spawnPosition)
    {
        transform.position = spawnPosition;
    }
}

