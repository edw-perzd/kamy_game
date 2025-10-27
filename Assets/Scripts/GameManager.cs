using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int TotalScore { get { return totalScore; } }
    private int totalScore = 0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.Log("Duplicate GameManager found, destroying the new one.");
        }
    }
    public void AddScore(int score)
    {
        totalScore += score;
    }
}
