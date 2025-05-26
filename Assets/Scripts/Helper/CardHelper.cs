using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CardHelper
{

    public static List<Card> GetCardLists(this List<UI_Card> p_List)
    {
        List<Card> t_List = new();
        foreach(var t_Card in p_List)
        {
            t_List.Add(t_Card.Card);
        }
        return t_List;
    }

    public static List<UI_Card> GetCardList(this List<Card> p_List)
    {
        List<UI_Card> t_List = new();
        foreach (var t_Card in p_List)
        {
            t_List.Add(t_Card.UICard);
        }
        return t_List;
    }
}