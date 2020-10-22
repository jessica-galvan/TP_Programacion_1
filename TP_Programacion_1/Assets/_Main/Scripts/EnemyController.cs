using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Health Settings")]
    public LifeController lifeController = null;
    private Animator animatorController;
    
    [Header("Attack Settings")]
    [SerializeField] private int bodyDamage = 5;
    public bool facingRight = false;
    public bool canAttack = false;
    /*//[SerializeField] private float speed = 0f;
    [SerializeField] private Vector3 offset = Vector3.zero;
    [SerializeField] private float cooldown = 0f;
    private float cooldownTimer = 0f;
    private bool canShoot = true;*/

    [Header("Prefabs Settings")]
    [SerializeField] private GameObject player; 
    //[SerializeField] private GameObject bullet = null;
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
        //player = GameObject.Find("Player").GetComponent<PlayerController>();
        //gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        //gameManager.addOneInEnemyCounter();
        //Debug.Log(player);
    }

    void Update()
    {
        /*if (canAttack) //Si el enemigo puede atacar es porque el player esta dentro de al trigger zone
        {
            if(player.transform.position.x > transform.position.x && !facingRight) //estoy a la derecha
            {
                BackFlip();
            } else if(player.transform.position.x < transform.position.x && facingRight) //estoy a la izquierda
            {
                BackFlip();
            }

            if (Time.time > cooldownTimer && canShoot) //cooldown y que ataque
            {
                canShoot = false;
                Shoot();
            }
        }*/
    }

    public void BackFlip()
    {
        transform.Rotate(0f, 180f, 0f);
        facingRight = !facingRight;
    }

    /*private void OnShootListener()
    {
        shootingSound.Play();
        animatorController.SetTrigger("IsShooting");
        Instantiate(bullet, transform.position + offset, transform.rotation);
        cooldownTimer += cooldown;
        canShoot = true;
    }*/

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

    //SET & GET
    public void SetGameManager(GameManager _gameManager)
    {
        gameManager = _gameManager;
    }

    public void SetPlayer(GameObject _player)
    {
        player = _player;
    }

    public GameObject GetPlayer()
    {
        return player;
    }
}
