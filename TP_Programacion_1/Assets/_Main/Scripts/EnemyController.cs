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

    [Header("Prefabs Settings")]
    [SerializeField] private GameObject canvas = null;
    private GameManager gameManager = null;
    private GameObject player;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource shootingSound = null;
    [SerializeField] private AudioSource damageSound = null;


    private void Awake() 
    {
        animatorController = GetComponent<Animator>();
    }
    void Start()
    {
        animatorController = GetComponent<Animator>();
        lifeController = GetComponent<LifeController>();
        lifeController.OnTakeDamage += OnTakeDamageListener;
        lifeController.OnDie.AddListener(OnDieListener);
    }

    public void BackFlip()
    {
        transform.Rotate(0f, 180f, 0f);
        canvas.transform.Rotate(0f, 180f, 0f);
        facingRight = !facingRight;
    }

    private void OnTakeDamageListener(int currentLife, int damage)
    {
        animatorController.SetTrigger("TakeDamage");
        damageSound.Play();
    }

    private void OnDieListener()
    {
        gameManager.takeOneEnemy();
    }

    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
        LifeController life = collision.gameObject.GetComponent<LifeController>();
        if (life != null && !player.CanHeadKill())
        {
            life.TakeDamage(bodyDamage);
        }
    }*/

    //SET & GET
    public void SetGameManager(GameManager _gameManager)
    {
        gameManager = _gameManager;
    }

    public void SetPlayer(GameObject _player)
    {
        player = _player;
    }
}
