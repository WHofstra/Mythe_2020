using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class PauseMenu : UIButtonScript
{
    public event Action Resume;

    protected PauseButton pauseButton;
    protected bool paused;

    protected void Start()
    {
        pauseButton = FindObjectOfType<PauseButton>();
        paused = false;
        transform.GetChild(0).gameObject.SetActive(paused);
        Time.timeScale = 1;

        if (pauseButton != null) {
            pauseButton = pauseButton.GetComponent<PauseButton>();
            pauseButton.PauseGame += Pause;
        }
    }

    public void ResumeGame()
    {
        Pause();
        Resume();
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    protected void Pause()
    {
        paused = !paused;
        transform.GetChild(0).gameObject.SetActive(paused);
    }
}
