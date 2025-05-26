using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Card : MonoBehaviour
{
    public Button UICardButton;
    public Image UICard;
    public Image UICardLight;
    private bool m_CanSelect = false; // 是否可以选择
    public bool CanSelect
    {
        get
        {
            return m_CanSelect;
        }
        set
        {
            if (m_CanSelect != value)
            {
                m_CanSelect = value;
                ActiveOnClick(m_CanSelect);
            }
        }
    }
    private Card m_Card;
    public bool IsSelected { get; set; } = false;
    public Card Card { get { return m_Card; } }

    public void InitData(Card p_Card, bool p_CanSelect = false)
    {
        m_Card = p_Card;
        m_Card.UICard = this;
        m_CanSelect = p_CanSelect;
        UICard.sprite = Resources.Load<Sprite>(m_Card.Icon);
    }

    public void ChangeParent(UI_CardGroup p_Parent)
    {
        IsSelected = false; // 移动后取消选中状态
        m_CanSelect = p_Parent.CanSelect;
        transform.SetParent(p_Parent.transform);
    }

    /// <summary>
    /// 激活或禁用点击事件
    /// </summary>
    /// <param name="p_Active"></param>
    public void ActiveOnClick(bool p_Active = true)
    {
        if (p_Active)
        {
            UICardButton.onClick.AddListener(m_OnClick);
        }
        else
        {
            UICardButton.onClick.RemoveListener(m_OnClick);
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
            UICardButton.transform.position += new Vector3(0, 50, 0);
        }
        else
        {
            UICardButton.transform.position -= new Vector3(0, 50, 0);
        }
        
    }

    /// <summary>
    /// 卡牌取消选中时的效果
    /// </summary>
    public void CardUnSelect()
    {
        
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
    public void CardLight()
    {
        UICardLight.transform.gameObject.SetActive(true);
    }

    public void CardUnLight()
    {
        UICardLight.transform.gameObject.SetActive(false);
    }
}
