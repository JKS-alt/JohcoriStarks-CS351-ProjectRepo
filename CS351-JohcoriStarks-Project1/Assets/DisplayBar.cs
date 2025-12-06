using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public void SetValue(float value)
    {
        slider.value = value;

        if (fill != null)
        {
            // 0–1 value based on current / max
            fill.color = gradient.Evaluate(slider.normalizedValue);
        }
    }

    public void SetMaxValue(float value)
    {
        slider.maxValue = value;
        slider.value = value;

        if (fill != null)
        {
            // full health color
            fill.color = gradient.Evaluate(1f);
        }
    }
}

