using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDCollectable : MonoBehaviour
{
    [SerializeField] private GameManager gameManager = null;
    [SerializeField] private GameObject hudCollectable = null;
    [SerializeField] private Text textBox;
    private bool isShowing;
    private int cantidad = 0;
    
    void Start()
    {
        gameManager.OnChangeCollectable.AddListener(OnChangeCollectableListener);
        hudCollectable.SetActive(false);
    }

    void OnChangeCollectableListener()
    {
        cantidad++;
        textBox.text = "x" + cantidad.ToString();

        if (!isShowing)
        {
            isShowing = true;
            hudCollectable.SetActive(true);
        }
    }
}
