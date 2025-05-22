using UnityEngine;

public abstract class FightingUnitBase
{
    /// <summary>
    /// 战内的汇总数据
    /// </summary>
    public FightingPropertys FightingProperty { get; set; } = new();
    /// <summary>
    /// 战外属性的复制数据
    /// </summary>
    public BattlePropertys BattlePropertys { get; set; } = new();

    public abstract void LoadData();

    /// <summary>
    /// 更新属性
    /// </summary>
    /// <param name="p_Id"></param>
    public void Update(string p_Id = "")
    {
        if (string.IsNullOrEmpty(p_Id))
        {
            FightingProperty.SetOtherSum(BattlePropertys);
        }
        else
        {
            FightingProperty.SetOtherSum(p_Id, BattlePropertys);
        }
    }
}
