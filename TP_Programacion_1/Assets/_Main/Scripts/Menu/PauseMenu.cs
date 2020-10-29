using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private bool isActive;

    // Start is called before the first frame update
    void Start()
    {
        GoBack();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("ESCAPE");
            if (!isActive)
            {
                Pause();
            }
            else
            {
                GoBack();
            }
        }
    }

    private void Pause()
    {
        Time.timeScale = 0;
        isActive = true;
        pauseMenu.SetActive(true);
    }

    private void GoBack()
    {
        Time.timeScale = 1;
        isActive = false;
        pauseMenu.SetActive(false);
    }
}
