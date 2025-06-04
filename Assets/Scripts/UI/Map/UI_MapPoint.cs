using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_MapPoint : MonoBehaviour, IUI_Struct
{
    [SerializeField] private Button m_Picture;
    [SerializeField] private TextMeshProUGUI m_PointName;
    [SerializeField] private Image m_Type;
    [SerializeField] private TextMeshProUGUI m_Lv;

    public void Destroy()
    {
        throw new System.NotImplementedException();
    }

    public void InitData(MapPoint p_MapPoint)
    {

    }

    public void InitData(string p_PointId, int p_Level = 1)
    {

    }

    public void InitData()
    {
        throw new System.NotImplementedException();
    }

    public void UpdateData()
    {
        throw new System.NotImplementedException();
    }
}
