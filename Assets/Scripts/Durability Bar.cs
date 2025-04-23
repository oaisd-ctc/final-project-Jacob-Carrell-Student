using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DurabilityBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    public void SetDur(float durability)
    {
        slider.value = durability;

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    public void SetMaxDur(float durability)
    {
        slider.maxValue = durability;
        slider.value = durability;

        fill.color = gradient.Evaluate(1f);
    }
}
