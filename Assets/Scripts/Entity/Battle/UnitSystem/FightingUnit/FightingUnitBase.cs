using UnityEngine;

public abstract class FightingUnitBase
{
    /// <summary>
    /// ս�ڵĻ�������
    /// </summary>
    public FightingPropertys FightingPropertys { get; set; } = new();
    /// <summary>
    /// ս�����Եĸ�������
    /// </summary>
    public BattlePropertys BattlePropertys { get; set; } = new();

    /// <summary>
    /// �Ƿ�������������״̬�²��ܽ����κβ��������޷���Ѫ���߱���Ѫ�����Ǳ�����
    /// </summary>
    public bool IsDead
    {
        get
        {
            return FightingPropertys.CurrentHp.Value <= 0;
        }
    }

    /// <summary>
    /// ����
    /// </summary>
    public FightingUnitDisplay Display { get; set; } = new();
    /// <summary>
    /// buff������
    /// </summary>
    TimeManager<BuffBase> BuffManager { get; set; } = new();

    /// <summary>
    /// ��������
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
