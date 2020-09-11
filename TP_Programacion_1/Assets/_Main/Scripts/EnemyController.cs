using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Health Settings")]
    public LifeController lifeController = null;

    [Header("Movement Settings")]
    //[SerializeField] private float speed = 0f;
    [SerializeField] private Vector3 offset = Vector3.zero;
    [SerializeField] private float cooldown = 0f;
    private float cooldownTimer = 0f;
    public bool canAttack = false;

   

    [Header("Prefabs Settings")]
    //[SerializeField] private GameObject Player = null; //Si quiero que el enemigo siga al player... 
    [SerializeField] private GameObject bullet = null;

    /*[Header("Audio Sources")]
    [SerializeField] private AudioSource shootingSound = null;
    [SerializeField] private AudioSource deathSound = null;
    [SerializeField] private AudioSource damageSound = null;*/

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canAttack && Time.time > cooldownTimer)
            Shoot();
    }

    void Shoot()
    {
        Instantiate(bullet, transform.position + offset, transform.rotation);
        //shootingSound.Play();
        cooldownTimer += cooldown;
    }
}
