using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class HealUp : MonoBehaviour
{
    [SerializeField] private GameObject healLight;
    [SerializeField] private int heal= 0;
    [SerializeField] private float timer = 1;
    private  bool canDestroy = false;
    private AudioSource healSound = null;
    private SpriteRenderer sprite;
    private bool canHeal = true;

    private void Start()
    {
        healSound = GetComponent<AudioSource>();
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
        if (collision.tag == "Player")   //Si el item que collisiona tiene un tag de player...
        {
            LifeController life = collision.GetComponent<LifeController>();
            if(canHeal && life.CanHeal())
            {
                canHeal = false;                 //Solo deberia curar una vez. 
                sprite.enabled = false;                 //Desactivame el animator y el sprite renderer. Además toca el sonido.
                healLight.SetActive(false);
                healSound.Play();
                life.TakeHeal(heal);                 //Cura al jugador, cambia la variable de destruiye y setear el timer. 
                canDestroy = true;
                timer += Time.time;
            }
        }
    }
}
