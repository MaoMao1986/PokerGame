using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UI;

public class CurrentProperty : MaxProperty
{
    public static CurrentProperty New(int p_Id, Property p_Property)
    {
        CurrentProperty t_Property = new();
        t_Property.Id = p_Id;
        t_Property.GetMaxFunction = () =>
        {
            return p_Property.Get();
        };
        t_Property.Set(p_Property.Get());
        return t_Property;
    }

    /// <summary>
    /// 获取实际属性值，即跟最大值进行比较后的有效属性值
    /// </summary>
    /// <returns></returns>
    public override int Get()
    {
        int t_Max = GetMax();
        if (Value > t_Max)
        {
            Value = t_Max;
        }
        return Value;
    }

    public override string GetDisplay()
    {
        return GetDisplay(Id, Get());
    }

    /// <summary>
    /// 增加属性，不能超过上限值，返回实际增加的值
    /// </summary>
    /// <param name="p_Value"></param>
    /// <returns></returns>
    public override int Add(int p_Value)
    {
        int t_Value = p_Value;
        int t_Max = GetMax();
        if (Value + p_Value > t_Max)
        {
            t_Value = t_Max - Value;
        }
        return base.Add(t_Value);
    }

    /// <summary>
    /// 减少属性，不能超过上限值（减负值），返回实际减少的值
    /// </summary>
    /// <param name="p_Value"></param>
    /// <returns></returns>
    public override int Reduce(int p_Value, out bool p_OneTeamDead)
    {
        p_OneTeamDead = false;
        int t_Value = p_Value;
        int t_Max = GetMax();
        if (Value - p_Value > t_Max)
        {
            t_Value = Value - t_Max;
        }
        int t_R = base.Reduce(t_Value, out bool t_OneTeamDead);
        p_OneTeamDead = t_OneTeamDead;
        return t_R;
    }

    /// <summary>
    /// 减少属性，不能超过上限值（减负值），返回实际减少的值
    /// </summary>
    /// <param name="p_Value"></param>
    /// <returns></returns>
    public override int Reduce(int p_Value)
    {
        int t_Value = p_Value;
        int t_Max = GetMax();
        if (Value - p_Value > t_Max)
        {
            t_Value = Value - t_Max;
        }
        int t_R = base.Reduce(t_Value);
        return t_R;
    }

    /// <summary>
    /// 设置属性，不能超过最大值
    /// </summary>
    /// <param name="p_Value"></param>
    /// <returns></returns>
    public override int Set(int p_Value)
    {
        int t_Value = p_Value;
        int t_Max = GetMax();
        if (t_Value > t_Max)
        {
            t_Value = t_Max;
        }
        return base.Set(t_Value);
    }

    public new CurrentProperty Copy()
    {
        CurrentProperty t_Property = new()
        {
            Id = Id,
            Value = Value,
            MaxValue = MaxValue,
            GetMaxFunction = GetMaxFunction,
            OnValueChanged = OnValueChanged
        };
        return t_Property;
    }

    public CurrentProperty CopyValue()
    {
        CurrentProperty t_Property = new()
        {
            Id = Id,
            Value = Value,
            MaxValue = GetMax(),
            GetMaxFunction = null,
            OnValueChanged = OnValueChanged
        };
        return t_Property;
    }
}
