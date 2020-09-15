using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class DeathController : MonoBehaviour
{
    [SerializeField] private float timer1;
    [SerializeField] private float timer2;
    [SerializeField] private bool canInstantiate = true;
    [SerializeField] private GameObject reward = null;
    [SerializeField] private AudioSource deathSound = null;
    [SerializeField] private AudioSource rewardSound = null;

    // Start is called before the first frame update
    void Start()
    {
        timer1 += Time.time;
        timer2 += Time.time;
        deathSound.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > timer1)
        {
            if(canInstantiate)
            {
                canInstantiate = false;
                Instantiate(reward, transform.position, transform.rotation);
                rewardSound.Play();
            }
            if (Time.time > timer2)
            {
                Destroy(gameObject);
            }
        }
    }
}
