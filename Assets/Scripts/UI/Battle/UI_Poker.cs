using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Poker : MonoBehaviour, IUI_Struct
{
    [SerializeField] private Button m_UIPokerButton;
    [SerializeField] private Image m_UIPoker;
    [SerializeField] private Image m_UIPokerLight;
    private bool m_CanSelect = false; // 是否可以选择
    public Poker Poker { get; private set; }

    public bool IsSelected { get; private set; } = false;

    public void InitData(Poker p_Card, bool p_CanSelect = false)
    {
        Poker = p_Card;
        SetCanSelect(p_CanSelect);
        m_UIPoker.sprite = Resources.Load<Sprite>(Poker.Icon);
    }

    /// <summary>
    /// 设置是否可以选中（不可选中则无点击事件）
    /// </summary>
    /// <param name="p_CanSelect"></param>
    public void SetCanSelect(bool p_CanSelect = false)
    {
        if(m_CanSelect != p_CanSelect)
        {
            m_CanSelect = p_CanSelect;
            ActiveOnClick(m_CanSelect);
        }
    }

    /// <summary>
    /// 激活或禁用点击事件
    /// </summary>
    /// <param name="p_Active"></param>
    public void ActiveOnClick(bool p_Active = true)
    {
        if (p_Active)
        {
            m_UIPokerButton.onClick.AddListener(m_OnClick);
        }
        else
        {
            m_UIPokerButton.onClick.RemoveListener(m_OnClick);
        }
    }

    /// <summary>
    /// 点击事件
    /// </summary>
    private void m_OnClick()
    {
        IsSelected = !IsSelected;
        Select(IsSelected);
    }

    /// <summary>
    /// 卡牌选中时的效果
    /// </summary>
    public void Select(bool p_Select)
    {
        if (p_Select)
        {
            m_UIPokerButton.transform.position += new Vector3(0, 50, 0);
        }
        else
        {
            m_UIPokerButton.transform.position -= new Vector3(0, 50, 0);
        }
        
    }

    public void SetDraggable(bool p_CanDrag = true)
    {
        // 禁用拖拽（如果有DragHandler脚本）
        var dragHandler = GetComponent<Collider2D>();
        if (dragHandler != null)
        {
            dragHandler.enabled = p_CanDrag;
        }
    }

    /// <summary>
    /// 卡牌特效边框
    /// </summary>
    public void CardLight(bool p_Light = true)
    {
        m_UIPokerLight.transform.gameObject.SetActive(p_Light);
    }

    public void InitData()
    {
        throw new NotImplementedException();
    }

    public void UpdateData()
    {
        throw new NotImplementedException();
    }

    public void Destroy()
    {
        throw new NotImplementedException();
    }
}
