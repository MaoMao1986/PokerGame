using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

public abstract class UnitBase
{
    /// <summary>
    /// 养成的战斗属性集合
    /// </summary>
    [JsonIgnore]
    protected BattlePros m_BattlePropertys { get; set; }
    public string Name { get; set; }
    [JsonIgnore]
    /// <summary>
    /// 子对象集合
    /// </summary>
    public Dictionary<string, UnitBase> DevelopUnitList = new();

    /// <summary>
    /// 属性计算
    /// </summary>
    public abstract void CalculateBattlePropertys();
    public abstract void Init();

    /// <summary>
    /// 计算战斗属性的函数（将子对象的所有属性全加起来）
    /// </summary>
    protected void AddAllBattlePropertys()
    {
        List<BattlePros> t_List = DevelopUnitList.Select(unit => unit.Value.m_BattlePropertys).ToList();
        m_BattlePropertys.SetOtherSum(t_List);
    }
}
