using Newtonsoft.Json;
using System.Collections;
using UnityEngine;

public class DevelopUnit_Player : UnitBase, ISaveData
{
    public string IconId { get; set; } = "1";
    public DevelopUnit_PlayerLevel PlayerLevel { get; set; } = new();
    public DevelopUnit_PlayerInit PlayerInit { get; set; } = new();
    public ISaveDataBase.DataChangedEventHandler DataChangedEvent { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public override void CalculateBattlePropertys()
    {
        AddAllBattlePropertys();
    }

    public void InitData()
    {
        FileName = "Player";
        Name = "Player Name"; // 可以根据需要设置玩家名称

        // 如果挂上了子单位，需要在此处初始化子单位
        m_InitUnit(PlayerInit);
        m_InitUnit(PlayerLevel);
        
        // 最后计算所有属性之和
        CalculateBattlePropertys();
    }

    private void m_InitUnit<T>(T p_Unit) where T: UnitBase, ISaveDataBase,new()
    {
        p_Unit.InitData();
        DevelopUnitList.Add(p_Unit.FileName, p_Unit);
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
        throw new System.NotImplementedException();
    }
}