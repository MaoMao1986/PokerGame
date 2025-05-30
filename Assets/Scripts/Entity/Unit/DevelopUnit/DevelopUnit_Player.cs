using System.Collections;
using UnityEngine;

public class DevelopUnit_Player : UnitBase, ISaveData
{
    DevelopUnit_PlayerLevel PlayerLevel { get; set; } = new();
    DevelopUnit_PlayerInit PlayerInit { get; set; } = new();

    public override void CalculateBattlePropertys()
    {
        AddAllBattlePropertys();
    }

    public override void Init()
    {
        Name = "Player";

        // 如果挂上了子单位，需要在此处初始化子单位
        m_InitUnit(PlayerInit);
        m_InitUnit(PlayerLevel);
        
        // 最后计算所有属性之和
        CalculateBattlePropertys();
    }

    private void m_InitUnit(UnitBase p_Unit)
    {
        p_Unit.Init();
        DevelopUnitList.Add(p_Unit.Name, p_Unit);
    }

    public void Load()
    {
        
    }

    public void Save()
    {
        RuntimeData.Save(this, Name);
    }
}