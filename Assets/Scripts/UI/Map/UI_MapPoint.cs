using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_MapPoint : MonoBehaviour
{
    [SerializeField] private Button m_Picture;
    [SerializeField] private TextMeshProUGUI m_Name;
    [SerializeField] private Image m_Type;
    [SerializeField] private TextMeshProUGUI m_Lv;

    public void InitData(MapPoint p_MapPoint)
    {

    }

    public void InitData(string p_PointId, int p_Level = 1)
    {

    }
}
