using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine.UIElements;

public class FightingTeamBase
{
    /// <summary>
    /// 队伍索引，用以区分队伍，默认队伍之间均为敌对关系
    /// </summary>
    public int TeamId { get; set; } = -1;
    /// <summary>
    /// 战队的位置和单位信息，A队位置从1、2、3等，B队位置从-1、-2、-3等
    /// </summary>
    public Dictionary<int,FightingUnitBase> FightUnitList { get; set; } = new();

    public int Count
    {
        get
        {
            if(FightUnitList == null) { return 0; }
            else { return FightUnitList.Count; }
        }
    }

    public bool IsDead
    {
        get
        {
            bool t_isDead = true;
            if(FightUnitList == null) { return t_isDead; }
            foreach(FightingUnitBase t_Unit in FightUnitList.Values)
            {
                if(!t_Unit.IsDead)
                {
                    t_isDead = false;
                    break;
                }
            }
            return t_isDead;
        }
    }
}
