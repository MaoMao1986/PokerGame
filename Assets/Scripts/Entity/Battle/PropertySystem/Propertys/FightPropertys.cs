using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class FightPropertys : BattlePropertys
{
    public SimpleFightPropertys SimpleFightPropertys { get; set; } = new();

    public override void InitAllProperty()
    {
        base.InitPropertyList();

        SimpleFightPropertys = new()
        {
            CurrentHp = CurrentProperty.New((int)EmProperty.CurrentHp, Hp),
            CurrentMp = CurrentProperty.New((int)EmProperty.CurrentMp, Mp)
        };
    }

    /// <summary>
    /// 更新属性，不更新当前属值
    /// </summary>
    /// <param name="p_BattlePropertys"></param>
    public void Update(BattlePropertys p_BattlePropertys)
    {
        foreach(int t_Id in PropertyList.Keys)
        {
            GetProperty(t_Id).Set(p_BattlePropertys.GetProperty(t_Id).Get());
        }
    }
}
