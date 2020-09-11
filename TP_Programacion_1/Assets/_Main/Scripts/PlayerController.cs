using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Health Settings")]
    public LifeController lifeController = null;

    [Header("Movement Settings")]
    [SerializeField] private float speed = 0f;
    [SerializeField] private Vector3 offset = Vector3.zero;
    [SerializeField] private float cooldown = 0f;
    private float cooldownTimer = 0f;
    private float movement = 0f;
    private bool facingRight = true; //facingRight es para chequear en que sentido esta mirando el personaje. 


    [Header("Prefabs Settings")]
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


        if(Input.GetAxisRaw("Fire1") > 0 && Time.time > cooldownTimer) //Si recibe input de disparo y el cooldown ya no esta
        {
            Shoot();
        }

    }

    void Shoot() //Instancia una bala
    {
        Instantiate(bullet, transform.position + offset, transform.rotation);
        //shootingSound.Play();
        cooldownTimer += cooldown;
    }

    void Flip() //Solo flippea al personaje
    {
        transform.Rotate(0f, 180f, 0f);
    }
}
