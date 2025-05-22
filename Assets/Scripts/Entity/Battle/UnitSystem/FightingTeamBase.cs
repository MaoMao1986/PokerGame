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
    /// 战队的位置和单位信息，A队位置从1、2、3等，B队位置从-1、-2、-3等
    /// </summary>
    public Dictionary<int,FightUnit> FightUnitList { get; set; } = new();

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
            foreach(FightUnit t_Unit in FightUnitList.Values)
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

    public bool CheckTeamDead()
    {
        return IsDead;
    }

    /// <summary>
    /// 根据玩家数据创建战队信息
    /// </summary>
    /// <returns></returns>
    public static FightingTeamBase NewPlayer(EmBattle_TeamType p_Type)
    {
        FightingTeamBase t_Team = new();
        int t_Index = 1;
        int t_Side = 1;
        if(p_Type == EmBattle_TeamType.TeamB)
        {
            t_Side = -1;
        }
        int t_Position = t_Index * t_Side;
        FightMonster t_Player = FightMonster.New(1, t_Position);
        t_Player.FightPropertys.SimpleFightPropertys.CurrentHp.OnValueChanged = t_Team.CheckTeamDead;

        t_Team.FightUnitList.Add(t_Position, t_Player);

        t_Index++;
        return t_Team;
    }

    /// <summary>
    /// 根据怪物配置读取战队信息
    /// </summary>
    /// <param name="p_Id"></param>
    /// <returns></returns>
    public static FightingTeamBase NewMonster(int p_Id, EmBattle_TeamType p_Type = EmBattle_TeamType.TeamB)
    {
        FightingTeamBase t_Team = new();
        int t_Index = 1;
        int t_Side = 1;
        if (p_Type == EmBattle_TeamType.TeamB)
        {
            t_Side = -1;
        }

        DRBattleLevel t_Row = ConfigManager.GetRow<DRBattleLevel>(p_Id);
        if (t_Row.unitList.Length > 0)
        {
            foreach (int t_Id in t_Row.unitList)
            {
                int t_Position = t_Index * t_Side;
                FightMonster t_Monster = FightMonster.New(t_Id, t_Position);
                t_Monster.FightPropertys.SimpleFightPropertys.CurrentHp.OnValueChanged = t_Team.CheckTeamDead;
                t_Team.FightUnitList.Add(t_Position, t_Monster);
                t_Index++;
            }
        }

        return t_Team;
    }

    /// <summary>
    /// 获取按位置的所有单位的当前属性的状态
    /// </summary>
    /// <returns></returns>
    public BattleSimpleTeam GetPropertysState()
    {
        BattleSimpleTeam t_Team = new();
        foreach(KeyValuePair<int, FightUnit> t_Pair in FightUnitList)
        {
            SimpleFightPropertys t_Propertys = t_Pair.Value.FightPropertys.SimpleFightPropertys.CopyValue();
            t_Team.Team.Add(t_Pair.Key, t_Propertys);
        }
        return t_Team;
    }

    public List<int> GetSurvivalUnits()
    {
        List<int> t_List = new();
        foreach(KeyValuePair<int, FightUnit> t_Pair in FightUnitList)
        {
            if (!t_Pair.Value.IsDead)
            {
                t_List.Add(t_Pair.Key);
            }
        }
        return t_List;
    }

    public void ReviveHp()
    {
        foreach(FightUnit t_Unit in FightUnitList.Values)
        {
            t_Unit.ReviveHp();
        }
    }

    public void ReviveMp()
    {
        foreach (FightUnit t_Unit in FightUnitList.Values)
        {
            t_Unit.ReviveMp();
        }
    }

    public void OnSkillCDUpdate()
    {
        foreach(FightUnit t_Unit in FightUnitList.Values)
        {
            t_Unit.ActiveSkillController.UpdataCD();
        }
    }

    public void OnBuffRun()
    {
        foreach (FightUnit t_Unit in FightUnitList.Values)
        {
            t_Unit.BuffManager.OnBuffRun();
        }
    }
}
