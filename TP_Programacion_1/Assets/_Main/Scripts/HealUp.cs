using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class HealUp : MonoBehaviour
{
    [SerializeField] private int heal= 0;
    [SerializeField] private float timer = 1;
    private  bool canDestroy = false;
    private Animator animatorController;
    private AudioSource healSound = null;
    private SpriteRenderer sprite;

    private void Start()
    {
        healSound = GetComponent<AudioSource>();
        animatorController = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (canDestroy && Time.time > timer) 
        {
            Destroy(gameObject);
        }   
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Si el item que collisiona tiene un tag de player...
        if (collision.tag == "Player")
        {
            //Si tiene un life controller (deberia tenerlo)
            LifeController life = collision.GetComponent<LifeController>();
            if(life != null)
            {
                //Desactivame el animator y el sprite renderer. Además toca el sonido.
                sprite.enabled = false;
                animatorController.enabled = false;
                healSound.Play();
                //Cura al jugador, cambia la variable de destruiye y setear el timer. 
                life.TakeHeal(heal);
                canDestroy = true;
                timer += Time.time;
            }
        }
    }
}
