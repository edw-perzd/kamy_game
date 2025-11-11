using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class VolumeController : MonoBehaviour
{
    public Slider volumeSlider;
    public float sliderValue;
    public Image imageMute;
    void Start()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("volumeAudio", 0.8f);
        AudioListener.volume = volumeSlider.value;
        IsMute();
    }

    public void ChangeVolume(float value)
    {
        sliderValue = value;
        PlayerPrefs.SetFloat("volumeAudio", sliderValue);
        AudioListener.volume = volumeSlider.value;
        IsMute();
    }
    public void IsMute()
    {
        if(sliderValue == 0)
        {
            imageMute.enabled = true;
        }
        else
        {
            imageMute.enabled = false;
        }
    }
}
