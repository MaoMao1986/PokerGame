using System;
using TMPro.EditorUtilities;
using Unity.VisualScripting;
using UnityEngine;

public class Property
{
    public string Id { get; set; }
    /// <summary>
    /// �洢������ֵ����ֵΪ�����ֵ����������ʱ��ֵ������ʵ��ֵΪ90��������Ϊ75����ô�����ʱ���Ǳ���90��������ʱ��75
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
    /// ��������id��ʼ��������
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
    /// ��������id�ͳ�ʼֵ��ʼ��������
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
    /// ��ȡ��ʾ�ַ�����������ʾ������ʾ���ֻ��ǰٷֱ�
    /// </summary>
    /// <returns></returns>
    public static string GetDisplay(string p_Id, int p_Value)
    {
        string t_Return = "";
        int t_ValidValue = ConfigManager.GetRow<DRProperty>(p_Id).Validvalue;
        t_ValidValue = t_ValidValue <= 0 ? 1 : t_ValidValue; // �������Ϊ0����
        double t_Value = (double)p_Value / (double)t_ValidValue;
        string t_DisplayType = ConfigManager.GetRow<DRProperty>(p_Id).Displaytype;
        if (string.IsNullOrEmpty(t_DisplayType))
        {
            // ��ʾ����Ϊ�գ�Ĭ����ʾ
            t_Return = t_Value.ToString("G29");
        }
        else
        {
            // ����Ŀ���ʽ��ʾ
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
    /// ��������
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
    /// ��������
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
    /// ���Լ���
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
    /// ���Լ���
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
    /// ��������
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
            Debug.LogError($"����{Id}�����ֵС����Сֵ����������");
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
