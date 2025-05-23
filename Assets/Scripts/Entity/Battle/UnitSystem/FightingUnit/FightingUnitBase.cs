using UnityEngine;

public abstract class FightingUnitBase
{
    /// <summary>
    /// 战内的汇总数据
    /// </summary>
    public FightingPropertys FightingPropertys { get; set; } = new();
    /// <summary>
    /// 战外属性的复制数据
    /// </summary>
    public BattlePropertys BattlePropertys { get; set; } = new();

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
    TimeManager<BuffBase> BuffManager { get; set; } = new();

    /// <summary>
    /// 更新属性
    /// </summary>
    /// <param name="p_Id"></param>
    public void Update(string p_Id = "")
    {
        if (string.IsNullOrEmpty(p_Id))
        {
            FightingPropertys.SetOtherSum(BattlePropertys);
        }
        else
        {
            FightingPropertys.SetOtherSum(p_Id, BattlePropertys);
        }
    }

    public void LoadFromConfig(string p_Id)
    {
        DRFightingunit t_Row = ConfigManager.GetRow<DRFightingunit>(p_Id);
        BattlePropertys.LoadFromConfig(t_Row.Property);
        FightingPropertys.LoadFromOtherPropertys(BattlePropertys);
        Display.LoadFromConfig(t_Row.Display);
    }
}
