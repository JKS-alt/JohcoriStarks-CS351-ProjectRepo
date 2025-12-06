using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayBar : MonoBehaviour
{
    public Slider slider;

    // gradient for the fill color (green → red)
    public Gradient gradient;

    // the UI Image for the slider's fill
    public Image fill;

    // set the current value of the slider
    public void SetValue(float value)
    {
        slider.value = value;

        // update the color of the fill based on value (0–1)
       
            fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    // initialize the slider at full
    public void SetMaxValue(float value)
    {
        slider.maxValue = value;
        slider.value = value;

        // ensure full HP starts with the highest gradient color
       
            fill.color = gradient.Evaluate(1f);
    }
}