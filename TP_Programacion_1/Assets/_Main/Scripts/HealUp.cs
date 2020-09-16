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
        if (collision.tag == "Player")
        {
            //animatorController.SetBool("Dissapear", true);
            sprite.enabled = false;
            animatorController.enabled = false;
            healSound.Play();
            LifeController life = collision.GetComponent<LifeController>();
            life.TakeHeal(heal);
            canDestroy = true;
            timer += Time.time;
        }
    }
}
