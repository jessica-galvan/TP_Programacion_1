using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class DeathController : MonoBehaviour
{
    [SerializeField] private float timer1;
    [SerializeField] private float timer2;
    [SerializeField] private bool canInstantiate = true;
    [SerializeField] private GameObject [] reward = new GameObject[2];
    [SerializeField] private AudioSource deathSound = null;
    [SerializeField] private AudioSource rewardSound = null;
    private int numberReward;

    void Start()
    {
        timer1 += Time.time;
        timer2 += Time.time;
        deathSound.Play();
    }

    void Update()
    {
        if(Time.time > timer1)         //Primer timer, para instanciar luego de la animacion de muerte, la reward.
        {  
            if(canInstantiate)  //el control es para que no instancie eternamente. Apenas entra, cambia a false, para frenarlo. Sin esto, lo hace hasta que se destruye el objeto. 
            {
                canInstantiate = false;
                numberReward = Random.Range(0, reward.Length); //Para obtener un numero random entre reward
                Instantiate(reward[numberReward], transform.position, transform.rotation);
                rewardSound.Play();
            }
            
            if (Time.time > timer2) //Mientras tanto, hay un control del segundo timer, asi el objeto se destruye también. 
            {
                Destroy(gameObject);
            }
        }
    }
}
