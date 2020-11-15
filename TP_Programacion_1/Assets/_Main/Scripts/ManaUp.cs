using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaUp : MonoBehaviour
{
    [SerializeField] private GameObject manaLight;
    [SerializeField] private int manaRecharge = 1;
    [SerializeField] private float timer = 1;
    private bool canDestroy = false;
    private Animator animatorController;
    private AudioSource rechargeSound = null;
    private SpriteRenderer sprite;
    private bool canRecharge = true;

    private void Start()
    {
        rechargeSound = GetComponent<AudioSource>();
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
        if (collision.tag == "Player")
        {
            //Si tiene un Player Controller (deberia tenerlo)
            PlayerController player = collision.GetComponent<PlayerController>();
            if (player != null & canRecharge)
            {
                //Si el mana del player es menor al mana maximo
                if (player.CanRechargeMana())
                {
                    //Solo deberia curar una vez. 
                    canRecharge = false;
                    //Desactivame el animator y el sprite renderer. Además toca el sonido.
                    sprite.enabled = false;
                    animatorController.enabled = false;
                    manaLight.SetActive(false);
                    rechargeSound.Play();
                    //Recarga el mana
                    player.RechargeMana(manaRecharge);
                    canDestroy = true;
                    timer += Time.time;
                }
            }
        }
    }
}
