using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISlider : MonoBehaviour
{
    [SerializeField] private Slider m_Slider;
    [SerializeField] private TextMeshProUGUI m_NumberText;
    [SerializeField] private Image m_BackGround;
    [SerializeField] private Image m_FontGround;

    /// <summary>
    /// 更新数据
    /// </summary>
    /// <param name="p_Pro"></param>
    public void UpdateData(Property p_Pro)
    {
        int t_Value = p_Pro.Value;
        if (t_Value < 0) { t_Value = 0; }
        m_Slider.maxValue = (float)p_Pro.GetMax();
        m_Slider.value = (float)t_Value;
        m_NumberText.text = $"{t_Value} / {p_Pro.GetMax()}";
    }
}
