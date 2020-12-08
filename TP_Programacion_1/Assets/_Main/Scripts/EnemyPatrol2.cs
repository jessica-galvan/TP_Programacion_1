using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol2 : MonoBehaviour
{
    [Header("Patrol Settings")]
    [SerializeField] private float normalSpeed = 5f;
    [SerializeField] private float followingSpeed = 15f;
    [SerializeField] private GameObject leftX;
    [SerializeField] private GameObject rightX;
    [SerializeField] private LayerMask groundDetectionList;
    [SerializeField] private float groundDetectionDistance = 1f;
    [SerializeField] private float checkPlayerTimeDuration = 5f;
    private float currentSpeed;
    private float checkPlayerTimer = 0f;
    private Vector2 spawnPoint;
    private GameObject barrierLeft;
    private GameObject barrierRight;
    private EnemyController enemy;
    private bool followingPlayer;
    private bool isBarrierActive;
    private bool checkDirection;
    private bool canReturnToSpawnPoint;

    [Header("Prefab Settings")]
    [SerializeField] private Transform groundDetectionPoint;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private Transform playerDetectionPoint;
    [SerializeField] private GameObject invisibleBarrierPrefab;

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
    private bool facingRight;
    private float moveTimer = 0f;
    private GameManager gameManager;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animatorController = GetComponent<Animator>();
        enemy = GetComponent<EnemyController>();
        canMove = true;
        canAttack = true;
        isBarrierActive = true;
        currentSpeed = normalSpeed;
        barrierLeft = Instantiate(invisibleBarrierPrefab, leftX.transform.position, transform.rotation);
        barrierLeft.GetComponent<PatrolEnemyFlip>().SetIsPatrol(true);
        barrierRight = Instantiate(invisibleBarrierPrefab, rightX.transform.position, transform.rotation);
        barrierRight.GetComponent<PatrolEnemyFlip>().SetIsPatrol(true);
        spawnPoint = transform.position;
        playerDetectionDistance = Vector2.Distance(transform.position, playerDetectionPoint.position);  //Con esto sacamos a cuanta distancia puede ver. 
    }

    void Update()
    {
        if (!gameManager.isFreeze)
        {
            RaycastHit2D hitPlayer = Physics2D.Raycast(transform.position, transform.right, playerDetectionDistance, playerDetectionList);
            if (hitPlayer) //CUANDO VEAS AL PLAYER
            {
                var playerController = hitPlayer.collider.GetComponent<PlayerController>();
                if (!followingPlayer && playerController)  //Desactiva las barreras de patruyar para perseguirlo
                {
                    statusBarriers(false);
                    followingPlayer = true;
                    checkDirection = true;
                    canReturnToSpawnPoint = false;
                    currentSpeed = followingSpeed;
                }

                float distance = Vector2.Distance(hitPlayer.collider.transform.position, attackPoint.position);
                if (distance <= attackRadius) //Y si esta a una distancia menor o igual al radio de ataque, dejate de mover. 
                {
                    canMove = false;
                    if (canAttack && Time.time > cooldownTimer)
                    {
                        Attack();
                    }
                }

                if (!canMove && Time.time > moveTimer && distance > attackRadius) //Termino animación ataque? Se puede mover
                {
                    canMove = true;
                }
            }
            else   //Si dejaste de ver al player, espera un rato y patrulla
            {
                if (followingPlayer) //Si estabas siguiendo al player
                {
                    checkPlayerTimer = checkPlayerTimeDuration + Time.time;
                    canMove = false;
                    currentSpeed = normalSpeed;
                    followingPlayer = false;
                }

                if (!canReturnToSpawnPoint && Time.time > checkPlayerTimer) //PERO espera unos segundos para al spawnPoint
                {
                    canReturnToSpawnPoint = true;
                    canMove = true;
                }

                if (canReturnToSpawnPoint && !isBarrierActive) //Ahora podes volver al punto de spawn
                {
                    if (checkDirection) //hace un check de la direcion del spawnpoint
                    {
                        checkDirection = false; //pero solo una vez
                        checkSpawnPointDirection();
                    }

                    float difMax = Vector2.Distance(transform.position, spawnPoint);  //cuando estes cerca, activa las barreras asi patruyas
                    if (difMax < 1f)
                    {
                        canReturnToSpawnPoint = false;
                        statusBarriers(true);
                    }
                }
            }

            animatorController.SetBool("Walk", canMove); //Mientras canMove sea true, vas a caminar
            if (canMove)
            {
                animatorController.SetFloat("Speed", currentSpeed);
            }


            RaycastHit2D hitPatrol = Physics2D.Raycast(groundDetectionPoint.position, Vector2.down, groundDetectionDistance, groundDetectionList);
            if (!hitPatrol)      //GroundDetection esta funcionando todo el tiempo, si deja de detectar ground, va a flippear.
            {
                BackFlip();
            }

            if (!canAttack && Time.time > cooldownTimer) //Cooldown Attack Timer
            {
                canAttack = true;
            }
        }
    }

    private void FixedUpdate()
    {
        if (canMove && !gameManager.isFreeze)
        {
            rb2d.velocity = transform.right * currentSpeed;
        }
    }

    private void statusBarriers(bool status)
    {
        isBarrierActive = status;
        barrierLeft.SetActive(isBarrierActive);
        barrierRight.SetActive(isBarrierActive);
    }

    private void Attack()
    {
        canMove = false; //mientras hace la animación de ataque, no deberia moverse
        canAttack = false;

        moveTimer = moveCooldown + Time.time;
        attackSound.Play();
        animatorController.SetTrigger("IsAttacking");

        Collider2D collider = Physics2D.OverlapCircle((Vector2)attackPoint.position, attackRadius, playerDetectionList);
        if (collider != null)
        {
            LifeController life = collider.gameObject.GetComponent<LifeController>();
            if (life != null)
            {
                life.TakeDamage(damage);
            }
        }
       
        cooldownTimer = cooldown + Time.time;  //Comienza el attack cooldown
    }

    private void checkSpawnPointDirection()     //Aca chequeamos en que sentido esta mirando el enemigo y en que sentido esta el spawn point. 
    {
        if (spawnPoint.x > transform.position.x && !facingRight) //Si el spawnpint es mayor a la posicion del enemigo, y no esta mirando a la derecha...
        {
            BackFlip();
        } else if(spawnPoint.x < transform.position.x && facingRight) //si o si esta este else if porque solo tiene que flipear si esta mirando en la direccion contraria, sino ni flipea. 
        {
            BackFlip();
        }
    }

    public void BackFlip()
    {
        enemy.BackFlip();
        facingRight = !facingRight;
    }

    public void OnDestroy() //Para que destruya las barreras cuando se destruye el objeto. 
    {
        Destroy(barrierLeft);
        Destroy(barrierRight);
    }
    public void SetGameManager(GameManager _gameManager)
    {
        gameManager = _gameManager;
    }
}
