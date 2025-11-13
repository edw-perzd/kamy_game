using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        DontDestroyOnLoad(this.gameObject);
    }
}
