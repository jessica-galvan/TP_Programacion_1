using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolController : MonoBehaviour
{
    [Header("Patrol Settings")]
    [SerializeField] private float speed;
    [SerializeField] private Transform maxX;
    [SerializeField] private Transform minX;
    [SerializeField] private LayerMask groundDetectionList;
    [SerializeField] private float groundDetectionDistance = 1f;
    private float minDistance;
    private float maxDistance;

    [Header("Prefab Settings")]
    [SerializeField] private GameObject player;
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
    //private Animator animatorController = null;
    private bool facingRight;
    private bool canMove;
    private float moveTimer = 0f;


    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        //animatorController = GetComponent<Animator>();
        //enemyController = GetComponent<EnemyController>();
        canMove = true;
        canAttack = true;
        minDistance = minX.position.x;
        maxDistance = maxX.position.x;
        playerDetectionDistance = Mathf.Abs(detectionPoint.position.x);
    }

    void Update()
    {
        //Simple PatrolArea CODE
        /*if(transform.position.x >= maxDistance)
        {
            BackFlip();
        }
        if (transform.position.x <= minDistance)
        {
            BackFlip();
        }

        //Si no detectas suelo, gira
        RaycastHit2D hitPatrol = Physics2D.Raycast(groundDetectionPoint.position, Vector2.down, groundDetectionDistance, groundDetectionList);
        if (!hitPatrol)
        {
            BackFlip();
        }*/

        Vector2 hitDirection = facingRight ? Vector2.right : -Vector2.right;
        //CUANDO VEAS AL PLAYER, PERSEGUILO
        RaycastHit2D hitPlayer = Physics2D.Raycast(transform.position, hitDirection, playerDetectionDistance, playerDetectionList);
        if (hitPlayer)
        {
            //Perseguilo
            //transform.position += hitPlayer.collider.transform.position;

            //Y si esta a una distancia menor o igual al radio de ataque, atacalo. 
            float distance = Vector2.Distance(hitPlayer.collider.transform.position, detectionPoint.position);
            if(distance <= attackRadius && canAttack && Time.time > cooldownTimer)
                Attack();
        }

        //Termino animación ataque? Se puede mover
        //if (!canMove && Time.time > moveTimer)
        //    canMove = true;
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
        //animatorController.SetTrigger("IsAttacking");
        Collider2D player = Physics2D.OverlapCircle((Vector2)detectionPoint.position, attackRadius, playerDetectionList);
        LifeController life =  player.gameObject.GetComponent<LifeController>();
        if (life != null)
        {
            life.TakeDamage(damage);
        }
        canAttack = true;
        Debug.Log("Ejecute ataque");
    }

    private void BackFlip()
    {
        transform.Rotate(0f, 180f, 0f);
        facingRight = !facingRight;
    }

    public void SetPlayer(GameObject _player)
    {
        player = _player;
    }
}
