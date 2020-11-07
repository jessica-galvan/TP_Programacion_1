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
        //Primer timer, para instanciar luego de la animacion de muerte, la reward.
        if(Time.time > timer1)
        {
            //el control es para que no instancie eternamente. Apenas entra, cambia a false, para frenarlo. Sin esto, lo hace hasta que se destruye el objeto. 
            if(canInstantiate)
            {
                canInstantiate = false;
                GetRandom();
                Instantiate(reward[numberReward], transform.position, transform.rotation);
                rewardSound.Play();
            }
            //Mientras tanto, hay un control del segundo timer, asi el objeto se destruye también. 
            if (Time.time > timer2)
            {
                Destroy(gameObject);
            }
        }
    }
    
    private void GetRandom()
    {
        numberReward = Random.Range(0, reward.Length);
    }
}
