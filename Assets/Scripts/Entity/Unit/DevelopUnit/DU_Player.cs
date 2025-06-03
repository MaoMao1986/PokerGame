using Newtonsoft.Json;
using System.Collections;
using UnityEngine;

public class DU_Player : UnitBase, ISaveData
{
    public string IconId { get; set; } = "1";
    public DU_PlayerLevel PlayerLevel { get; set; } = new();
    public DU_PlayerInit PlayerInit { get; set; } = new();
    public DataChangedEventHandler DataChangedEvent { get; set; }
    public string FileName { get; set; } = "Player";

    public override void CalculateBattlePropertys()
    {
        m_BattlePropertys.SetValue0();
        AddAllBattlePropertys();
    }

    public void CreateNew()
    {
        Name = "Player Name"; // 可以根据需要设置玩家名称

        PlayerInit.CreateNew();
        DevelopUnitList.Add(PlayerInit.Name, PlayerInit);

        PlayerLevel.CreateNew();
        DevelopUnitList.Add(PlayerLevel.Name, PlayerLevel);

        // 最后计算所有属性之和
        CalculateBattlePropertys();

        InitEvent();
    }

    public void InitData()
    {
        // 如果挂上了子单位，需要在此处初始化子单位
        PlayerInit.InitData();       
        DevelopUnitList.Add(PlayerInit.Name, PlayerInit);

        PlayerLevel.InitData();    
        DevelopUnitList.Add(PlayerLevel.Name, PlayerLevel);
        
        // 最后计算所有属性之和
        CalculateBattlePropertys();

        InitEvent();
    }

    public void SaveToJson()
    {
        RuntimeData.Save(this, FileName);
    }

    public void LoadFromJson()
    {
        this.Load(FileName);
    }

    public void InitEvent()
    {
        
    }
}