using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class IntroAnimation : MonoBehaviour
{
    [SerializeField] private Button buttonSkip;
    [SerializeField] private GameObject button;
    [SerializeField] private float timer = 5f;
    [SerializeField] private VideoPlayer VideoPlayer;

    void Start()
    {
        buttonSkip.onClick.AddListener(OnSkipListener);
        button.SetActive(false);
        timer += Time.time;
        VideoPlayer.loopPointReached += LoadScene;
    }

    void Update()
    {
        if (Time.time > timer)
        {
            button.SetActive(true);

            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Space))
            {
                OnSkipListener();
            }
        }
    }

    public void OnSkipListener()
    {
        SceneManager.LoadScene("MainMenu");
    }

    void LoadScene(VideoPlayer vp)
    {
        SceneManager.LoadScene("MainMenu");
    }
}
