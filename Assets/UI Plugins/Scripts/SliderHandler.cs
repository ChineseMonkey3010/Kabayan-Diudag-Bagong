using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UIblabla
{
public class SliderHandler : MonoBehaviour
{
      public Slider mainSlider;

    void Start()
    {
        SetSliderValue(0.5f);
    }
    //Invoked when a submit button is clicked.
    public void SetSliderValue(float sliderValue)
    {
        //Displays the value of the slider in the console.
        mainSlider.value = sliderValue;
        Debug.Log(mainSlider.value);
    }
}
}
