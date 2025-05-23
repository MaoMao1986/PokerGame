using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class BattleImpact_1
{
    //public int MultParam;

    //public static BattleImpact_1 New(int p_Id)
    //{
    //    BattleImpact_1 t_Impact = new();
    //    t_Impact.LoadData(p_Id);
    //    return t_Impact;
    //}

    //public override void LoadData(int p_Id)
    //{
    //    m_LoadData(p_Id);
    //    MultParam = Params[0];
    //}

    //public override BattleReportRecord_Impact Run(BattleManager p_Manager, FightUnit p_Atker, BattleActiveSkill p_Skill, out bool p_OneTeamDead)
    //{
    //    p_OneTeamDead = false;
    //    List<int> t_TargetList = GetTargetList(p_Manager, p_Atker, p_Skill);
    //    if (t_TargetList.Count > 0)
    //    {
    //        BattleReportRecord_Impact t_ImpactRecord = new();
    //        foreach (int t_Index in t_TargetList)
    //        {
    //            t_ImpactRecord.AttackRecords = new();
    //            FightUnit t_Unit = p_Manager.GetFightUnit(t_Index);
    //            int t_Dmg = p_Manager.CountDmg(p_Atker, t_Unit, p_Skill.AttackType, out BattleReportRecord_Attack t_AttackRecord, MultParam);
    //            t_Dmg = t_Unit.FightPropertys.SimpleFightPropertys.CurrentHp.Reduce(t_Dmg, out bool t_OneTeamDead);
    //            p_OneTeamDead = t_OneTeamDead;
    //            t_AttackRecord.FightUnitPosition = t_Index;
    //            t_AttackRecord.HpChange.Add(t_Dmg);
    //            t_ImpactRecord.AttackRecords.Add(t_Index, t_AttackRecord);
    //            if (p_OneTeamDead)
    //            {
    //                break;
    //            }
    //        }
    //        return t_ImpactRecord;
    //    }
    //    return null;
    //}
}
