using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    public GameObject selectorNivelUI;
    private int totalLevels = 3;
    void Start()
    {
        AsignLevelBtns();
    }

    void AsignLevelBtns()
    {
        for (int i = 0; i < totalLevels ; i++)
        {
            int levelIndex = i + 1;
            selectorNivelUI.transform.GetChild(i).GetComponent<Button>().onClick.AddListener(() =>
            {
                SceneManager.LoadScene(levelIndex);
            });
        }
    }
}
