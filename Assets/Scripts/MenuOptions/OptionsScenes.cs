using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class OptionsScenes : MonoBehaviour
{
    private void Awake()
    {
        var noDestroyObjects = FindObjectsOfType<OptionsScenes>();
        if (noDestroyObjects.Length > 1)
        {
            Destroy(this.gameObject);
            return;
        }
        SceneManager.sceneLoaded += OnSceneLoaded;
        DontDestroyOnLoad(this.gameObject);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Menu" || scene.name == "CreditsScene")
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            Destroy(gameObject);
        }
    }

}
