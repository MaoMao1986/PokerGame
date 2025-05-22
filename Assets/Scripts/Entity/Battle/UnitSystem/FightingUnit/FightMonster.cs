using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class FightMonster : FightingUnitBase
{
    public int MonsterId;

    public static FightMonster New(string p_Id)
    {
        DRBattleUnit t_Row = ConfigManager.GetRow<DRBattleUnit>(p_Id);
        FightMonster t_Unit = new()
        {
            MonsterId = p_Id,
            Position = p_Position,
            //按照配置读取属性
            BattlePropertys = BattlePropertys.New(t_Row.propertyId),
            ActiveSkillController = ActiveSkillController.New(p_Id)
        };
        t_Unit.FightPropertys = t_Unit.BattlePropertys.Copy<FightPropertys>();
        return t_Unit;
    }

    /// <summary>
    /// 从配置表中读取属性，技能等各种配置先放到BattlePropertys中，实际战斗使用的FightPropertys通过该对象计算而来
    /// </summary>
    public override void LoadData()
    {
        
    }

    
}
