using System.Collections;
using UnityEngine;

public partial class DU_PlayerInit : UnitBase, ISaveDataBase
{
    public DataChangedEventHandler DataChangedEvent{ get; set; }

    public override void CalculateBattlePropertys()
    {
        
    }

    public void InitData()
    {
        Name = "PlayerInit";
    }

    public void InitEvent()
    {
        
    }
}