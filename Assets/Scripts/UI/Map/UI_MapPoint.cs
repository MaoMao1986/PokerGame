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
        InitData(p_MapPoint.Id, p_MapPoint.Level);
    }

    public void InitData(string p_PointId, int p_Level = 1)
    {
        DRMappoint t_MapPointRow = ConfigManager.GetRow<DRMappoint>(p_PointId);
        Image t_Bg = m_Picture.GetComponent<Image>();
        t_Bg.sprite = Resources.Load<Sprite>("UI/Head/" + t_MapPointRow.Icon);
        m_Lv.text = $"Lv{p_Level}";
        m_PointName.text = t_MapPointRow.Name;
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
