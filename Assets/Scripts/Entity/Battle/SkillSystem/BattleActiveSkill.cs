using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class BattleActiveSkill : BattleSkill
{
    public EmBattle_ActiveSkillType Type { get; private set; }
    public EmSkill_Type SkillType { get;private set; } = EmSkill_Type.Active;
    public EmBattle_AttackType AttackType { get; private set; }
    public int CD { get;private set; } = 0;
    public int MpCost { get;private set; } = 0;
    public int StartRoud { get;private set; } = 1;
    public int Order { get;private set; } = 0;
    public int Probility { get; private set; } = 10000;
    public EmSkill_TargetType TargetType { get; private set; } = EmSkill_TargetType.HeadOneEnemy;
    public List<int> ImpactList { get;private set; } = new();
    public int LeftRound { get;private set; } = 0;

    public static BattleActiveSkill New(int p_Id)
    {
        DRActiveSkill t_Row = ConfigManager.GetRow<DRActiveSkill>(p_Id);
        BattleActiveSkill t_Skill = new()
        {
            Id = p_Id,
            Type = (EmBattle_ActiveSkillType)t_Row.type,
            SkillType = EmSkill_Type.Active,
            AttackType = (EmBattle_AttackType)t_Row.dmgType,
            Name = t_Row.name,
            CD = t_Row.cd,
            MpCost = t_Row.mpCost,
            StartRoud = t_Row.startRound,
            Order = t_Row.order,
            Probility = t_Row.probility,
            TargetType = (EmSkill_TargetType)t_Row.targetType,
            ImpactList = t_Row.impactList.ToList()
        };

        return t_Skill;
    }

    /// <summary>
    /// 技能进入CD
    /// </summary>
    public void JumpInCD()
    {
        LeftRound = CD;
    }

    /// <summary>
    /// 技能是否在CD中
    /// </summary>
    /// <returns></returns>
    public bool IsInCD()
    {
        return LeftRound > 0;
    }

    /// <summary>
    /// 更新cd
    /// </summary>
    /// <param name="p_Round"></param>
    public void UpDataCD(int p_Round =1)
    {
        if (LeftRound > 0) { LeftRound -= p_Round; }
    }

    /// <summary>
    /// 清除CD
    /// </summary>
    public void ClearCD()
    {
        LeftRound = 0;
    }
}
