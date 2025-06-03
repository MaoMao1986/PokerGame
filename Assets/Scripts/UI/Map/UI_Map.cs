using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_Map : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IUI_Struct
{
    private RectTransform m_RectTransform;
    private Canvas m_Canvas;
    private Vector2 m_OriginalPosition;

    void Awake()
    {
        m_RectTransform = GetComponent<RectTransform>();
        m_Canvas = GetComponentInParent<Canvas>(); // 获取所在的 Canvas
        m_OriginalPosition = m_RectTransform.anchoredPosition;
    }

    // 开始拖拽时调用
    public void OnBeginDrag(PointerEventData eventData)
    {
        
    }

    // 拖拽过程中调用
    public void OnDrag(PointerEventData eventData)
    {
        // 将屏幕坐标转换为 UI 局部坐标
        Vector2 newPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            m_Canvas.transform as RectTransform,
            eventData.position,
            m_Canvas.worldCamera,
            out newPos
        );

        // 获取父对象的矩形范围
        RectTransform parentRect = transform.parent.GetComponent<RectTransform>();
        Vector2 minPosition = parentRect.rect.min - m_RectTransform.rect.min;
        Vector2 maxPosition = parentRect.rect.max - m_RectTransform.rect.max;

        // 限制位置
        newPos.x = Mathf.Clamp(newPos.x, minPosition.x, maxPosition.x);
        newPos.y = Mathf.Clamp(newPos.y, minPosition.y, maxPosition.y);

        m_RectTransform.anchoredPosition = newPos;
    }

    // 结束拖拽时调用
    public void OnEndDrag(PointerEventData eventData)
    {
        
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
