using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevelopUnit_PlayerLevel : UnitBase, ISaveDataBase
{
    public PlayerPros PlayerPros { get; set; } = new PlayerPros();
    public ISaveDataBase.DataChangedEventHandler DataChangedEvent { get; set; }

    public override void CalculateBattlePropertys()
    {
        m_BattlePropertys = BattlePros.LoadConfig(PlayerPros.Lv.GetValidValue().ToString());
    }

    public void InitData()
    {
        FileName = "PlayerLevel";
        Name  = "Player Level"; // 可以根据需要设置玩家名称
        PlayerPros.Init();
        CalculateBattlePropertys();
    }

    public void InitEvent()
    {
        throw new System.NotImplementedException();
    }
}
