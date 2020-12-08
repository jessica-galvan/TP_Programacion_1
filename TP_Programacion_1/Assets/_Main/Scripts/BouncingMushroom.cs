using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingMushroom : MonoBehaviour
{
    [SerializeField] bool canBounce = true;
    [SerializeField] float jumpForce = 1f;
    [SerializeField] private AudioSource bounceSound;
    private Animator animatorController;
    

    private void Awake() {
        animatorController = GetComponentInParent<Animator>();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
        if (player)
        {
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            bounceSound.Play();
            animatorController.SetTrigger("IsJumping");
        }
    }
}
