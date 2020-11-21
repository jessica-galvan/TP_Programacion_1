using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStaticController : MonoBehaviour
{
    [Header("Attack Settings")]
    [SerializeField] private Vector3 offset = Vector3.zero;
    [SerializeField] private float cooldown = 2f;
    private float cooldownTimer = 0f;
    private bool canShoot = true;

    [Header("Prefabs Settings")]
    [SerializeField] private GameObject bullet = null;
    private EnemyController enemyController = null;
    private GameManager gameManager = null;
    private Animator animatorController = null;
    private GameObject player = null;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource shootingSound = null;

    void Start()
    {
        animatorController = GetComponent<Animator>();
        enemyController = GetComponent<EnemyController>();
    }

    void Update()
    {
        if (!gameManager.isFreeze)
        {
            if (enemyController.canAttack) //Si el enemigo puede atacar es porque el player esta dentro de al trigger zone
            {
                if (player.transform.position.x > transform.position.x && !enemyController.facingRight) //estoy a la derecha
                {
                    enemyController.BackFlip();
                }
                else if (player.transform.position.x < transform.position.x && enemyController.facingRight) //estoy a la izquierda
                {
                    enemyController.BackFlip();
                }

                if (canShoot && Time.time > cooldownTimer) //cooldown y que ataque
                {
                    canShoot = false;
                    Shoot();
                }
            }
        }
    }

    private void Shoot()
    {
        canShoot = false;
        shootingSound.Play();
        animatorController.SetTrigger("IsShooting");
        Instantiate(bullet, transform.position + offset, transform.rotation);
        cooldownTimer += cooldown;
        canShoot = true;
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
