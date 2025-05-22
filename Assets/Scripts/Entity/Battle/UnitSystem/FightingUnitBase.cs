using UnityEngine;

public abstract class FightingUnitBase
{
    /// <summary>
    /// ս�ڵĻ�������
    /// </summary>
    public FightingPropertys FightingProperty { get; set; } = new();
    /// <summary>
    /// ս�����Եĸ�������
    /// </summary>
    public BattlePropertys BattlePropertys { get; set; } = new();

    public abstract void LoadData();

    /// <summary>
    /// ��������
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
