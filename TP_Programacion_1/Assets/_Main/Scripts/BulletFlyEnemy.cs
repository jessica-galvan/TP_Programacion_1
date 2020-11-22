using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFlyEnemy : MonoBehaviour
{
    [SerializeField] private float speed = 0f;
    [SerializeField] private int damage = 1;
    private Rigidbody2D rb2;
    private PlayerController player;
    private Vector2 movement;
    private Vector2 direction;
    private Vector2 spawnPosition;

    void Start()
    {
        rb2 = GetComponent<Rigidbody2D>();
        spawnPosition = transform.position;
        player = GameObject.FindObjectOfType<PlayerController>();
        direction = player.transform.position - (Vector3)spawnPosition;
        direction.Normalize();
    }

    private void Update()
    {
        transform.position += (Vector3)direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision);
        LifeController life = collision.GetComponent<LifeController>(); //Si el item tiene un life controller.... (ya la matrix de fisica va decidir si detecta la collision)
        if (life != null) //si no tiene un life controller el item con el que collisiono, va a ser null. 
        {
            life.TakeDamage(damage);
            //Destroy(gameObject);
        }
        Destroy(gameObject);
    }

    public void SetBullet(PlayerController _player)
    {
        player = _player;
        movement = (player.transform.position - transform.position).normalized * speed;
        rb2.velocity = new Vector2(movement.x, movement.y);
    }
}
