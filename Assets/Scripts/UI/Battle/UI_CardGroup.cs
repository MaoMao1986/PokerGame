using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UI_CardGroup : MonoBehaviour
{
    public GameObject Prefab_UICard;
    private List<UI_Card> m_CardList;
    public bool CanSelect = false; // 是否可以选择

    public List<UI_Card> InitData(List<Card> p_CardList, bool p_CanSelect = false)
    {
        DestroyCards();
        m_CardList = new();
        CanSelect = p_CanSelect;
        return AddCards(p_CardList);
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

    public List<UI_Card> AddCards(List<Card> p_CardList)
    {
        List<UI_Card> t_UICards = new();
        foreach (var t_Card in p_CardList)
        {
            GameObject t_Cell = Instantiate(Prefab_UICard, transform);
            UI_Card t_Script = t_Cell.GetComponent<UI_Card>();
            t_Script.InitData(t_Card, CanSelect);
            t_UICards.Add(t_Script);
            m_CardList.Add(t_Script);
        }
        return t_UICards;
    }

    // 移动牌组到出牌区域（协程版）
    public IEnumerator MoveToOtherGroup(List<UI_Card> p_UICards, UI_CardGroup p_OtherGroup)
    {
        List<Coroutine> moveCoroutines = new List<Coroutine>();
        // 1. 按顺序启动每张牌的移动协程（间隔delayBetweenCards）
        foreach (var t_Card in p_UICards)
        {
            // 计算牌在目标区域的水平位置（均匀分布）
            Vector3 targetPos = p_OtherGroup.transform.position;

            // 从当前组中移除牌
            if (m_CardList.Contains(t_Card))
            {
                m_CardList.Remove(t_Card);
            }

            // 启动单张牌移动协程
            Coroutine coroutine = StartCoroutine(MoveSingleCard(t_Card, targetPos, 300f));
            moveCoroutines.Add(coroutine);
            
            // 如果不是最后一张牌，等待间隔时间
            yield return new WaitForSeconds(0.5f);
        }

        foreach (var coroutine in moveCoroutines)
        {
            yield return coroutine;
        }
        p_OtherGroup.MoveCard(p_UICards);
    }

    public IEnumerator MoveSingleCard(UI_Card card, Vector3 targetPos, float speed)
    {
        card.SetDraggable(false); // 禁用交互

        Vector3 startPos = card.transform.position;
        float distance = Vector3.Distance(startPos, targetPos);
        float duration = distance / speed; // 根据速度计算需要的时间
        float elapsed = 0f;

        while (elapsed < duration)
        {
            card.transform.position = Vector3.Lerp(
                startPos,
                targetPos,
                elapsed / duration
            );
            elapsed += Time.deltaTime;
            yield return null;
        }

        card.transform.position = targetPos; // 确保最终位置精确
        card.SetDraggable(true);
    }

    public void AddCards(List<GameObject> p_CardList)
    {
        m_CardList.AddRange(p_CardList);
    }

    public void RemoveCards(List<UI_Card> p_Cards = null)
    {
        List<UI_Card> t_Cards = p_Cards ?? m_CardList;
        foreach (var t_Card in t_Cards)
        {
            m_CardList.Remove(t_Card);
        }
    }

    public void DestroyCards(List<UI_Card> p_Cards = null)
    {
        List<UI_Card> t_Cards = p_Cards ?? m_CardList;
        if(t_Cards == null)
        {
            return;
        }
        foreach (var t_Card in t_Cards.ToArray())
        {
            m_CardList.Remove(t_Card);
            Destroy(t_Card.gameObject);
        }
    }

    public void MoveCard(List<UI_Card> p_Cards)
    {
        foreach (var t_Card in p_Cards)
        {
            MoveCard(t_Card);
        }
    }

    public void MoveCard(UI_Card p_Card)
    {
        p_Card.CanSelect = CanSelect; // 保持选择状态一致
        m_CardList.Add(p_Card);
        p_Card.ChangeParent(this);
    }

    /// <summary>
    /// 获得卡牌数据列表
    /// </summary>
    /// <param name="p_Selected"></param>
    /// <returns></returns>
    public List<UI_Card> GetCardDatas(bool p_Selected = false)
    {
        List<UI_Card> t_CardDatas = new();
        foreach (var t_Card in m_CardList)
        {
            if (t_Card.IsSelected == p_Selected)
            {
                t_CardDatas.Add(t_Card);
            }
        }
        return t_CardDatas;
    }
}