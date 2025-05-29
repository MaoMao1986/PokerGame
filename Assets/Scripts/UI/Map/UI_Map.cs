using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_Map : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
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


    //public void OnDrag(PointerEventData eventData)
    //{
    //    transform.position = Input.mousePosition;
    //    transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
    //    UICellItem drag = eventData.pointerEnter.GetComponent<UICellItem>();
    //    //if (drag != null && drag.transform != transform)
    //    //{
    //    //    transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
    //    //}
    //}

    //public void OnEndDrag(PointerEventData eventData)
    //{
    //    UICellItem drag = eventData.pointerEnter.GetComponent<UICellItem>();
    //    if (drag != null && drag.transform != transform)
    //    {
    //        if (drag.ItemId != ItemId)
    //        {
    //            Transform parent = drag.transform.parent;
    //            Vector3 position = drag.transform.localPosition;
    //            drag.transform.parent = beginParent;
    //            drag.transform.localPosition = beginPosition;
    //            transform.parent = parent;
    //            transform.localPosition = position;
    //            transform.localScale = Vector3.one;
    //        }
    //        else
    //        {
    //            DRItem t_Row = CfgTableMgr.GetRow<DRItem>(ItemId);
    //            if (string.IsNullOrEmpty(t_Row.nextId.ToString()) || t_Row.nextId <= 0)
    //            {
    //                transform.parent = beginParent;
    //                transform.localPosition = beginPosition;
    //                transform.localScale = Vector3.one;
    //                Debug.Log("已合成到最大等级");
    //            }
    //            else
    //            {
    //                Destroy(gameObject);
    //                ItemState t_State = new ItemState();
    //                t_State.Id = t_Row.nextId;
    //                t_State.State = EmItem_State.Unlock;
    //                drag.Show(t_State);
    //            }
    //        }
    //    }
    //    else
    //    {
    //        transform.parent = beginParent;
    //        transform.localPosition = beginPosition;
    //        transform.localScale = Vector3.one;
    //    }
    //    ItemIcon.raycastTarget = true;
    //}
}
