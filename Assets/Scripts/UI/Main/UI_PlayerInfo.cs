using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_PlayerInfo : MonoBehaviour, IUI_Struct
{
    [SerializeField] private Slider m_ExpSlider;
    [SerializeField] private Image m_HeadIcon;
    [SerializeField] private TextMeshProUGUI m_PlayerName;
    [SerializeField] private TextMeshProUGUI m_PlayerLevel;

    public void Destroy()
    {
        throw new System.NotImplementedException();
    }

    public void InitData()
    {
        throw new System.NotImplementedException();
    }

    public void UpdateData()
    {
        DU_Player t_Player = RuntimeData.Player;
        m_PlayerLevel.text = "Lv : " + t_Player.PlayerLevel.PlayerPros.Lv.GetValidValue().ToString();
        m_PlayerName.text = t_Player.Name;
        UI_Slider t_Slider = m_ExpSlider.GetComponent<UI_Slider>();
        if(t_Slider != null)
        {
            t_Slider.UpdateData(t_Player.PlayerLevel.PlayerPros.CurrentExp);
            t_Player.PlayerLevel.PlayerPros.CurrentExp.OnValueChanged += UpdateData;
        }
        DRFightingdisplay t_Row = ConfigManager.GetRow<DRFightingdisplay>(t_Player.IconId);
        m_HeadIcon.sprite = Resources.Load<Sprite>("UI/Head/" + t_Row.Icon);
    }

    public void UpdateData(Property p_Pro)
    {
        m_PlayerLevel.text = "Lv : " + RuntimeData.Player.PlayerLevel.PlayerPros.Lv.GetValidValue().ToString();
        UI_Slider t_Slider = m_ExpSlider.GetComponent<UI_Slider>();
        if (t_Slider != null)
        {
            t_Slider.UpdateData(RuntimeData.Player.PlayerLevel.PlayerPros.CurrentExp);
        }
    }
}
