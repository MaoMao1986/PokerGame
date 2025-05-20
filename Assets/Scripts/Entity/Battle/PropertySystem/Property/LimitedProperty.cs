using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class LimitedProperty : MaxProperty
{
    public static new LimitedProperty New(int p_Id)
    {
        LimitedProperty t_Property = new();
        t_Property.Id = p_Id;
        t_Property.Value = ConfigManager.GetRow<DRProperty>(p_Id).initValue;
        return t_Property;
    }

    /// <summary>
    /// 获取实际属性值，即跟最大值进行比较后的有效属性值
    /// </summary>
    /// <returns></returns>
    public int GetValid()
    {
        int t_Max = GetMax();
        if (Value > t_Max)
        {
            return t_Max;
        }
        else
        {
            return Value;
        }
    }

    public string GetGetValidDisplay()
    {
        return GetDisplay(Id, GetValid());
    }

    public new LimitedProperty Copy()
    {
        LimitedProperty t_Property = new()
        {
            Id = Id,
            Value = Value,
            GetMaxFunction = GetMaxFunction,
            OnValueChanged = OnValueChanged
        };
        return t_Property;
    }
}
