using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UI_PokerGroup : MonoBehaviour
{
    [SerializeField] private GameObject m_Prefab_UIPoker;
    /// <summary>
    /// 扑克飞行速度（单位：像素/秒）
    /// </summary>
    [SerializeField] private float m_FlySpeed = 1000.0f;
    /// <summary>
    /// 多张扑克一起飞行时，飞行间隔时间（单位：秒）
    /// </summary>
    [SerializeField] private float m_DelayBetweenCards = 0.5f;
    private bool m_CanSelect { get;set; } = false; // 是否可以选择

    #region 初始化
    /// <summary>
    /// 初始化卡牌组数据
    /// </summary>
    /// <param name="p_CanSelect"></param>
    public void InitData(bool p_CanSelect = false)
    {
        DestroyPoker();
        m_CanSelect = p_CanSelect;
    }
    #endregion

    #region 卡牌操作
    /// <summary>
    /// 创建扑克
    /// </summary>
    /// <param name="p_Poker"></param>
    public UI_Poker CreatePoker(Poker p_Poker)
    {
        GameObject t_Cell = Instantiate(m_Prefab_UIPoker, transform);
        UI_Poker t_Script = t_Cell.GetComponent<UI_Poker>();
        t_Script.InitData(p_Poker, m_CanSelect);
        return t_Script;
    }

    /// <summary>
    /// 创建多张扑克
    /// </summary>
    /// <param name="p_Pokers"></param>
    /// <returns></returns>
    public List<UI_Poker> CreatePoker(List<Poker> p_Pokers)
    {
        if(p_Pokers == null) { return new List<UI_Poker>(); }        
        List<UI_Poker> t_List = new();
        foreach(var t_Poker in p_Pokers)
        {
            t_List.Add(CreatePoker(t_Poker));
        }
        return t_List;
    }

    /// <summary>
    /// 销毁扑克
    /// </summary>
    /// <param name="p_PokerList"></param>
    public void DestroyPoker(List<UI_Poker> p_PokerList = null)
    {
        if (p_PokerList == null)
        {
            for (int i = transform.childCount - 1; i >= 0; i--)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
        }
        else
        {
            foreach (var t_Poker in p_PokerList)
            {
                Destroy(t_Poker.gameObject);
            }
        }
    }

    /// <summary>
    /// 移动扑克列表，目标位置是同一个点
    /// </summary>
    /// <param name="p_UIPokers"></param>
    /// <param name="p_TargetPos"></param>
    /// <param name="p_IsFlying"></param>
    /// <returns></returns>
    public IEnumerator MovePokers(List<UI_Poker> p_UIPokers, Vector3 p_TargetPos, UI_PokerGroup p_Group = null, bool p_IsFlying = true)
    {
        if (p_UIPokers == null) { yield break; }
        List<Vector3> t_TargetPosList = new();
        for (int i = 0; i < p_UIPokers.Count; i++)
        {
            t_TargetPosList.Add(p_TargetPos);
        }
        yield return MovePokers(p_UIPokers, t_TargetPosList, p_Group, p_IsFlying);
    }

    /// <summary>
    /// 移动扑克列表，目标位置按照当前对象布局自动计算
    /// </summary>
    /// <param name="p_UIPokers"></param>
    /// <param name="p_IsFlying"></param>
    /// <returns></returns>
    public IEnumerator MovePokers(List<UI_Poker> p_UIPokers, UI_PokerGroup p_Group = null, bool p_IsFlying = true)
    {
        List<Vector3> t_TargetPosList = m_PreViewPosList(p_UIPokers.Count);
        yield return MovePokers(p_UIPokers, t_TargetPosList, p_Group, p_IsFlying);
    }

    /// <summary>
    /// 移动扑克列表到指定位置列表
    /// </summary>
    /// <param name="p_UIPokers"></param>
    /// <param name="p_TargetPosList"></param>
    /// <param name="p_IsFlying"></param>
    /// <returns></returns>
    public IEnumerator MovePokers(List<UI_Poker> p_UIPokers, List<Vector3> p_TargetPosList, UI_PokerGroup p_Group = null, bool p_IsFlying = true)
    {
        if(p_UIPokers == null) { yield break; }
        List<Coroutine> moveCoroutines = new List<Coroutine>();
        List<Vector3> t_TargetPosList = p_TargetPosList ?? m_PreViewPosList(p_UIPokers.Count);

        // 1. 按顺序启动每张牌的移动协程（间隔delayBetweenCards）
        for (int i=0;i<p_UIPokers.Count; i++)
        {
            if (p_UIPokers[i] == null) { continue; }
            // 计算牌在目标区域的水平位置
            Vector3 t_TargetPos = transform.position;
            if (t_TargetPosList != null)
            {
                if (t_TargetPosList.Count > i)
                {
                    if(t_TargetPosList[i] != Vector3.zero)
                    {
                       t_TargetPos = t_TargetPosList[i];
                    }
                }
            }

            // 启动单张牌移动协程
            Coroutine coroutine = StartCoroutine(m_MovePoker(p_UIPokers[i], t_TargetPos, p_IsFlying));
            moveCoroutines.Add(coroutine);

            // 如果不是最后一张牌，等待间隔时间
            yield return new WaitForSeconds(m_DelayBetweenCards);
        }

        foreach (var coroutine in moveCoroutines)
        {
            yield return coroutine;
        }
        m_MovePokerParent(p_UIPokers, p_Group);
    }

    /// <summary>
    /// 设置扑克的父对象
    /// </summary>
    /// <param name="p_UIPokers"></param>
    private void m_MovePokerParent(List<UI_Poker> p_UIPokers, UI_PokerGroup p_Group = null)
    {
        UI_PokerGroup t_Group = p_Group ?? this;
        // 所有牌移动完成后，先隐藏所有扑克
        foreach (var t_Poker in p_UIPokers)
        {
            if (t_Poker != null)
            {
                t_Poker.gameObject.SetActive(false);
            }
        }
        // 设置所有扑克的父物体为当前对象，隐藏之后再设置是避免设置父物体时触发布局更新导致看起来闪烁
        foreach (var t_Poker in p_UIPokers)
        {
            if (t_Poker != null)
            {
                t_Poker.transform.SetParent(t_Group.transform);
            }
        }
        // 设置完之后再显示所有扑克
        foreach (var t_Poker in p_UIPokers)
        {
            if (t_Poker != null)
            {
                t_Poker.gameObject.SetActive(true);
            }
        }
    }

    /// <summary>
    /// 将扑克移动到自己的牌组中
    /// 如果p_TargetPos
    /// </summary>
    /// <param name="p_Poker">要移动的扑克</param>
    /// <param name="p_TargetPos">飞行目标位置，如果p_TargetPos不为0,0,0，则飞行到p_TargetPos，否则飞行到自己牌组的位置</param>
    /// <param name="IsFlying">是否带飞行动画，true=带飞行动画，false=不带飞行动画，直接设置目标位置</param>
    /// <returns></returns>
    private IEnumerator m_MovePoker(UI_Poker p_Poker, Vector3 p_TargetPos, bool IsFlying = true)
    {
        p_Poker.SetCanSelect(m_CanSelect); // 保持选择状态一致
        Vector3 t_Pos = p_TargetPos == Vector3.zero ? transform.position : p_TargetPos;
        if (IsFlying)
        {
            yield return m_MoveSinglePoker(p_Poker, t_Pos);
        }
        else
        {
            p_Poker.transform.position = t_Pos; // 直接设置位置
            yield return null; // 等待一帧以确保更新
        }
    }

    /// <summary>
    /// 将单张扑克移动到目标位置（飞行表现）
    /// </summary>
    /// <param name="p_Poker"></param>
    /// <param name="p_TargetPos"></param>
    /// <returns></returns>
    private IEnumerator m_MoveSinglePoker(UI_Poker p_Poker, Vector3 p_TargetPos)
    {
        p_Poker.SetDraggable(false); // 禁用交互

        Vector3 startPos = p_Poker.transform.position;
        float distance = Vector3.Distance(startPos, p_TargetPos);
        float duration = distance / m_FlySpeed; // 根据速度计算需要的时间
        float elapsed = 0f;

        while (elapsed < duration)
        {
            p_Poker.transform.position = Vector3.Lerp(
                startPos,
                p_TargetPos,
                elapsed / duration
            );
            elapsed += Time.deltaTime;
            yield return null;
        }

        p_Poker.transform.position = p_TargetPos; // 确保最终位置精确
        p_Poker.SetDraggable(true);
    }

    #endregion


    #region 获取卡牌数据
    /// <summary>
    /// 获取所有选中/未选中的卡牌数据
    /// </summary>
    /// <param name="p_Selected"></param>
    /// <returns></returns>
    public List<UI_Poker> GetCardDatas(bool p_Selected = false)
    {
        List<UI_Poker> t_CardDatas = new();
        for (int i = 0; i < transform.childCount; i++)
        {
            UI_Poker t_Poker = transform.GetChild(i).GetComponent<UI_Poker>();
            if (t_Poker != null)
            {
                if (t_Poker.IsSelected == p_Selected)
                {
                    t_CardDatas.Add(t_Poker);
                }
            }
        }
        return t_CardDatas;
    }

    /// <summary>
    /// 获取所有卡牌数据
    /// </summary>
    /// <returns></returns>
    public List<UI_Poker> GetCardDatas()
    {
        List<UI_Poker> t_CardDatas = new();
        for (int i = 0; i < transform.childCount; i++)
        {
            UI_Poker t_Poker = transform.GetChild(i).GetComponent<UI_Poker>();
            if (t_Poker != null)
            {
                t_CardDatas.Add(t_Poker);
            }
        }
        return t_CardDatas;
    }
    #endregion

    #region 预测卡牌位置
    /// <summary>
    /// 预测未来增加的扑克的位置列表
    /// </summary>
    /// <param name="p_Num"></param>
    /// <returns></returns>
    private List<Vector3> m_PreViewPosList(int p_Num)
    {
        List<Vector3> t_PosList = new List<Vector3>();
        int t_ChildCount = transform.childCount;
        for (int i = 0; i < p_Num; i++)
        {
            t_PosList.Add(m_PreviewPosition(i + t_ChildCount, p_Num + t_ChildCount));
        }
        return t_PosList;
    }

    /// <summary>
    /// 计算指定索引的扑克位置
    /// </summary>
    /// <param name="p_Index"></param>
    /// <returns></returns>
    private Vector3 m_PreviewPosition(int p_Index, int p_ChildNum)
    {
        Vector3 t_Center = transform.position;
        HorizontalLayoutGroup t_LayoutGroup = GetComponent<HorizontalLayoutGroup>();
        if (t_LayoutGroup != null)
        {
            // 如果有水平布局组，计算基于布局的偏移
            float t_Spacing = t_LayoutGroup.spacing;
            float t_XOffset = t_Spacing * (p_Index - ((float)p_ChildNum-1)/2.0f);
            return new Vector3(t_Center.x + t_XOffset, t_Center.y, t_Center.z);
        }
        else
        {
            // 如果没有布局组，返回中心位置
            return t_Center;
        }
    }
    #endregion


}