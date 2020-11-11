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
    private bool playerWasHere;
    private bool isBarrierActive;
    private bool checkDirection;
    private bool canStartTimer;
    private bool canReturnToSpawnPoint;


    [Header("Prefab Settings")]
    [SerializeField] private GameObject player;
    [SerializeField] private GameManager gameManager;
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
    //private Animator animatorController = null;
    private bool canMove;
    private bool facingRight;
    private float moveTimer = 0f;


    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        //animatorController = GetComponent<Animator>();
        enemy = GetComponent<EnemyController>();
        canMove = true;
        canAttack = true;
        isBarrierActive = true;
        currentSpeed = normalSpeed;
        barrierLeft = Instantiate(invisibleBarrierPrefab, leftX.transform.position, transform.rotation);
        barrierRight = Instantiate(invisibleBarrierPrefab, rightX.transform.position, transform.rotation);
        spawnPoint = transform.position;
        playerDetectionDistance = Vector2.Distance(transform.position, playerDetectionPoint.position);
    }

    // Update is called once per frame
    void Update()
    {
        //CUANDO VEAS AL PLAYER
        RaycastHit2D hitPlayer = Physics2D.Raycast(transform.position, transform.right, playerDetectionDistance, playerDetectionList);
        if (hitPlayer)
        {
            //Desactiva las barreras de patruyar.
            if (isBarrierActive)
            {
                toggleBarriers();
                playerWasHere = true;
                checkDirection = true;
            }
            currentSpeed = followingSpeed;
            //Perseguilo
            Vector2.MoveTowards(player.transform.position, transform.position, currentSpeed);

            //Y si esta a una distancia menor o igual al radio de ataque, atacalo. 
            float distance = Vector2.Distance(hitPlayer.collider.transform.position, attackPoint.position);
            if (distance <= attackRadius && canAttack && Time.time > cooldownTimer)
            {
                Attack();
            }
        } else if (playerWasHere && !hitPlayer)    //Si dejaste de ver al player, se activa playerWashere que inicia "volver al spawn point".
        {
            currentSpeed = normalSpeed;
            checkPlayerTimer = checkPlayerTimeDuration;
            canStartTimer = true;
            playerWasHere = false;
        }

        //PERO espera unos segundos para al spawnPoint
        if (canStartTimer && Time.time > checkPlayerTimer)
        {
            canReturnToSpawnPoint = true;
            canStartTimer = false;
        }

        //Ahora podes volver al punto de spawn
        if (canReturnToSpawnPoint && !isBarrierActive)
        {
            //hace un check de la direcion del spawnpoint
            if (checkDirection)
            {
                checkDirection = false;
                checkSpawnPointDirection();
            }

            //cuando estes cerca, activa las barreras asi patruyas
            float difMax = Vector2.Distance(transform.position, spawnPoint);
            if (difMax < 1f)
            {
                canReturnToSpawnPoint = false;
                toggleBarriers();
            }
        }

        //GroundDetection
        RaycastHit2D hitPatrol = Physics2D.Raycast(groundDetectionPoint.position, Vector2.down, groundDetectionDistance, groundDetectionList);
        if (!hitPatrol)
        {
            BackFlip();
        }

        //Termino animación ataque? Se puede mover
        if (!canMove && Time.time > moveTimer)
        {
            canMove = true;
        }

        //Cooldown Attack Timer
        if(!canAttack && Time.time > cooldownTimer)
        {
            canAttack = true;
        }

    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            rb2d.velocity = transform.right * currentSpeed;
        }
    }

    private void toggleBarriers()
    {
        isBarrierActive = !isBarrierActive;
        barrierLeft.SetActive(isBarrierActive);
        barrierRight.SetActive(isBarrierActive);
    }

    private void Attack()
    {
        canMove = false;
        canAttack = false;
        cooldownTimer += cooldown;
        moveTimer += moveCooldown;
        //animatorController.SetTrigger("IsAttacking");

        Collider2D collider = Physics2D.OverlapCircle((Vector2)attackPoint.position, attackRadius, playerDetectionList);
        Debug.Log(collider);
        if (collider != null)
        {
            LifeController life = collider.gameObject.GetComponent<LifeController>();
            if (life != null)
            {
                life.TakeDamage(damage);
            }
        }
    }

    //Aca chequeamos en que sentido esta mirando el enemigo y en que sentido esta el currentTarget. Si currentTransform es mayor a la posicion del enemigo, y no esta mirando a la derecha...
    private void checkSpawnPointDirection()
    {
        if (spawnPoint.x > transform.position.x && !facingRight)
        {
            transform.Rotate(0f, 180f, 0f);
            facingRight = true;
        }
        else if (spawnPoint.x < transform.position.x && facingRight)
        {
            transform.Rotate(0f, 180f, 0f);
            facingRight = false;
        }
    }

    public void OnDestroy()
    {
        Destroy(barrierLeft);
        Destroy(barrierRight);
    }

    public void BackFlip()
    {
        enemy.BackFlip();
        facingRight = true;
    }

    public void SetPlayer(GameObject _player)
    {
        player = _player;
    }
    public void SetGameManager(GameManager _gameManager)
    {
        gameManager = _gameManager;
    }
}
