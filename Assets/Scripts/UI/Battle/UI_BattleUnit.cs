using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_BattleUnit : MonoBehaviour, IUI_Struct
{
    [SerializeField] private Image Head;
    [SerializeField] private TextMeshProUGUI Name;
    [SerializeField] private GameObject HpSlider;
    private UI_Slider m_HpScript;
    public FightingUnit FightingUnit;

    public void Destroy()
    {
        FightingUnit.FightingPropertys.CurrentHp.OnValueChanged -= m_HpScript.UpdateData;
    }

    public void InitData(FightingUnit p_FightingUnit)
    {
        FightingUnit = p_FightingUnit;
        InitData();
    }

    public void InitData()
    {
        Head.sprite = Resources.Load<Sprite>(FightingUnit.Display.Icon);
        Name.text = FightingUnit.Display.Name;
        m_HpScript = HpSlider.GetComponent<UI_Slider>();
        m_HpScript.UpdateData(FightingUnit.FightingPropertys.CurrentHp);

        FightingUnit.FightingPropertys.CurrentHp.OnValueChanged += m_HpScript.UpdateData;
    }

    public void UpdateData()
    {
        
    }
}
