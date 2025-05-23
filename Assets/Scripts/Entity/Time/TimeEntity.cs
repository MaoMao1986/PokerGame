using System;

public class TimeEntity<T>
{
    public T Entity;
    public int LeftTime { get; private set; }

    public Action OnTimeChanged;
    public Action AfterTimeEnd;

    public TimeEntity(T p_Entity,int p_LeftTime)
    {
        Entity = p_Entity;
        LeftTime = p_LeftTime;
    }

    /// <summary>
    /// 更新buff剩余时间，默认1代表减去1，填-1代表加上1
    /// </summary>
    /// <param name="p_Time"></param>
    public void UpdateBuffTime(int p_Time = 1)
    {
        LeftTime -= p_Time;
        if(p_Time != 0) { 
            OnTimeChanged?.Invoke();
        }
        if(LeftTime <= 0)
        {
            AfterTimeEnd?.Invoke();
        }
    }
}