using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip soundPause;
    public AudioClip soundUnPause;
    public GameObject pauseBtn;
    public GameObject pauseMenu;
    public GameObject optionsMenu;
    public GameObject youWinPanel;
    public bool isPaused = false;
    void Update()
    {
        if(!youWinPanel.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
            {
                if (isPaused)
                {
                    if (optionsMenu.activeSelf)
                    {
                        optionsMenu.SetActive(false);
                    }
                    ResumeGame();
                }
                else
                {
                    PauseGame();
                }
            }
        }
        else
        {
            pauseMenu.SetActive(false);
            pauseBtn.SetActive(false);
        }
    }
    public void ResumeGame()
    {
        PlayUnPause();
        pauseBtn.SetActive(true);
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }
    public void PauseGame()
    {
        PlayPause();
        pauseBtn.SetActive(false);
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void PlayPause()
    {
        audioSource.PlayOneShot(soundPause);
    }
    public void PlayUnPause()
    {
        audioSource.PlayOneShot(soundUnPause);
    }
}
