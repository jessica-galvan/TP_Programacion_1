using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Header("Health Settings")]
    [SerializeField] private int maxLife;
    private int currentLife;
    public LifeController lifeController;

    [Header("Movement Settings")]
    [SerializeField] private float speed;

    // Start is called before the first frame update
    void Start()
    {
        maxLife = currentLife;
    }

    // Update is called once per frame
    void Update()
    {
        //MOVEMENT
        if (Input.GetKey("left"))
        {
            transform.position -= transform.right * speed * Time.deltaTime;
        }

        if (Input.GetKey("right"))
        {
            transform.position += transform.right * speed * Time.deltaTime;
        }
    }
}
