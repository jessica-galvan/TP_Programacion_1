using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField] private float timer = 1f;
    [SerializeField] private AudioSource deathSound = null;

    void Start()
    {
        //Esto ejecuta el sonido de muerte apenas se instacia y setea el timer. Además, en la pantalla se ve la animación de muerte. 
        deathSound.Play();
        timer += Time.time;
    }

    void Update()
    {
        //Cuando pasa el tiempo, el personaje muere. 
        if (Time.time > timer)
        {
            Destroy(gameObject);
        }
    }
}
