using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Health Settings")]
    public LifeController lifeController = null;
    //private Animator animatorController;
    
    [Header("Attack Settings")]
    [SerializeField] private int bodyDamage = 5;
    public bool facingRight = false;
    public bool canAttack = false;

    [Header("Prefabs Settings")]
    [SerializeField] private GameObject player; 
    [SerializeField] private GameManager gameManager = null;
    [SerializeField] private GameObject canvas = null;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource shootingSound = null;
    [SerializeField] private AudioSource damageSound = null;

    void Start()
    {
        //animatorController = GetComponent<Animator>();
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
