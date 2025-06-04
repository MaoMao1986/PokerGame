using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class DU_PlayerLevel : UnitBase, ISaveDataBase
{
    public DataChangedEventHandler DataChangedEvent { get; set; }

    public override void CalculateBattlePropertys()
    {
        m_BattlePropertys.LoadConfig(PlayerPros.Lv.GetValidValue().ToString());
    }

    public void InitData()
    {
        Name = "PlayerLevel";
    }

    public void InitEvent()
    {
        

        // ¼àÌýÊôÐÔ±ä»¯
        PlayerPros.Lv.OnValueChanged += (p_Id) =>
        {
            CalculateBattlePropertys();
        };
    }
}
