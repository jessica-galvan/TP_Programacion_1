using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaBarController : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] private Image manaBar;
    void Start()
    {
        player.OnChangeMana.AddListener(OnManaChangesHandler);
        UpdateManaBar();
    }

    private void OnManaChangesHandler()
    {
        UpdateManaBar();
    }

    private void UpdateManaBar()
    {
        manaBar.fillAmount = player.GetManaPercentage();
    }
}
