using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class NextLevel : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip soundBright;
    [SerializeField] private GameObject UIScoreGame;
    [SerializeField] private GameObject UIYouWin;
    [SerializeField] private TMP_Text textNameLevel;
    [SerializeField] private GameObject ButtonNextLevel;
    [SerializeField] private TMP_Text puntajeTotalText;
    [SerializeField] private TMP_Text textButtonLevel;
    private string puntajeTotalLbl = "Puntaje total: ";
    private string levelLbl = "Nivel: ";
    private string[] levelsNames = {"Bosque", "Desierto", "Playa"};
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // if (collision.CompareTag("Player"))
        // {
        //     SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        // }
        if (collision.CompareTag("Player"))
        {
            UIYouWin.SetActive(true);
            LlenarUIYouWin();
        }
        else
        {
            UIYouWin.SetActive(false);
        }
    }

    public void LlenarUIYouWin()
    {
        PlayBright();
        Time.timeScale = 0f;
        UIScoreGame.SetActive(false);
        puntajeTotalText.text = puntajeTotalLbl + GameManager.Instance.TotalScore.ToString();
        int nivelActual = SceneManager.GetActiveScene().buildIndex;
        int nextIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextIndex < SceneManager.sceneCountInBuildSettings)
        {
            for(int i = 0; i < levelsNames.Length; i++)
            {
                if (nivelActual == i + 1)
                {
                    textNameLevel.text = levelLbl + levelsNames[i];
                    break;
                }
            }
            textButtonLevel.text = levelLbl + nextIndex;
            ButtonNextLevel.SetActive(true);
            if (nextIndex < SceneManager.sceneCountInBuildSettings)
            {
                textButtonLevel.text = "Ir a los creditos";
                ButtonNextLevel.SetActive(true);
            }
        }
        else
        {
            if (nextIndex < SceneManager.sceneCountInBuildSettings)
            {
                textButtonLevel.text = "Ir a los creditos";
                ButtonNextLevel.SetActive(true);
            }
        }
    }

    public void LoadNextLevel()
    {
        Debug.Log("Se ha pulsado nextLevel");
        Time.timeScale = 1f;
        int nextIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextIndex);
        }
        else
        {
            Debug.Log("No hay más escenas. Este es el último nivel.");
            Debug.Log("No deberías poder acceder a esta función.");
        }
    }

    public void PlayBright()
    {
        audioSource.PlayOneShot(soundBright);
    }
}
