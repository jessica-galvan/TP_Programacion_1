using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeController : MonoBehaviour
{
    [SerializeField] private int maxLife = 0;
    [SerializeField] private int currentLife = 0;

    // Start is called before the first frame update
    void Start()
    {
        //Si currentLife esta vacia.. igualamelo a MaxLife
        currentLife = maxLife;
    }

    public void TakeDamage(int damage)
    {
        currentLife -= damage;

        if (currentLife <= 0)
            Destroy(gameObject);
    }
}
