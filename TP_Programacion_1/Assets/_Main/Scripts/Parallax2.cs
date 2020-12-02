using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax2 : MonoBehaviour
{
    [SerializeField] private Transform player;
     public Vector3 offset;
   
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
      Vector3 position = transform.position ;
      position.y = (player.position + offset).y;
      transform.position = position ;

    }
}
