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
        sprite.enabled = false;
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
            PlayerController player = collision.GetComponent<PlayerController>();
            if (player != null & canRecharge)  //Si tiene un Player Controller (deberia tenerlo)
            {
                if (player.CanRechargeMana()) //Si el mana del player es menor al mana maximo
                {
                    Debug.Log("Stage1");
                    canRecharge = false; //Solo deberia curar una vez. 
                    sprite.enabled = false; //Desactivame el animator y el sprite renderer. Además toca el sonido.
                    animatorController.enabled = false;
                    manaLight.SetActive(false);
                    rechargeSound.Play(); 
                    player.RechargeMana(manaRecharge); //Recarga el mana
                    canDestroy = true; //Empeza el timer
                    timer += Time.time; //Set time
                }
            }
        }
    }
}
