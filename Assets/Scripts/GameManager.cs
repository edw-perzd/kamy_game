using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int TotalScore { get { return totalScore; } }
    private int totalScore = 0;

    public GameObject gameOverPanel;
    public TextMeshProUGUI gameoverScoreText;
    public Button restartButton;
    public Button mainMenuButton;
    private bool isGameOver = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.Log("Duplicate GameManager found, destroying the new one.");
            Destroy(gameObject);
            return;
        }
    }

    public void AddScore(int score)
    {
        totalScore += score;
    }

    public void ResetScore()
    {
        totalScore = 0;
    }

    void Start()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }

        if (restartButton != null)
        {
            restartButton.onClick.AddListener(RestartGame);
        }

        if (mainMenuButton != null)
        {
            mainMenuButton.onClick.AddListener(ReturnToMainMenu);
        }
    }

    void Update()
    {
        if (isGameOver)
        {
            if (Input.GetKeyDown(KeyCode.R))
                RestartGame();

            if (Input.GetKeyDown(KeyCode.M) || Input.GetKeyDown(KeyCode.Escape))
                ReturnToMainMenu();
        }
    }

    // ================================
    // ACTIVAR GAME OVER
    // ================================
    public void GameOver()
    {
        if (isGameOver) return;
        isGameOver = true;

        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        if (gameoverScoreText != null)
        {
            gameoverScoreText.text =
                "GAME OVER\nScore: " + totalScore.ToString() +
                "\nR - Reiniciar\nEsc - Menu Principal";
        }
    }

    // ================================
    // REINICIAR ESCENA
    // ================================
    public void RestartGame()
    {
        Time.timeScale = 1f;

        // 🔥 Asegurar que el panel se oculte
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        isGameOver = false;
        totalScore = 0;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // ================================
    // VOLVER AL MENÚ
    // ================================
    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;

        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        isGameOver = false;

        SceneManager.LoadScene("Menu");
    }
}
