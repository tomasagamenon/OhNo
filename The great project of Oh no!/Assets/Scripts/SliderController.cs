using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    public float value;
    [SerializeField] private float minValue;
    [SerializeField] private float maxValue;
    [SerializeField] private Settings settings;
    [SerializeField] private TypeSlider typeSlider;

    void Start()
    {
        var slider = GetComponent<Slider>();
        slider.minValue = minValue;
        slider.maxValue = maxValue;
        slider.value = value;
        switch(typeSlider)
        {
            case TypeSlider.sliderPopulation:
                settings.population = (int)value;
                break;
            case TypeSlider.sliderMouseSensitivity:
                settings.mouseSensitivity = (int)value;
                break;
            case TypeSlider.sliderFOV:
                settings.FOV = (int)value;
                break;
            case TypeSlider.sliderGeneralVolume:
                settings.generalVolume = (int)value;
                break;
            case TypeSlider.sliderMusicVolume:
                settings.musicVolume = (int)value;
                break;
            case TypeSlider.sliderSoundsVolume:
                settings.soundVolume = (int)value;
                break;
        }    
    }
    public enum TypeSlider : byte
    {
        sliderPopulation,
        sliderMouseSensitivity,
        sliderFOV,
        sliderGeneralVolume,
        sliderMusicVolume,
        sliderSoundsVolume,
    }
}
