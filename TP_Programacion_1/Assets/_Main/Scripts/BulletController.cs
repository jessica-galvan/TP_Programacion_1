using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private float speed = 0f;
    [SerializeField] private int damage = 1;

    void Update()
    {
        transform.Translate(speed * Time.deltaTime,0,0); //la bala va a moverse hacia "adelante" del sentido en donde salio. 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        LifeController life = collision.GetComponent<LifeController>(); //Si el item tiene un life controller.... (ya la matrix de fisica va decidir si detecta la collision)
        if (life != null) //si no tiene un life controller el item con el que collisiono, va a ser null. 
        {
            life.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
