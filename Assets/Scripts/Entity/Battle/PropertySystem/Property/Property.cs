using System;
using UnityEngine;

public class Property
{
    public string Id { get; set; }
    /// <summary>
    /// 存储的属性值，该值为保存的值，并非运算时的值，例如实际值为90，但上限为75，那么保存的时候还是保存90，但运算时用75
    /// </summary>
    public  int Value { get; set; }

    public Func<bool> OnValueChanged;

    /// <summary>
    /// 根据属性id初始化新属性
    /// </summary>
    /// <param name="p_Id"></param>
    /// <returns></returns>
    public static Property New(string p_Id)
    {
        Property t_Property = new();
        t_Property.Id = p_Id;
        t_Property.Value = ConfigManager.GetRow<DRProperty>(p_Id).Initvalue;
        return t_Property;
    }

    /// <summary>
    /// 获取显示字符串，根据显示类型显示数字还是百分比
    /// </summary>
    /// <returns></returns>
    public static string GetDisplay(int p_Id, int p_Value)
    {
        string t_Return = "";
        EmProperty_DisplayType t_Type = (EmProperty_DisplayType)ConfigManager.GetRow<DRProperty>(p_Id).displayType;
        switch (t_Type)
        {
            case EmProperty_DisplayType.Percent:
                t_Return = (p_Value / ((double)Numbers.Percent)).ToString() + "%";
                break;
            case EmProperty_DisplayType.Number:
                t_Return = p_Value.ToString();
                break;
            default:
                Debug.LogError($"Porperty表中Id为{p_Id}的字段NumberType配置出错，超过了枚举定义");
                break;
        }
        return t_Return;
    }

    public static string GetDisplay(Property p_Property)
    {
        return GetDisplay(p_Property.Id, p_Property.Get());
    }

    public virtual string GetDisplay()
    {
        return GetDisplay(Id, Value);
    }


    /// <summary>
    /// 复制属性
    /// </summary>
    /// <returns></returns>
    public virtual Property Copy()
    {
        Property t_Property = new()
        {
            Id = Id,
            Value = Value,
            OnValueChanged = OnValueChanged
        };
        return t_Property;
    }

    public virtual int Get()
    {
        return Value;
    }

    /// <summary>
    /// 属性增加
    /// </summary>
    /// <param name="p_Value"></param>
    public virtual int Add(int p_Value)
    {
        Value += p_Value;
        if (p_Value != 0)
        {
            OnValueChanged?.Invoke();
        }
        return p_Value;
    }

    /// <summary>
    /// 属性减少
    /// </summary>
    /// <param name="p_Value"></param>
    public virtual int Reduce(int p_Value,  out bool p_OneTeamDead)
    {
        p_OneTeamDead = false;
        Value -= p_Value;
        if (p_Value != 0)
        {
            if(OnValueChanged != null)
            {
                p_OneTeamDead = OnValueChanged.Invoke();
            }
        }
        return p_Value;
    }

    /// <summary>
    /// 属性减少
    /// </summary>
    /// <param name="p_Value"></param>
    public virtual int Reduce(int p_Value)
    {
        Value -= p_Value;
        if (p_Value != 0)
        {
            OnValueChanged?.Invoke();
        }
        return p_Value;
    }

    /// <summary>
    /// 设置属性
    /// </summary>
    /// <param name="p_Value"></param>
    public virtual int Set(int p_Value)
    {
        Value = p_Value;
        if (Value != p_Value)
        {
            OnValueChanged?.Invoke();
        }
        return p_Value;
    }
}
