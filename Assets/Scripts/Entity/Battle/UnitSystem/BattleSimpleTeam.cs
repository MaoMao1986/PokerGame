using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class BattleSimpleTeam
{
    public Dictionary<int, SimpleFightPropertys> Team { get; set; } = new();

    public static Dictionary<int, Dictionary<int,int>> operator -(BattleSimpleTeam p_TeamA, BattleSimpleTeam p_TeamB)
    {
        Dictionary<int, Dictionary<int, int>> t_Dic = new();
        for (int i = 0; i < p_TeamA.Team.Count; i++)
        {
            int t_Index = p_TeamA.Team.ElementAt(i).Key;
            Dictionary<int, int> t_PropertyChanged = p_TeamA.Team[t_Index] - p_TeamA.Team[t_Index];
            if (t_PropertyChanged.Count > 0)
            {
                t_Dic.Add(t_Index, t_PropertyChanged);
            }
        }
        return t_Dic;
    }
}
