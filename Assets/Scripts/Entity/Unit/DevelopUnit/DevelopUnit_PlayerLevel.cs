using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevelopUnit_PlayerLevel : UnitBase
{
    PlayerOtherPropertys PlayerOtherPropertys { get; set; } = new PlayerOtherPropertys();

    public override void CalculateBattlePropertys()
    {
        m_BattlePropertys.LoadFromConfig(PlayerOtherPropertys.Lv.GetValidValue().ToString());
    }

    public override void Init()
    {
        Name = "PlayerLevel";
        PlayerOtherPropertys.Init();
        CalculateBattlePropertys();
    }
}
