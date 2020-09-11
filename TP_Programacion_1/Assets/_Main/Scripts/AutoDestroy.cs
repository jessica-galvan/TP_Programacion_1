using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    //El autodestroy se puede reutilizar, por eso se creo aparte. Cualquier game object que se lo agregue, se le pone un tiempo de vida y cuando el objeto se crea empieza a correr el timer. 
    [SerializeField] private float timer;

    void Start()
    {
        //Se hace esto porque la funcion Time.time empieza a correr cuando se ejecuta el programa. Se le suma el tiempo que se setea en el timer y luego en el update...
        timer += Time.time;
    }

    void Update()
    {
        //Acá se chequea el tiempo, una vez que el Time.time es mayor al timer, el objeto se destruye. 
        if(Time.time > timer)
        {
            Destroy(gameObject);
        }
        
    }
}
