using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class UI_Map : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IUI_Struct
{
    [SerializeField] private GameObject m_PointLayer;
    [SerializeField] private GameObject m_LineLayer;
    [SerializeField] private GameObject m_Prefab_MapPoint;
    [SerializeField] private GameObject m_Prefab_MapPointLine;
    private string m_MapId = "1";
    private Dictionary<string, UI_MapPoint> m_PointList = new();

    private RectTransform m_RectTransform;
    private Canvas m_Canvas;
    private Vector2 m_OriginalPosition;

    void Awake()
    {
        m_RectTransform = GetComponent<RectTransform>();
        m_Canvas = GetComponentInParent<Canvas>(); // 获取所在的 Canvas
        m_OriginalPosition = m_RectTransform.anchoredPosition;
    }

    private void Start()
    {
        DRMap t_MapRow = ConfigManager.GetRow<DRMap>(m_MapId);

        // 初始化完所有地图点
        foreach(var t_PointId in t_MapRow.Pointlist)
        {
            DRMappoint t_PointRow = ConfigManager.GetRow<DRMappoint>(t_PointId);
            Vector2 t_Position = new((float)t_PointRow.X, (float)t_PointRow.Y);
            Quaternion t_Rotation = m_PointLayer.transform.rotation;
            GameObject t_Point = Instantiate(m_Prefab_MapPoint, t_Position, t_Rotation, m_PointLayer.transform);
            t_Point.GetComponent<RectTransform>().localScale = Vector2.one * (float)t_PointRow.Scale;
            UI_MapPoint t_UIMapPoint = t_Point.GetComponent<UI_MapPoint>();
            m_PointList.Add(t_PointId, t_UIMapPoint);
            t_UIMapPoint.InitData(t_PointId);
        }

        // 创建地图点连线
        foreach (var t_PointId in t_MapRow.Pointlist)
        {
            DRMappoint t_PointRow = ConfigManager.GetRow<DRMappoint>(t_PointId);
            GameObject t_Point = m_PointList[t_PointId].gameObject;

            foreach(var t_PrefixPointId in t_PointRow.Prefixpoints)
            {
                DRPointprefix t_PrefixPointRow = ConfigManager.GetRow<DRPointprefix>(t_PrefixPointId);
                string t_PrefixId = t_PrefixPointRow.Prefixpoint;
                int t_Lv = t_PrefixPointRow.Lv;
                GameObject t_PrefixPoint = m_PointList[t_PrefixId].gameObject;
                GameObject t_Line = Instantiate(m_Prefab_MapPointLine, m_LineLayer.transform);
                UI_MapPointLine t_Script = t_Line.GetComponent<UI_MapPointLine>();
                if (t_Script != null)
                {
                    t_Script.UpdateLine(t_PrefixPoint, t_Point , 5 , t_Lv);
                }
                else
                {
                    Debug.LogError("UI_MapPointLine script not found on the prefab.");
                }
            }
        }
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
