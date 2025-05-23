using System;
using System.Collections.Generic;

public class TimeManager<T>
{
    private List<TimeEntity<T>> m_TimeEntityList = new List<TimeEntity<T>>();
    public Action Changed;
    /// <summary>
    /// 更新buff剩余时间，默认1代表减去1，填-1代表加上1
    /// </summary>
    /// <param name="p_Time"></param>
    public void UpdateTime(int p_Time = 1)
    {
        if(m_TimeEntityList.Count == 0) { return; }
        for (int i = m_TimeEntityList.Count - 1; i >= 0; i--)
        {
            TimeEntity<T> timeEntity = m_TimeEntityList[i];
            timeEntity.UpdateBuffTime(p_Time);
        }
        Changed?.Invoke();
    }
    
    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="p_Entity"></param>
    /// <param name="p_LeftTime"></param>
    public void AddTimeEntity(T p_Entity, int p_LeftTime)
    {
        TimeEntity<T> timeEntity = new(p_Entity, p_LeftTime);
        timeEntity.AfterTimeEnd = () =>
        {
            RemoveTimeEntity(timeEntity.Entity);
        };
        m_TimeEntityList.Add(timeEntity);
    }

    /// <summary>
    /// 移除
    /// </summary>
    /// <param name="p_Entity"></param>
    public void RemoveTimeEntity(T p_Entity)
    {
        for (int i = m_TimeEntityList.Count - 1; i >= 0; i--)
        {
            TimeEntity<T> timeEntity = m_TimeEntityList[i];
            if (timeEntity.Entity.Equals(p_Entity))
            {
                m_TimeEntityList.RemoveAt(i);
                Changed?.Invoke();
                break;
            }
        }
    }

    public void RemoveTimeEntity(TimeEntity<T> p_TimeEntity)
    {
        if (m_TimeEntityList.Contains(p_TimeEntity))
        {
            m_TimeEntityList.Remove(p_TimeEntity);
            Changed?.Invoke();
        }
    }
}