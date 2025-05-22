using System;
using TMPro.EditorUtilities;
using Unity.VisualScripting;
using UnityEngine;

public class Property
{
    public string Id { get; set; }
    /// <summary>
    /// 存储的属性值，该值为保存的值，并非运算时的值，例如实际值为90，但上限为75，那么保存的时候还是保存90，但运算时用75
    /// </summary>
    public  int Value { get; private set; }
    public Func<int> OnValueChanged { get; set; } = null;
    public Func<int> OnValueAdded { get; set; } = null;
    public Func<int> OnValueReduced { get; set; } = null;
    public Func<int> OnValueSeted { get; set; } = null;
    public Func<int> GetMaxFunction { get; set; } = null;
    public Func<int> GetMinFunction { get; set; } = null;
    public Func<int> GetValidValueFunction { get; set; } = null;

    #region New
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
    /// 根据属性id和初始值初始化新属性
    /// </summary>
    /// <param name="p_Id"></param>
    /// <param name="p_Value"></param>
    /// <returns></returns>
    public static Property New(string p_Id, int p_Value)
    {
        Property t_Property = new();
        t_Property.Id = p_Id;
        t_Property.Value = p_Value;
        return t_Property;
    }
    #endregion

    #region Display
    /// <summary>
    /// 获取显示字符串，根据显示类型显示数字还是百分比
    /// </summary>
    /// <returns></returns>
    public static string GetDisplay(string p_Id, int p_Value)
    {
        string t_Return = "";
        int t_ValidValue = ConfigManager.GetRow<DRProperty>(p_Id).Validvalue;
        t_ValidValue = t_ValidValue <= 0 ? 1 : t_ValidValue; // 避免除数为0或负数
        double t_Value = (double)p_Value / (double)t_ValidValue;
        string t_DisplayType = ConfigManager.GetRow<DRProperty>(p_Id).Displaytype;
        if (string.IsNullOrEmpty(t_DisplayType))
        {
            // 显示类型为空，默认显示
            t_Return = t_Value.ToString("G29");
        }
        else
        {
            // 按照目标格式显示
            t_Return = t_Value.ToString(t_DisplayType);
        }
        return t_Return;
    }

    public string GetDisplay()
    {
        return GetDisplay(Id, Value);
    }
    #endregion

    #region Copy
    /// <summary>
    /// 复制属性
    /// </summary>
    /// <returns></returns>
    public Property Copy()
    {
        Property t_Property = new()
        {
            Id = Id,
            Value = Value,
            OnValueChanged = OnValueChanged,
            OnValueAdded = OnValueAdded,
            OnValueReduced = OnValueReduced,
            OnValueSeted = OnValueSeted,
            GetMaxFunction = GetMaxFunction,
            GetMinFunction = GetMinFunction,
            GetValidValueFunction =  GetValidValueFunction
        };
        return t_Property;
    }
    #endregion

    #region Max
    public int GetMax()
    {
        int t_Value = 0;
        if (GetMaxFunction != null)
        {
            t_Value = GetMaxFunction();
        }
        else
        {
            t_Value = ConfigManager.GetRow<DRProperty>(Id).Max;
        }
        return t_Value;
    }
    #endregion

    #region Min
    public int GetMin()
    {
        int t_Value = 0;
        if (GetMinFunction != null)
        {
            t_Value = GetMinFunction();
        }
        else
        {
            t_Value = ConfigManager.GetRow<DRProperty>(Id).Min;
        }
        return t_Value;
    }
    #endregion

    #region Add
    /// <summary>
    /// 属性增加
    /// </summary>
    /// <param name="p_Value"></param>
    public bool Add(int p_Value, out int p_AddedValue, out int p_Added, out int p_Changed)
    {
        bool t_Result = m_Add(p_Value, out p_AddedValue);
        p_Added = 0;
        p_Changed = 0;
        if (p_AddedValue != 0)
        {
            if (OnValueAdded != null) { p_Added = OnValueAdded.Invoke(); }
            if (OnValueChanged != null) { p_Changed = OnValueChanged.Invoke(); }
        }
        return t_Result;
    }

    public bool Add(int p_Value, out int p_AddedValue)
    {
        bool t_Result = m_Add(p_Value, out p_AddedValue);
        if (p_AddedValue != 0)
        {
            OnValueAdded?.Invoke();
            OnValueChanged?.Invoke();
        }
        return t_Result;
    }

