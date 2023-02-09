using UnityEngine;
using UnityEngine.UI;

public class UIBar : MonoBehaviour
{
    private Slider _slider;


    public void Init(float minValue, float maxValue, float curValue)
    {
        _slider = GetComponent<Slider>();
        _slider.minValue = minValue;
        _slider.maxValue = maxValue;
        _slider.value = curValue;
    }

    public void SetValue(float value)
    { 
        _slider.value = value;
    }

}
