using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_MapPointLine : MonoBehaviour
{
    [SerializeField] private LineRenderer m_LineRenderer_Front;
    [SerializeField] private LineRenderer m_LineRenderer_Bg;

    void Update()
    {
        
    }

    public void UpdateLine(GameObject p_StartObject, GameObject p_EndObject, float p_Current, float p_Max)
    {
        if (p_StartObject == null || p_EndObject == null || m_LineRenderer_Bg == null || m_LineRenderer_Front == null) 
            return;

        float t_CurrentProgress = Mathf.Clamp01(p_Current / p_Max);

        Vector3 t_StartPos = Vector3.Lerp(p_StartObject.transform.position, p_EndObject.transform.position, 0.1f);
        Vector3 t_EndPos = Vector3.Lerp(p_StartObject.transform.position, p_EndObject.transform.position, 0.9f);

        m_LineRenderer_Bg.positionCount = 2;
        m_LineRenderer_Bg.SetPosition(0, t_StartPos);
        m_LineRenderer_Bg.SetPosition(1, t_EndPos);

        m_LineRenderer_Front.positionCount = 2;
        m_LineRenderer_Front.SetPosition(0, t_StartPos);

        // 计算中间点位置
        Vector3 midPoint = Vector3.Lerp(t_StartPos, t_EndPos, t_CurrentProgress);
        m_LineRenderer_Front.SetPosition(1, midPoint);
    }
}
