using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrilloController : MonoBehaviour
{
    public Slider slider;
    public float sliderValue;
    public Image imagenBrillo;

    void Start()
    {
        slider.value = PlayerPrefs.GetFloat("brilloPantalla", 0.5f);
        imagenBrillo.color = new Color(imagenBrillo.color.r, imagenBrillo.color.g, imagenBrillo.color.b, slider.value);
    }

    public void ChangeSlider(float value)
    {
        sliderValue = value;
        PlayerPrefs.SetFloat("brilloPantalla", sliderValue);
        imagenBrillo.color = new Color(imagenBrillo.color.r, imagenBrillo.color.g, imagenBrillo.color.b, slider.value);
    }
}
