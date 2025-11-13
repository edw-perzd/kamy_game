using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsLogic : MonoBehaviour
{
    public OptionsController optionsController;
    void Start()
    {
        optionsController = GameObject.FindGameObjectWithTag("Options").GetComponent<OptionsController>();
    }

    public void OpenOptions()
    {
        optionsController.optionsMenu.SetActive(true);
    }
}
