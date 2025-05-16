using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UI_Card : MonoBehaviour
{
    public Button UICardButton;
    public Image UICard;
    public Image UICardLight;
    private bool m_CanSelect = false; // 是否可以选择
    private Card m_Card;
    public bool IsSelected { get; set; } = false;
    public Card Card { get { return m_Card; } }

    public void InitData(Card p_Card, bool p_CanSelect = false)
    {
        m_Card = p_Card;
        m_Card.UICard = this;
        m_CanSelect = p_CanSelect;
        if (m_CanSelect)
        {
            // 如果可以选择，添加点击事件
            ActiveOnClick(true);
        }
        else
        {
            ActiveOnClick(false);
        }
        UICard.sprite = Resources.Load<Sprite>(m_Card.Icon);
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
        if (IsSelected)
        {
            IsSelected = false;
            // 下移100像素
            CardUnSelect();
            // 
            //UICardButton.image.color = Color.white;
            //UICardButton.image.sprite = null; // 移除特效边框
        }
        else
        {
            IsSelected = true;
            //UICardButton.image.color = Color.yellow;
            CardSelect();
            //UICardButton.image.sprite = Resources.Load<Sprite>("UI/UI/waifaguang1"); // 添加特效边框
        }
    }

    /// <summary>
    /// 卡牌选中时的效果
    /// </summary>
    public void CardSelect()
    {
        UICardButton.transform.position += new Vector3(0, 50, 0);
    }

    /// <summary>
    /// 卡牌取消选中时的效果
    /// </summary>
    public void CardUnSelect()
    {
        UICardButton.transform.position -= new Vector3(0, 50, 0);
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
