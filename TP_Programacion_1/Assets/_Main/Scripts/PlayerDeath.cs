using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField] private float timer = 1f;
    [SerializeField] private AudioSource deathSound = null;

    void Start()
    {
        deathSound.Play();
        timer += Time.time;
    }

    void Update()
    {
        if (Time.time > timer)
        {
            Destroy(gameObject);
        }
    }
}
