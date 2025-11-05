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

    public void SetValue(float value)
    {
        slider.value = value;
        // Set the color of the fill of the slider
        fill.color = gradient.Evaluate(slider.normalizedValue);

    }

    public void SexMaxValue (float value)
    {
        // Set the max value of the slider
        slider.maxValue = value;

        // Set the current value of the slider to the max value
        slider.value = value;

        // Set the color of the fill of the slider
        fill.color = gradient.Evaluate(1f);

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
