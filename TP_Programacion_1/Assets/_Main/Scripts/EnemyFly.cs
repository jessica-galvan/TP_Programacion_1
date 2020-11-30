using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFly : MonoBehaviour
{
    [Header("Patrol Settings")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private GameObject leftX;
    [SerializeField] private GameObject rightX;
    private GameObject barrierLeft;
    private GameObject barrierRight;
    private EnemyController enemy;
    private Vector2 spawnPoint;

    [Header("Prefab Settings")]
    [SerializeField] private GameObject invisibleBarrierPrefab;
    [SerializeField] private GameObject bullet;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource attackSound = null;

    [Header("Attack Settings")]
    [SerializeField] private float attackTimeDuration = 1f;
    [SerializeField] private float cooldown = 5f;
    [SerializeField] private float moveCooldown = 0.8f;
    [SerializeField] private Vector3 offset = Vector3.zero;
    private float cooldownTimer = 0f;
    private bool canShoot;

    //Extras
    private Rigidbody2D rb2d;
    private GameObject player;
    private Animator animatorController = null;
    private bool canMove;
    private bool facingRight;
    private float moveTimer = 0f;

    private GameManager gameManager;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animatorController = GetComponent<Animator>();
        //animatorController.SetBool("Fly", true); //La idea es que siempre que no este atacando o muriendo, el personaje vuele. No necesita una animación de idle, es la misma que cuando se mueve.
        gameManager.OnPlayerRespawn.AddListener(OnPlayerRespawnListener);
        canMove = true;
        enemy = GetComponent<EnemyController>();
        canShoot = true;
        spawnPoint = transform.position;
        barrierLeft = Instantiate(invisibleBarrierPrefab, leftX.transform.position, transform.rotation);
        barrierLeft.GetComponent<PatrolEnemyFlip>().SetIsPatrol(false);
        barrierRight = Instantiate(invisibleBarrierPrefab, rightX.transform.position, transform.rotation);
        barrierRight.GetComponent<PatrolEnemyFlip>().SetIsPatrol(false);
    }

    void Update()
    {
        if (!gameManager.isFreeze)
        {
            if (enemy.canAttack) //Si el enemigo puede atacar es porque el player esta dentro de al trigger zone
            {
                if (canShoot && Time.time > cooldownTimer) //cooldown y que ataque
                {
                    canShoot = false;
                    //canMove = false;
                    Attack();
                }
            }

            /*if (!canMove && Time.time > moveTimer) //Termino animación ataque? Se puede mover
            {
                canMove = true;
            }*/
        }
    }

    private void FixedUpdate()
    {
        if (canMove && !gameManager.isFreeze)
        {
            rb2d.velocity = transform.right * speed;
        }
    }

    public void BackFlip()
    {
        enemy.BackFlip();
        facingRight = !facingRight;
    }

    private void Attack()
    {
        canShoot = false;
        //canMove = false;
        //moveTimer = moveCooldown + Time.time;
        animatorController.SetTrigger("IsAttacking");
        attackSound.Play();
        Instantiate(bullet, transform.position + offset, Quaternion.identity);
        cooldownTimer = cooldown + Time.time; 
        canShoot = true;
    }


    public void OnDestroy()
    {
        Destroy(barrierLeft);
        Destroy(barrierRight);
    }
    public void SetGameManager(GameManager _gameManager)
    {
        gameManager = _gameManager;
    }

    public void SetPlayer(GameObject _player)
    {
        player = _player;
    }

    public void OnPlayerRespawnListener()
    {
        transform.position = spawnPoint;
    }

}
