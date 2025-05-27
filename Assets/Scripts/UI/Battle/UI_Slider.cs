using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISlider : MonoBehaviour
{
    public Slider Slider;
    public TextMeshProUGUI NumberText;
    public Image BackGround;
    public Image FontGround;

    /// <summary>
    /// 更新数据
    /// </summary>
    /// <param name="p_Pro"></param>
    public void UpdateData(Property p_Pro)
    {
        int t_Value = p_Pro.Value;
        if (t_Value < 0) { t_Value = 0; }
        Slider.maxValue = (float)p_Pro.GetMax();
        Slider.value = (float)t_Value;
        NumberText.text = $"{t_Value} / {p_Pro.GetMax()}";
    }
}
