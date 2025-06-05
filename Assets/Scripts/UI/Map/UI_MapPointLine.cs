using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_MapPointLine : MonoBehaviour, IUI_Struct
{
    [SerializeField] private Slider m_Slider;
    private GameObject m_StartPoint;
    private GameObject m_EndPoint;

    void Update()
    {
        
    }

    public void UpdateLine(GameObject p_StartObject, GameObject p_EndObject, float p_Current, float p_Max)
    {

        float t_CurrentProgress = Mathf.Clamp01(p_Current / p_Max);

        Vector3 t_StartPos = Vector3.Lerp(p_StartObject.transform.position, p_EndObject.transform.position, 0.1f);
        Vector3 t_EndPos = Vector3.Lerp(p_StartObject.transform.position, p_EndObject.transform.position, 0.9f);

        // 计算中点位置
        Vector3 midpoint = (t_StartPos + t_EndPos) / 2f;
        m_Slider.transform.position = midpoint;

        // 计算方向（旋转角度）
        Vector3 direction = t_EndPos - t_StartPos;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        m_Slider.transform.rotation = Quaternion.Euler(0, 0, angle);

        // 计算长度（缩放X轴）
        float length = direction.magnitude/ m_Slider.GetComponent<RectTransform>().rect.width ;
        m_Slider.transform.localScale = new Vector3(length, 1, 1);

        m_Slider.GetComponent<UI_Slider>().UpdateData(p_Current, p_Max);
    }

    public void InitData()
    {
        throw new System.NotImplementedException();
    }

    public void UpdateData()
    {
        throw new System.NotImplementedException();
    }

    public void Destroy()
    {
        throw new System.NotImplementedException();
    }
}
