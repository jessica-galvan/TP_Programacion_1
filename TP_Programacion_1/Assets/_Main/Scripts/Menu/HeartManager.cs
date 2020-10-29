using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> hearts = new List<GameObject>();

    void Start()
    {
        //Se suscribe a la vida del player, cuando hay algun cambio en la vida, se quita o agrega vida. 
        //Si vamos a poner medio corazon, quizas la vida del jugador deberia ser el doble, y cada vez que tiene la vida en impar, el ultimo corazon de la lista tendria que tener la mitad. De la misma manera, el ultimo corazon seria eliminado o completado si se quita o se agregan vida
    }

    void Update()
    {
        
    }
}
