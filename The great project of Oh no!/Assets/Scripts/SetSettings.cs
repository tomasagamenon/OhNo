using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetSettings : MonoBehaviour
{
    public Settings settings;
    public AudioMixer mixer;
    public GameObject sliderPopulation;
    public GameObject sliderMouseSensitivity;
    public GameObject sliderFOV;
    public GameObject sliderGeneralVolume;
    public GameObject sliderMusicVolume;
    public GameObject sliderSoundsVolume;

    public void SetPopulation()
    {
        settings.population = (int)sliderPopulation.GetComponent<Slider>().value;
        sliderPopulation.GetComponent<SliderController>().value = sliderPopulation.GetComponent<Slider>().value;
        FindObjectOfType<NpcManager>().ChangePopulation((int)sliderPopulation.GetComponent<Slider>().value);
    }
    public void SetMouseSensitivity()
    {
        settings.mouseSensitivity = sliderMouseSensitivity.GetComponent<Slider>().value;
        sliderMouseSensitivity.GetComponent<SliderController>().value = sliderMouseSensitivity.GetComponent<Slider>().value;
    }
    public void SetMouseFOV()
    {
        settings.FOV = sliderFOV.GetComponent<Slider>().value;
        sliderFOV.GetComponent<SliderController>().value = sliderFOV.GetComponent<Slider>().value;
    }
    public void SetVolumeGeneral()
    {
        settings.generalVolume = sliderGeneralVolume.GetComponent<Slider>().value;
        mixer.SetFloat("generalVolume", Mathf.Log10(sliderGeneralVolume.GetComponent<Slider>().value) * 20);
        sliderGeneralVolume.GetComponent<SliderController>().value = sliderGeneralVolume.GetComponent<Slider>().value;
    }
    public void SetVolumeBGM()
    {
        settings.musicVolume = sliderMusicVolume.GetComponent<Slider>().value;
        mixer.SetFloat("musicVolume", Mathf.Log10(sliderMusicVolume.GetComponent<Slider>().value) * 20);
        sliderMusicVolume.GetComponent<SliderController>().value = sliderMusicVolume.GetComponent<Slider>().value;
    }
    public void SetVolumeSFX()
    {
        settings.soundVolume = sliderSoundsVolume.GetComponent<Slider>().value;
        mixer.SetFloat("soundVolume", Mathf.Log10(sliderSoundsVolume.GetComponent<Slider>().value) * 20);
        sliderSoundsVolume.GetComponent<SliderController>().value = sliderSoundsVolume.GetComponent<Slider>().value;
    }
}
