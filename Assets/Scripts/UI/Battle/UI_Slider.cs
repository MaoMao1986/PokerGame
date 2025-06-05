using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Slider : MonoBehaviour, IUI_Struct
{
    [SerializeField] private Slider m_Slider;
    [SerializeField] private TextMeshProUGUI m_NumberText;
    [SerializeField] private Image m_BackGround;
    [SerializeField] private Image m_FontGround;

    public void Destroy()
    {
        throw new System.NotImplementedException();
    }

    public void InitData()
    {
        throw new System.NotImplementedException();
    }

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

    /// <summary>
    /// 更新数据
    /// </summary>
    /// <param name="p_Value"></param>
    /// <param name="p_MaxValue"></param>
    public void UpdateData(float p_Value, float p_MaxValue)
    {
        if (p_Value < 0) { p_Value = 0; }
        m_Slider.maxValue = p_MaxValue;
        m_Slider.value = p_Value;
        m_NumberText.text = $"{p_Value} / {p_MaxValue}";
    }

    public void UpdateData()
    {
        
    }
}
