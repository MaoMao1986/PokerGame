using UnityEngine;

public class FightingUnit : UnitBase
{
    /// <summary>
    /// 战内的汇总数据
    /// </summary>
    public FightPros FightingPropertys { get; set; } = new();

    /// <summary>
    /// 是否已死亡，死亡状态下不能进行任何操作，且无法回血或者被加血，除非被复活
    /// </summary>
    public bool IsDead
    {
        get
        {
            return FightingPropertys.CurrentHp.Value <= 0;
        }
    }

    /// <summary>
    /// 外显
    /// </summary>
    public FightingUnitDisplay Display { get; set; } = new();
    /// <summary>
    /// buff管理器
    /// </summary>
    public TimeManager<BuffBase> BuffManager { get; set; } = new();

    /// <summary>
    /// 从配置中加载战斗单位
    /// </summary>
    /// <param name="p_Id"></param>
    /// <returns></returns>
    public static FightingUnit LoadConfig(string p_Id)
    {
        FightingUnit t_Unit = new();
        DRFightingunit t_Row = ConfigManager.GetRow<DRFightingunit>(p_Id);
        t_Unit.m_BattlePropertys = BattlePros.LoadConfig(t_Row.Property);
        t_Unit.FightingPropertys.LoadFromOtherPropertys(t_Unit.m_BattlePropertys);
        t_Unit.Display = FightingUnitDisplay.LoadConfig(t_Row.Display);
        return t_Unit;
    }

    public override void CalculateBattlePropertys()
    {
        throw new System.NotImplementedException();
    }


    /// <summary>
    /// 更新属性
    /// </summary>
    /// <param name="p_Id"></param>
    public void Update(string p_Id = "")
    {
        if (string.IsNullOrEmpty(p_Id))
        {
            FightingPropertys.SetOtherSum(m_BattlePropertys);
        }
        else
        {
            FightingPropertys.SetOtherSum(p_Id, m_BattlePropertys);
        }
    }

    
}
