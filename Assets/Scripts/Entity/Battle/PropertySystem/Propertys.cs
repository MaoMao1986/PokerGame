using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public abstract class Propertys
{
    public int Id;
    public Dictionary<int, Property> PropertyList { get; set; } = new();
    public abstract void InitPropertyList();
    public abstract void InitAllProperty();

    public static T New<T>() where T : Propertys, new()
    {
        T t_Propertys = new();
        t_Propertys.InitAllProperty();
        return t_Propertys;
    }

    public Property GetProperty(int p_Id)
    {
        if (PropertyList.ContainsKey(p_Id))
        {
            return PropertyList[p_Id];
        }
        return null;
    }

    public void Calculate<T>(int p_Id, params T[] p_PropertyUnits) where T : Propertys
    {
        foreach(T t_Unit in p_PropertyUnits)
        {
            PropertyList[p_Id].Add(t_Unit.GetProperty(p_Id).Get());
        }
    }

    public void Calculate<T>(params T[] p_PropertyUnits) where T : Propertys
    {
        foreach(int t_Id in PropertyList.Keys)
        {
            Calculate(t_Id, p_PropertyUnits);
        }
    }
}
