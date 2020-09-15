using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class HealUp : MonoBehaviour
{
    [SerializeField] private int heal= 0;
    [SerializeField] private AudioSource healSound = null;
    [SerializeField] private float timer;
    [SerializeField] private  bool canDestroy = false;

    private void Start()
    {
        healSound = GetComponent<AudioSource>();
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
            LifeController life = collision.GetComponent<LifeController>();
            life.TakeHeal(heal);
            healSound.Play();
            gameObject.GetComponent<Renderer>().enabled = true;
            canDestroy = true;
            timer += Time.time;
        }
    }
}
