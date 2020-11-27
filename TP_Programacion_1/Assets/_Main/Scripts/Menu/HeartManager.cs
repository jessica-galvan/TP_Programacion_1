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
    private int playerCurrentHearts;
    private int currentHearts;

    void Start()
    {
        lifeController = player.GetComponent<LifeController>();
        lifeController.OnChangeCurrentLife.AddListener(OnCurrentLifeListener);
        lifeController.OnRespawnLife.AddListener(OnRespawnLifeListener);
        playerCurrentHearts = lifeController.GetMaxLife(); //Como es un solo nivel, siempre traemos el maximo de vida

        for (int i = 0; i < playerCurrentHearts; i++) //Y con esto inicializamos la vida
        {
            GameObject newHeart = Instantiate(heart);
            newHeart.transform.parent = gameObject.transform;
            hearts.Add(newHeart);
            currentHearts++;
        }
    }

    private void OnCurrentLifeListener() //Con esto se actualiza la vida
    {
        playerCurrentHearts = lifeController.GetCurrentLife();
        if (playerCurrentHearts < currentHearts) //Si la vida actual es menor a la cantidad de corazones en la lista
        {
            for (int i = 0; i < currentHearts; i++)
            {
                if (playerCurrentHearts <= i)
                {
                    hearts[i].SetActive(false);
                    currentHearts--;
                }
            }
        }
        else
        {
            for (int i = 0; i < playerCurrentHearts; i++)  //Reactivame los corazones
            {
                if (!hearts[i].activeSelf)
                {
                    hearts[i].SetActive(true);
                    currentHearts++;
                }
            }
        }
    }

    private void OnRespawnLifeListener()
    {
        playerCurrentHearts = lifeController.GetCurrentLife();
        currentHearts = playerCurrentHearts;
        for (int i = 0; i < playerCurrentHearts; i++)
        {

            hearts[i].SetActive(true);
        }
    }
}
