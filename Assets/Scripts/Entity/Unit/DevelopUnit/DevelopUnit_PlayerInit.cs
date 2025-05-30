using System.Collections;
using UnityEngine;

public class DevelopUnit_PlayerInit : UnitBase
{
    public override void CalculateBattlePropertys()
    {
        
    }

    public override void Init()
    {
        Name = "PlayerInit";
        m_BattlePropertys.SetValueInit();
    }
}