    private bool m_Add(int p_Value, out int p_AddValue)
    {
        bool t_Result = false;
        p_AddValue = 0;
        int t_Max = GetMax();

        if (Value + p_AddValue <= t_Max)
        {
            Value += p_Value;
            p_AddValue = p_Value;
            t_Result = true;
        }
        else
        {
            switch (ConfigManager.GetRow<DRProperty>(Id).Maxmethod)
            {
                case Enum_PropertyLimitSet.Success:
                    Value += p_Value;
                    p_AddValue = p_Value;
                    t_Result = true;
                    break;
                case Enum_PropertyLimitSet.SetToLimit:
                    p_AddValue = t_Max - Value;
                    Value = t_Max;
                    t_Result = true;
                    break;
            }
        }
        return t_Result;
    }
    #endregion

    #region Reduce
    /// <summary>
    /// 属性减少
    /// </summary>
    /// <param name="p_Value"></param>
    public bool Reduce(int p_Value, out int p_ReduceValue, out int p_Reduced, out int p_Changed)
    {
        bool t_Result = m_Reduce(p_Value, out p_ReduceValue);
        p_Reduced = 0;
        p_Changed = 0;
        if (p_ReduceValue != 0)
        {
            if (OnValueReduced != null) { p_Reduced = OnValueReduced.Invoke(); }
            if (OnValueChanged != null) { p_Changed = OnValueChanged.Invoke(); }
        }
        return t_Result;
    }

    /// <summary>
    /// 属性减少
    /// </summary>
    /// <param name="p_Value"></param>
    public virtual int Reduce(int p_Value, out int p_ReduceValue)
    {
        bool t_Result = m_Reduce(p_Value, out p_ReduceValue);
        if (p_ReduceValue != 0)
        {
            OnValueReduced?.Invoke();
            OnValueChanged?.Invoke();
        }
        return p_Value;
    }

    private bool m_Reduce(int p_Value, out int p_ReduceValue)
    {
        bool t_Result = false;
        p_ReduceValue = 0;
        int t_Min = GetMin();

        if (Value - p_ReduceValue > t_Min)
        {
            Value -= p_Value;
            p_ReduceValue = p_Value;
            t_Result = true;
        }
        else
        {
            switch (ConfigManager.GetRow<DRProperty>(Id).Minmethod)
            {
                case Enum_PropertyLimitSet.Success:
                    Value -= p_Value;
                    p_ReduceValue = p_Value;
                    t_Result = true;
                    break;
                case Enum_PropertyLimitSet.SetToLimit:
                    p_ReduceValue = Value - t_Min;
                    Value = t_Min;
                    t_Result = true;
                    break;
            }
        }
        return t_Result;
    }
    #endregion

    #region Set
    public bool Set(int p_Value ,out int p_Seted,out int p_Changed)
    {
        int t_Value = Value;
        bool t_Result = m_Set(p_Value);
        p_Seted = 0;
        p_Changed = 0;
        if (Value != t_Value)
        {
            if (OnValueSeted != null) { p_Seted = OnValueSeted.Invoke(); }
            if (OnValueChanged != null) { p_Changed = OnValueChanged.Invoke(); }
        }
        return t_Result;
    }

    /// <summary>
    /// 设置属性
    /// </summary>
    /// <param name="p_Value"></param>
    public bool Set(int p_Value)
    {
        int t_Value = Value;
        bool t_Result = m_Set(p_Value);
        if (Value != t_Value)
        {
            OnValueSeted?.Invoke();
            OnValueChanged?.Invoke();
        }
        return t_Result;
    }

    private bool m_Set(int p_Value)
    {
        bool t_Result = false;
        int t_Max = GetMax();
        int t_Min = GetMin();
        if(t_Max < t_Min)
        {
            Debug.LogError($"属性{Id}的最大值小于最小值，请检查配置");
            return t_Result;
        }
        if(p_Value > t_Max)
        {
            switch (ConfigManager.GetRow<DRProperty>(Id).Maxmethod)
            {
                case Enum_PropertyLimitSet.Success:
                    Value = p_Value;
                    t_Result = true;
                    break;
                case Enum_PropertyLimitSet.SetToLimit:
                    Value = t_Max;
                    t_Result = true;
                    break;
            }
        }

        if(p_Value < t_Min)
        {
            switch (ConfigManager.GetRow<DRProperty>(Id).Minmethod)
            {
                case Enum_PropertyLimitSet.Success:
                    Value = p_Value;
                    t_Result = true;
                    break;
                case Enum_PropertyLimitSet.SetToLimit:
                    Value = t_Min;
                    t_Result = true;
                    break;
            }
        }
        return t_Result;
    }
    #endregion

    #region GetVaildValue
    public int GetValidValue()
    {
        int t_ValidValue = Value;
        if(GetValidValueFunction != null)
        {
            t_ValidValue = GetValidValueFunction.Invoke();
        }
        return t_ValidValue;
    }
    #endregion
}
