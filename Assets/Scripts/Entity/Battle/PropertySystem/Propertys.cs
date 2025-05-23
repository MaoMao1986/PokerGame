using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public abstract class Propertys
{
    public string Id;
    public Dictionary<string, Property> PropertyList { get; set; } = new();
    
    public delegate void PropertyChangedEventHandler(string p_Id = "");
    public PropertyChangedEventHandler PropertyChangedEvent;

    /// <summary>
    /// 初始化属性列表
    /// </summary>
    public abstract void InitPropertyList();
    /// <summary>
    /// 初始化属性事件
    /// </summary>
    public abstract void InitPropertyEvent();
    /// <summary>
    /// 初始化属性数据
    /// </summary>
    public abstract void InitPropertyData();

    public static T New<T>() where T : Propertys, new()
    {
        T t_Propertys = new();
        t_Propertys.InitPropertyList();
        return t_Propertys;
    }

    public Property GetProperty(string p_Id)
    {
        if (PropertyList.ContainsKey(p_Id))
        {
            return PropertyList[p_Id];
        }
        return null;
    }

    #region 属性组对象计算

    #region Copy
    /// <summary>
    /// 将其他属性组的同属性的所有内容复制到自己身上（包括事件）（按照自己属性组的所有属性）
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="p_Id"></param>
    /// <param name="p_Propertys"></param>
    public void Copy<T>(string p_Id ,T p_Propertys) where T : Propertys
    {
        if (!PropertyList.ContainsKey(p_Id)) { return; }
        if (p_Propertys.PropertyList.ContainsKey(p_Id)) 
        {
            PropertyList[p_Id] = p_Propertys.GetProperty(p_Id).Copy();
        }
    }

    /// <summary>
    /// 将其他属性组的同属性的所有内容复制到自己身上（包括事件）（按照自己属性组的所有属性）
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="p_Propertys"></param>
    public void Copy<T>(T p_Propertys) where T : Propertys
    {
        foreach (string t_Id in PropertyList.Keys)
        {
            Copy(t_Id, p_Propertys);
        }
    }
    #endregion

    #region 累加，将其他对象的和设置到自己身上
    /// <summary>
    /// 累加，将其他对象的同id属性之和设置到自己身上
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="p_Id"></param>
    /// <param name="p_Propertys"></param>
    public void SetOtherSum<T>(string p_Id, params T[] p_Propertys) where T : Propertys
    {
        if (!PropertyList.ContainsKey(p_Id)){return;}
        int t_Value = 0;
        foreach (T t_Unit in p_Propertys)
        {
            if (!t_Unit.PropertyList.ContainsKey(p_Id)) { continue; }
            t_Value += t_Unit.GetProperty(p_Id).Value;
        }
        bool t_Result = PropertyList[p_Id].Set(t_Value);
        if (!t_Result)
        {
            Debug.LogError($"属性组{Id}的属性{p_Id}设置失败");
        }
    }

    /// <summary>
    /// 累加，将其他对象的同id属性之和设置到自己身上
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="p_Id"></param>
    /// <param name="p_Propertys"></param>
    public void SetOtherSum<T>(string p_Id, List<T> p_Propertys) where T : Propertys
    {
        T[] t_Propertys = p_Propertys.ToArray();
        SetOtherSum(p_Id, t_Propertys);
    }

    /// <summary>
    /// 累加，将其他对象的同id属性之和设置到自己身上
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="p_Propertys"></param>
    public void SetOtherSum<T>(params T[] p_Propertys) where T : Propertys
    {
        foreach (string t_Id in PropertyList.Keys)
        {
            SetOtherSum(t_Id, p_Propertys);
        }
    }

    /// <summary>
    /// 累加，将其他对象的同id属性之和设置到自己身上
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="p_Propertys"></param>
    public void SetOtherSum<T>(List<T> p_Propertys) where T : Propertys
    {
        T[] t_Propertys = p_Propertys.ToArray();
        SetOtherSum(t_Propertys);
    }
    #endregion

    #region 累加，将其他对象加到自己身上
    /// <summary>
    /// 根据属性id，将其他对象的属性加到自己身上
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="p_Id"></param>
    /// <param name="p_Propertys"></param>
    public void AddOthers<T>(string p_Id, params T[] p_Propertys) where T : Propertys
    {
        if (!PropertyList.ContainsKey(p_Id)) { return; }
        foreach (T t_Unit in p_Propertys)
        {
            if (!t_Unit.PropertyList.ContainsKey(p_Id)) { continue; }
            bool t_Result = PropertyList[p_Id].Add(t_Unit.GetProperty(p_Id).Value, out int t_AddValue);
            if (!t_Result)
            {
                Debug.LogError($"属性组{Id}的属性{p_Id}加和失败");
            }
        }
    }

    /// <summary>
    /// 根据属性id，将其他对象的属性加到自己身上
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="p_Id"></param>
    /// <param name="p_Propertys"></param>
    public void AddOthers<T>(string p_Id, List<T> p_Propertys) where T : Propertys
    {
        T[] t_Propertys = p_Propertys.ToArray();
        AddOthers(p_Id, t_Propertys);
    }

    /// <summary>
    /// 将其他属性组的所有属性加到自己身上（按照自己属性组的所有属性）
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="p_Propertys"></param>
    public void AddOthers<T>(params T[] p_Propertys) where T : Propertys
    {
        foreach (string t_Id in PropertyList.Keys)
        {
            AddOthers(t_Id, p_Propertys);
        }
    }

    /// <summary>
    /// 将其他属性组的所有属性加到自己身上（按照自己属性组的所有属性）
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="p_Propertys"></param>
    public void AddOthers<T>(List<T> p_Propertys) where T : Propertys
    {
        T[] t_Propertys = p_Propertys.ToArray();
        AddOthers(t_Propertys);
    }
    #endregion
    #endregion

}
