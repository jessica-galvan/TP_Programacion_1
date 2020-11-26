using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartManager : MonoBehaviour
{
    [Header("Heart Settings")]
    [SerializeField] private GameObject player = null; 
    [SerializeField] private LifeController lifeController = null;
    [SerializeField] private List<GameObject> hearts = new List<GameObject>();
    [SerializeField] private GameObject heart = null;
    private int currentHearts;

    void Start()
    {
        lifeController = player.GetComponent<LifeController>();
        lifeController.OnChangeCurrentLife.AddListener(OnCurrentLifeListener);
        lifeController.OnRespawnLife.AddListener(OnRespawnLifeListener);

        currentHearts = lifeController.GetMaxLife(); //Como es un solo nivel, siempre traemos el maximo de vida

        for (int i = 0; i < currentHearts; i++) //Y con esto inicializamos la vida
        {
            GameObject newHeart = Instantiate(heart);
            newHeart.transform.parent = gameObject.transform;
            hearts.Add(newHeart);
        }
    }

    private void OnCurrentLifeListener() //Con esto se actualiza la vida
    {
        currentHearts = lifeController.GetCurrentLife();
        Debug.Log("cantidad actual: " + currentHearts);
        if (currentHearts < hearts.Count) //Si la vida actual es menor a la cantidad de corazones en la lista
        {
            for (int i = 0; i < hearts.Count; i++)
            {
                if (i > (currentHearts - 1))
                {
                    hearts[i].SetActive(false);
                }
            }
        }
        else //Si hay igual o más...
        {
            for (int i = 0; i < currentHearts; i++)  //Reactivame los corazones
            {
                hearts[i].SetActive(true);
            }
            Debug.Log("sumo");
        }
    }

    private void OnRespawnLifeListener()
    {
        currentHearts = lifeController.GetCurrentLife();
        for (int i = 0; i < currentHearts; i++)
        {
            hearts[i].SetActive(true);
        }
    }
}
