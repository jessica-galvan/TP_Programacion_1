using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{
    [Header("Health Settings")]
    public LifeController lifeController = null;
    private Animator animatorController = null;

    [Header("Movement Settings")]
    [SerializeField] private float speed = 0f;
    [SerializeField] private Vector3 offset = Vector3.zero;
    private float movement = 0f;
    private bool facingRight = true; //facingRight es para chequear en que sentido esta mirando el personaje. 

    [Header("Attack Settings")]
    [SerializeField] private int maxAmmo = 6;
    [SerializeField] private int currentAmmo;
    [SerializeField] private float cooldown = 0f;
    private float cooldownTimer = 0f;
    [SerializeField] private GameObject bullet = null;
    [SerializeField] private GameObject rButton = null;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource shootingSound = null;
    [SerializeField] private AudioSource rechargeAmmoSound = null;
    [SerializeField] private AudioSource negativeActionSound = null;
    [SerializeField] private AudioSource damageSound = null;

    void Start ()
    {
       animatorController = GetComponent<Animator>();
       lifeController = GetComponent<LifeController>();
       currentAmmo = maxAmmo;
       lifeController.OnDie.AddListener(OnDieListener);
       lifeController.OnTakeDamage += OnTakeDamageListener;
    }

    void Update()
    {
        //MOVEMENT
        movement = Input.GetAxisRaw("Horizontal") * (speed * Time.deltaTime); //El valor va entre -1 (izquierda) y 1 (derecha). 
        transform.Translate(Mathf.Abs(movement), 0,0); //El Mathf.Abs -> Math Absolute le saca los signos. Esto sirve porque al flippear el personaje siempre se mueve hacia adelante y el Flip me lo rota. 

        if(movement < 0 && facingRight) //Si el movimiento es positivo y esta mirando a la derecha...
        {
            Flip();
            facingRight = false;
        } else if(movement > 0 && !facingRight ) //Si el movimiento es negativo y no esta mirando a la derecha...
        {
            Flip();
            facingRight = true;
        }

        //Disparo
        if(Input.GetMouseButtonDown(0) && Time.time > cooldownTimer && currentAmmo > 0) //Si recibe input de disparo y el cooldown ya no esta y además hay ammo...
        {
            Shoot();
        } else if(Input.GetMouseButtonDown(0) && Time.time > cooldownTimer || Input.GetMouseButtonDown(0) && currentAmmo > 0)
        {
           negativeActionSound.Play();
        }

        //RecargarAmmo
        if (Input.GetKeyDown(KeyCode.R))
        {
            RechargeAmmo();
        }

        //Activa una imagne visiuald e un boton para que el usuario sepa que apretar para recargar las balas. 
        rButton.SetActive(currentAmmo == 0);

        //Animaciones
        if (movement != 0)
        {
            animatorController.SetBool("IsRunning", true);
        } else
        {
            animatorController.SetBool("IsRunning", false);
        }
    }


    private void Shoot() //Instancia una bala
    {
        Instantiate(bullet, transform.position + offset, transform.rotation);
        shootingSound.Play();
        cooldownTimer += cooldown;
        currentAmmo--;
    }

    void Flip() //Solo flippea al personaje
    {
        transform.Rotate(0f, 180f, 0f);
    }

    private void RechargeAmmo()
    {
        if (currentAmmo < maxAmmo)
        {
            currentAmmo = maxAmmo;
            rechargeAmmoSound.Play();
        }
        else
        {
            negativeActionSound.Play();
        }
    }

    private void OnDieListener()
    {

    }

    private void OnTakeDamageListener(int currentLife, int damage)
    {
        animatorController.SetTrigger("TakeDamage");
        damageSound.Play();
    }
}
