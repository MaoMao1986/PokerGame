using System.Collections;
using UnityEngine;

public class DU_PlayerInit : UnitBase, ISaveDataBase
{
    public DataChangedEventHandler DataChangedEvent{ get; set; }

    public override void CalculateBattlePropertys()
    {
        
    }

    public void CreateNew()
    {
        Name = "PlayerInit";
        InitData();
    }

    public void InitData()
    {
        m_BattlePropertys.SetValueInit();
        InitEvent();
    }

    public void InitEvent()
    {
        
    }
}