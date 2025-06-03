using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DU_PlayerLevel : UnitBase, ISaveDataBase
{
    public PlayerPros PlayerPros { get; set; } = new PlayerPros();
    public DataChangedEventHandler DataChangedEvent { get; set; }

    public override void CalculateBattlePropertys()
    {
        m_BattlePropertys.SetValue0();
        m_BattlePropertys.LoadConfig(PlayerPros.Lv.GetValidValue().ToString());
    }

    public void CreateNew()
    {
        Name = "Player Level"; // 可以根据需要设置玩家名称
        PlayerPros.CreateNew();
        CalculateBattlePropertys();

        InitEvent();
    }

    public void InitData()
    {
        PlayerPros.InitData();
        CalculateBattlePropertys();

        InitEvent();
    }

    public void InitEvent()
    {
        // 监听属性变化
        PlayerPros.Lv.OnValueChanged += (p_Id) =>
        {
            CalculateBattlePropertys();
        };
    }
}
