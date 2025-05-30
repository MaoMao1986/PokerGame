using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_BattleUnit : MonoBehaviour, IUIStruct
{
    public Image Head;
    public TextMeshProUGUI Name;
    public GameObject HpSlider;
    public UISlider HpScript;
    FightingUnit FightingUnit;

    public void Destroy()
    {
        FightingUnit.FightingPropertys.CurrentHp.OnValueChanged -= HpScript.UpdateData;
    }

    public void InitData(FightingUnit p_FightingUnit)
    {
        FightingUnit = p_FightingUnit;
        Head.sprite = Resources.Load<Sprite>(FightingUnit.Display.Icon);
        Name.text = FightingUnit.Display.Name;
        HpScript = HpSlider.GetComponent<UISlider>();
        HpScript.UpdateData(FightingUnit.FightingPropertys.CurrentHp);

        FightingUnit.FightingPropertys.CurrentHp.OnValueChanged += HpScript.UpdateData;
    }
}
