using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ActiveSkillController
{
    //public int Id;
    //public BattleActiveSkill NormalSkill { get; set; } = new();

    //public List<BattleActiveSkill> ActiveSkills { get; set; } = new();

    //public static ActiveSkillController New(int p_Id)
    //{
    //    ActiveSkillController t_ActiveSkillController = new();
    //    t_ActiveSkillController.Id = p_Id;
    //    DRBattleUnit t_Row = ConfigManager.GetRow<DRBattleUnit>(p_Id);
    //    t_ActiveSkillController.NormalSkill = BattleActiveSkill.New(t_Row.normalSkill);

    //    if (t_Row.activeSkills.Length > 0)
    //    {
    //        foreach(int t_Id in t_Row.activeSkills)
    //        {
    //            t_ActiveSkillController.ActiveSkills.Add(BattleActiveSkill.New(t_Id));
    //        }
    //    }

    //    return t_ActiveSkillController;
    //}

    ///// <summary>
    ///// 获取当前出手的技能
    ///// </summary>
    ///// <returns></returns>
    //public BattleActiveSkill GetSkill(int p_Round, EmSkill_UseType p_UseType = EmSkill_UseType.CD)
    //{
    //    BattleActiveSkill t_Skill = NormalSkill;
    //    switch (p_UseType)
    //    {
    //        case EmSkill_UseType.CD:
    //            break;
    //        case EmSkill_UseType.Probility:
    //            break;
    //        case EmSkill_UseType.List:
    //            break;
    //    }
    //    return t_Skill;
    //}

    //public void UpdataCD()
    //{
    //    foreach(BattleActiveSkill t_Skill in ActiveSkills)
    //    {
    //        t_Skill.UpDataCD();
    //    }
    //}
}
