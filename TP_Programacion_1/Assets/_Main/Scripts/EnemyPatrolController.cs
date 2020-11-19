using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolController : MonoBehaviour
{
    [Header("Patrol Settings")]
    [SerializeField] private float speed;
    [SerializeField] private Transform leftX;
    [SerializeField] private Transform rightX;
    [SerializeField] private LayerMask groundDetectionList;
    [SerializeField] private float groundDetectionDistance = 1f;
    private Vector2 rightPoint;
    private Vector2 leftPoint;
    private Vector2 currentTarget;
    private bool facingRight;
    private EnemyController enemy;

    [Header("Prefab Settings")]
    [SerializeField] private GameObject player;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Transform groundDetectionPoint;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private Transform detectionPoint;


    [Header("Audio Sources")]
    [SerializeField] private AudioSource attackSound = null;

    [Header("Attack Settings")]
    [SerializeField] private LayerMask playerDetectionList;
    [SerializeField] private float attackRadius = 1f;
    [SerializeField] private int damage = 5;
    [SerializeField] private float attackTimeDuration = 1f;
    [SerializeField] private float cooldown = 5f;
    [SerializeField] private float moveCooldown = 0.8f;
    private float cooldownTimer = 0f;
    private bool canAttack;
    private float playerDetectionDistance;


    //Extras
    private Rigidbody2D rb2d;
    private Animator animatorController = null;
    private bool canMove;
    private float moveTimer = 0f;

    private void Awake() 
    {
       
    }
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animatorController = GetComponent<Animator>();
        enemy = GetComponent<EnemyController>();
        canMove = true;
        canAttack = true;
        rightPoint = rightX.position;
        leftPoint = leftX.position;
        currentTarget = leftPoint;
        playerDetectionDistance = Mathf.Abs(detectionPoint.position.x);
    }

    void Update()
    {
        //CUANDO VEAS AL PLAYER
        RaycastHit2D hitPlayer = Physics2D.Raycast(transform.position, transform.right, playerDetectionDistance, playerDetectionList);
        if (hitPlayer && !gameManager.isFreeze)
        {
            //Perseguilo
            Vector2.MoveTowards(player.transform.position, transform.position, speed * 2);
            
            //Y si esta a una distancia menor o igual al radio de ataque, atacalo. 
            float distance = Vector2.Distance(hitPlayer.collider.transform.position, attackPoint.position);
            if (distance <= attackRadius && canAttack && Time.time > cooldownTimer)
            {
                Attack();
            }
        }
        else
        {
            //Sino detectas al juego, patruya
            checkCurrentTargetDirection();
            RaycastHit2D hitPatrol = Physics2D.Raycast(groundDetectionPoint.position, Vector2.down, groundDetectionDistance, groundDetectionList);
            //Si no detecta suelo, BackFlip()
            if (!hitPatrol)
            {
                BackFlip();
            } else
            {
                //Patruya tranquilo y cuando tu distancia sea menor a tanto... rota.
                float difMax = Vector2.Distance(transform.position, currentTarget);
                Debug.Log(difMax);
                if (difMax <= 1f)
                {
                    BackFlip();
                } 
            }

        }

        //Termino animación ataque? Se puede mover
        if (!canMove && Time.time > moveTimer)
        {
            canMove = true;
        }

    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            rb2d.velocity = transform.right * speed;
        }
    }

    private void Attack()
    {
        canMove = false;
        canAttack = false;
        cooldownTimer += cooldown;
        moveTimer += moveCooldown;
        animatorController.SetTrigger("IsAttacking");

        Collider2D collider = Physics2D.OverlapCircle((Vector2)attackPoint.position, attackRadius, playerDetectionList);
        Debug.Log(collider);
        if (collider != null)
        {
            LifeController life = collider.gameObject.GetComponent<LifeController>();
            if (life != null)
            {
                life.TakeDamage(damage);
            }
            canAttack = true;
            //TIMER
        }
    }

    private void checkCurrentTargetDirection()
    {
        //Aca chequeamos en que sentido esta mirando el enemigo y en que sentido esta el currentTarget. Si currentTransform es mayor a la posicion del enemigo, y no esta mirando a la derecha...
        if(currentTarget.x > transform.position.x && !facingRight)
        {
            transform.Rotate(0f, 180f, 0f);
            facingRight = true;
        } else if(currentTarget.x < transform.position.x && facingRight)
        {
            transform.Rotate(0f, 180f, 0f);
            facingRight = false;
        }
    }

    private void BackFlip()
    {
        enemy.BackFlip();
        facingRight = true;
        currentTarget = currentTarget == leftPoint ? rightPoint : leftPoint;
    }

    public void SetPlayer(GameObject _player)
    {
        player = _player;
    }
    public void SetGameManager(GameManager _gameManager)
    {
        gameManager = _gameManager;
    }

    private void OnTakeDamageListener(int currentLife, int damage)
    {
        animatorController.SetTrigger("TakeDamage");
        //damageSound.Play();
    }
}
