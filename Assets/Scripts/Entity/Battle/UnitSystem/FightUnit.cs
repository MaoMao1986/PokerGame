using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEditor.Sprites;

public abstract class FightUnit : BattleUnit
{
    public FightPropertys FightPropertys { get; set; } = new();
    public BuffController BuffManager { get; set; } = new();
    public ActiveSkillController ActiveSkillController { get; set; } = new();

    public int Position;

    public abstract string Name { get; }
    public abstract string Icon { get; }
    public abstract void LoadData();
    public bool IsDead 
    { 
        get 
        {
            return FightPropertys.SimpleFightPropertys.CurrentHp.Value <= 0;
        }
    }

    public bool IsSurvival
    {
        get
        {
            return !IsDead;
        }
    }

    /// <summary>
    /// 根据技能配置以及属性计算技能实际蓝耗
    /// </summary>
    /// <param name="p_Skill"></param>
    /// <returns></returns>
    public int GetSkillMpCost(BattleActiveSkill p_Skill)
    {
        int t_Cost = p_Skill.MpCost + FightPropertys.AddMpCost.Get() - FightPropertys.ReduceMpCost.Get();
        t_Cost = t_Cost <= 0 ? 0 : t_Cost;
        t_Cost = (int)((double)t_Cost * (1.0d + (double)(FightPropertys.AddMpCostPercent.Get() - FightPropertys.ReduceMpCostPercent.Get()) / (double)Numbers.PropertyMult));
        t_Cost = t_Cost <= 0 ? 0 : t_Cost;
        return t_Cost;
    }

    public BattleReportRecord_Skill UseSkill(BattleManager p_Manager, out bool p_OneTeamDead)
    {
        p_OneTeamDead = false;
        BattleActiveSkill t_Skill = ActiveSkillController.GetSkill(p_Manager.Round, p_Manager.UseType);
        
        //普攻不耗蓝，也无cd，只有技能会耗蓝以及进入cd
        if (t_Skill.Type == EmBattle_ActiveSkillType.Skill)
        {
            //判断技能实际蓝是否够是前面逻辑判断的，到此处一定能释放出来，所以直接减扣蓝
            int t_Cost = GetSkillMpCost(t_Skill);
            if (t_Cost <= FightPropertys.SimpleFightPropertys.CurrentMp.Get())
            {
                FightPropertys.SimpleFightPropertys.CurrentMp.Reduce(t_Cost);
            }
            else
            {
                FightPropertys.SimpleFightPropertys.CurrentMp.Set(0);
            }
            //技能进入cd
            if (p_Manager.UseType == EmSkill_UseType.CD)
            {
                t_Skill.JumpInCD();
            }
        }

        BattleReportRecord_Skill t_SkillRecord = new();
        t_SkillRecord.SkillID = t_Skill.Id;
        foreach (int t_Id in t_Skill.ImpactList)
        {
            DRImpact t_Row = ConfigManager.GetRow<DRImpact>(t_Id);
            switch ((EmImpact_Type)t_Row.type)
            {
                case EmImpact_Type.AtkSkill:
                    {
                        BattleImpact_1 t_Impact = BattleImpact_1.New(t_Id);
                        BattleReportRecord_Impact t_Record = t_Impact.Run(p_Manager, this, t_Skill, out bool t_OneTeamDead);
                        p_OneTeamDead = t_OneTeamDead;
                        if (t_Record != null)
                        {
                            t_SkillRecord.ImpactRecordList.Add(t_Record);
                        }
                    }
                    
                    break;
                default:
                    break;
            }
            if (p_OneTeamDead)
            {
                break;
            }
        }
        return t_SkillRecord;
    }

    public void ReviveHp()
    {
        int t_Point = FightPropertys.HpRestore.Get() - FightPropertys.ReduceHpRestore.Get();
        int t_Percent = (int)((double)(FightPropertys.HpRestorePercent.Get() - FightPropertys.ReduceHpRestorePercent.Get()) / (double)Numbers.PropertyMult * (double)FightPropertys.Hp.Get());
        int t_Value = Math.Max(0, t_Point + t_Percent);
        FightPropertys.SimpleFightPropertys.CurrentHp.Add(t_Value);
    }

    public void ReviveMp()
    {
        int t_Point = FightPropertys.MpRestore.Get() - FightPropertys.ReduceMpRestore.Get();
        int t_Percent = (int)((double)(FightPropertys.HpRestorePercent.Get() - FightPropertys.ReduceMpRestorePercent.Get()) / (double)Numbers.PropertyMult * (double)FightPropertys.Mp.Get());
        int t_Value = Math.Max(0, t_Point + t_Percent);
        FightPropertys.SimpleFightPropertys.CurrentMp.Add(t_Value);
    }

    /// <summary>
    /// 返回攻击
    /// </summary>
    /// <param name="p_UnitA"></param>
    /// <param name="p_UnitB"></param>
    /// <returns></returns>
    public int GetAtkerRandomAtk(Random p_Random)
    {
        //攻击方攻击力，攻击为最小攻击和最大攻击之间随机，技能调用的攻击都用此处的攻击
        return p_Random.GetRandom(FightPropertys.AtkMin.Get(), FightPropertys.AtkMax.Get());
    }

    /// <summary>
    /// 返回是否致命一击
    /// </summary>
    /// <param name="p_UnitA"></param>
    /// <param name="p_UnitB"></param>
    /// <returns></returns>
    public bool GetAtkerIsFatal(Random p_Random)
    {
        //攻击方致命一击判定，致命一击是否发生
        return p_Random.GetRandom(1, Numbers.PropertyMult) <= FightPropertys.FatalAtk.GetValid();
    }

    /// <summary>
    /// 判定防守方是否触发坚韧
    /// </summary>
    /// <param name="p_UnitA"></param>
    /// <param name="p_UnitB"></param>
    /// <returns></returns>
    public bool GetDeferIsDefMult(Random p_Random)
    {
        //防守方坚韧判定
        return p_Random.GetRandom(1, Numbers.PropertyMult) <= FightPropertys.DefMultPro.GetValid();
    }

    public string GetColorfulName()
    {
        if (Position > 0)
        {
            return Name.ColorFormat("#7EE8BB");
        }
        else
        {
            return Name.ColorFormat("orange");
        }
    }
}
