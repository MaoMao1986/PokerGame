using System.Collections.Generic;

public abstract class BattleImpact
{
    //public int Id { get; set; }
    //public EmImpact_Type Type { get; set; }
    //protected EmSkill_TargetType m_TargetType { get; set; }
    //public int[] Params { get; set; }

    //public abstract void LoadData(int p_Id);
    //public abstract BattleReportRecord_Impact Run(BattleManager p_Manager,FightUnit p_Atker, BattleActiveSkill p_Skill, out bool p_OneTeamDead);

    //protected void m_LoadData(int p_Id)
    //{
    //    DRImpact t_Row = ConfigManager.GetRow<DRImpact>(p_Id);
    //    Id = p_Id;
    //    Type = (EmImpact_Type)t_Row.type;
    //    m_TargetType = (EmSkill_TargetType)t_Row.targetType;
    //    Params = t_Row.param;
    //}

    ///// <summary>
    ///// 获取impact的目标列表
    ///// </summary>
    ///// <param name="p_Manager"></param>
    ///// <param name="p_Atker"></param>
    ///// <param name="p_Skill"></param>
    ///// <returns></returns>
    //public List<int> GetTargetList(BattleManager p_Manager, FightUnit p_Atker, BattleActiveSkill p_Skill)
    //{
    //    EmSkill_TargetType t_Type = m_TargetType == EmSkill_TargetType.None ? p_Skill.TargetType : m_TargetType;
    //    return p_Manager.GetTargetList(p_Atker, t_Type);
    //}
}
