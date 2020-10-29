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
    //[SerializeField] private Sprite[] heartsImage = new Sprite[2];
    private int currentHearts;

    void Start()
    {
        lifeController = player.GetComponent<LifeController>();
        lifeController.OnChangeCurrentLife.AddListener(OnCurrentLifeListener);

        currentHearts = lifeController.GetCurrentLife();
        if (currentHearts == 0)
        {
            currentHearts = lifeController.GetMaxLife();
        }

        //Con esto inicializamos la vida.
        StartHeartBar();

    }

    private void StartHeartBar()
    {
        for (int i = 0; i < currentHearts; i++)
        {
            GameObject newHeart = Instantiate(heart);
            newHeart.transform.parent = gameObject.transform;
            hearts.Add(newHeart);
        }
    }

    private void UpdateHearts()
    {
        currentHearts = lifeController.GetCurrentLife();
        if(currentHearts < hearts.Count)
        { 
            for (int i = 0; i < hearts.Count; i++)
            {
                if(i > (currentHearts-1))
                {
                    hearts[i].SetActive(false);
                } 
            }
        } else
        {
            for (int i = 0; i < hearts.Count; i++)
            {
                if (i < currentHearts)
                {
                    hearts[i].SetActive(true);
                }
            }
        }
    }

    private void OnCurrentLifeListener()
    {
        UpdateHearts();
    }
}
