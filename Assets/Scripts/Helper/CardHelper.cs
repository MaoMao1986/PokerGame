using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CardHelper
{

    public static List<Poker> GetCardLists(this List<UI_Poker> p_List)
    {
        List<Poker> t_List = new();
        foreach(var t_Card in p_List)
        {
            t_List.Add(t_Card.Poker);
        }
        return t_List;
    }

    public static Dictionary<Poker, UI_Poker> GetCardPair(this List<UI_Poker> p_List, params List<UI_Poker>[] p_OtherList)
    {
        Dictionary<Poker, UI_Poker> t_Pair = new();
        foreach (var t_Card in p_List)
        {
            if (!t_Pair.ContainsKey(t_Card.Poker))
            {
                t_Pair.Add(t_Card.Poker, t_Card);
            }
        }
        foreach (var t_OtherList in p_OtherList)
        {
            foreach (var t_Card in t_OtherList)
            {
                if (!t_Pair.ContainsKey(t_Card.Poker))
                {
                    t_Pair.Add(t_Card.Poker, t_Card);
                }
            }
        }
        return t_Pair;
    }
}