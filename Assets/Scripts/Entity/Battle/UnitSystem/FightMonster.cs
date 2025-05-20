using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class FightMonster : FightUnit
{
    public int MonsterId;

    public override string Name 
    {
        get
        {
            if(MonsterId <= 0) { return ""; }
            DRBattleUnit t_Row = ConfigManager.GetRow<DRBattleUnit>(MonsterId);
            DRBattleDisplay t_Display = ConfigManager.GetRow<DRBattleDisplay>(t_Row.displayId);
            return t_Display.name;
        }
    }

    public override string Icon
    {
        get
        {
            if (MonsterId <= 0) { return ""; }
            DRBattleUnit t_Row = ConfigManager.GetRow<DRBattleUnit>(MonsterId);
            DRBattleDisplay t_Display = ConfigManager.GetRow<DRBattleDisplay>(t_Row.displayId);
            return t_Display.icon;
        }
    }

    public static FightMonster New(int p_Id, int p_Position)
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
