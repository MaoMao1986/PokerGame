using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UI_CardGroup : MonoBehaviour
{
    public GameObject Prefab_UICard;
    private List<GameObject> m_CardList;
    private bool m_CanSelect = false; // 是否可以选择

    public void InitData(List<Card> p_CardList, bool p_CanSelect = false)
    {
        m_CardList = new();
        m_CanSelect = p_CanSelect;
        AddCards(p_CardList);
    }

    /// <summary>
    /// 清除所有牌
    /// </summary>
    public void ClearData()
    {
        foreach (var t_Cell in m_CardList)
        {
            Destroy(t_Cell);
        }
        m_CardList.Clear();
    }

    public void AddCards(List<Card> p_CardList)
    {
        foreach (var t_Card in p_CardList)
        {
            GameObject t_Cell = Instantiate(Prefab_UICard, transform);
            UI_Card t_Script = t_Cell.GetComponent<UI_Card>();
            t_Script.InitData(t_Card, m_CanSelect);
            m_CardList.Add(t_Cell);
        }
    }

    public void AddCards(List<GameObject> p_CardList)
    {
        m_CardList.AddRange(p_CardList);
    }

    public void RemoveCards(List<GameObject> p_Cards = null)
    {
        List<GameObject> t_Cards = p_Cards ?? m_CardList;
        foreach (var t_Card in t_Cards)
        {
            m_CardList.Remove(t_Card);
        }
    }

    public void DestroyCards(List<GameObject> p_Cards = null)
    {
        List<GameObject> t_Cards = p_Cards ?? m_CardList;
        foreach (var t_Card in t_Cards)
        {
            m_CardList.Remove(t_Card);
            Destroy(t_Card);
        }
    }

    

    public void MoveCards(List<GameObject> p_Cards)
    {
        foreach (var t_Card in p_Cards)
        {
            m_CardList.Add(t_Card);
            t_Card.GetComponent<UI_Card>().IsSelected = false; // 移动后取消选中状态
            t_Card.transform.SetParent(transform);
        }
    }

    /// <summary>
    /// 获得卡牌数据列表
    /// </summary>
    /// <param name="p_Selected"></param>
    /// <returns></returns>
    public List<Card> GetCardDatas(bool p_Selected = false)
    {
        List<Card> t_CardDatas = new();
        foreach (var t_Card in m_CardList)
        {
            UI_Card t_Script = t_Card.GetComponent<UI_Card>();
            if (t_Script.IsSelected == p_Selected)
            {
                t_CardDatas.Add(t_Script.Card);
            }
        }
        return t_CardDatas;
    }

    /// <summary>
    /// 获取已选中的卡UI列表
    /// </summary>
    /// <returns></returns>
    public List<GameObject> GetUICardDatas(bool p_Selected = false)
    {
        List<GameObject> t_SelectedCards = new();
        foreach (var t_Card in m_CardList)
        {
            UI_Card t_Script = t_Card.GetComponent<UI_Card>();
            if (t_Script.IsSelected == p_Selected)
            {
                t_SelectedCards.Add(t_Card);
            }
        }
        return t_SelectedCards;
    }
}