using Newtonsoft.Json;
using System.Collections;
using UnityEngine;

public partial class DU_Player : UnitBase, ISaveData
{
    public string IconId { get; set; } = "1";
    public DataChangedEventHandler DataChangedEvent { get; set; }
    public string FileName { get; set; } = "Player";

    public override void CalculateBattlePropertys()
    {
        AddAllBattlePropertys();
    }    

    public void SaveToJson()
    {
        RuntimeData.Save(this, FileName);
    }

    public void LoadFromJson()
    {
        this.Load(FileName);
    }

    public void InitData()
    {
        // 可以根据需要设置玩家名称
        if (string.IsNullOrEmpty(Name)) { Name = "Player Name"; }
    }

    public void InitEvent()
    {
        
    }
}