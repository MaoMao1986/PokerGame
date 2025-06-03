using System.Collections;
using UnityEngine;

public class DevelopUnit_PlayerInit : UnitBase, ISaveDataBase
{
    ISaveDataBase.DataChangedEventHandler ISaveDataBase.DataChangedEvent{ get; set; }

    public override void CalculateBattlePropertys()
    {
        
    }

    public void InitData()
    {
        FileName = "PlayerInit";
        m_BattlePropertys.SetValueInit();
    }

    void ISaveDataBase.InitData()
    {
        throw new System.NotImplementedException();
    }

    void ISaveDataBase.InitEvent()
    {
        throw new System.NotImplementedException();
    }
}