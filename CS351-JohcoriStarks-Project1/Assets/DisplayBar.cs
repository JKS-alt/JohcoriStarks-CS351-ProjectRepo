using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayBar : MonoBehaviour
{
    public Slider slider;

    // Gradient for the health bar
    public Gradient gradient;

    // Image for the fill of the health bar
    public Image fill;

    // Set current slider value
    public void SetValue(float value)
    {
        slider.value = value;

        // Set fill color based on health percentage
        if (fill != null)
            fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    // Set maximum slider value
    public void SetMaxValue(float value)
    {
        slider.maxValue = value;
        slider.value = value;
    }
}