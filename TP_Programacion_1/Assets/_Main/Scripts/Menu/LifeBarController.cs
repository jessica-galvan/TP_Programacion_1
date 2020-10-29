using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeBarController : MonoBehaviour
{
    [SerializeField] LifeController lifeController = null;
    [SerializeField] Image lifeBar = null;

    void Start()
    {
        lifeController = GetComponent<LifeController>();
        lifeController.OnChangeCurrentLife.AddListener(OnCurrentLifeListener);
        UpdateLifeBar();
        if(lifeController.GetCurrentLifePercentage() == 0)
        {
            lifeBar.fillAmount = lifeController.GetMaxLife();
        }
    }

    private void UpdateLifeBar()
    {
        lifeBar.fillAmount = lifeController.GetCurrentLifePercentage();
    }

    private void OnCurrentLifeListener()
    {
        UpdateLifeBar();
    }
}