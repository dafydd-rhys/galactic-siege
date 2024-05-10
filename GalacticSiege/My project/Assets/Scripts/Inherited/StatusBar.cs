using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public void SetMax(float max) {
        slider.maxValue = max;
        slider.value = max;

        fill.color = gradient.Evaluate(1f);
    }

    public void SetValue(float value) {
        slider.value = value;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

}
