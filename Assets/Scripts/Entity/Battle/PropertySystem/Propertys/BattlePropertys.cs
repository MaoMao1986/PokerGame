using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public partial class BattlePropertys : Propertys
{
    public static BattlePropertys New(int p_Id)
    {
        DRBattleProperty t_Row = ConfigManager.GetRow<DRBattleProperty>(p_Id);
        BattlePropertys t_Propertys = new();
        t_Propertys.Id = p_Id;
        t_Propertys.InitAllProperty();
        //按照怪物配置的数值覆盖初始化数据
        t_Propertys.AtkMin.Set(t_Row.atkMin);
        t_Propertys.AtkMax.Set(t_Row.atkMax);
        t_Propertys.Hp.Set(t_Row.hp);
        t_Propertys.Mp.Set(t_Row.mp);

        if (t_Row.otherPropertys.Length > 0)
        {
            for (int i = 0; i < t_Row.otherPropertys.Length; i++)
            {
                t_Propertys.GetProperty(t_Row.otherPropertys[i, 0]).Set(t_Row.otherPropertys[i, 1]);
            }
        }
        
        return t_Propertys;
    }

    public override void InitAllProperty()
    {
        InitPropertyList();

        PhyRes.GetMaxFunction = () =>
        {
            return PhyRes.GetConfigMax() + PhyResMax.GetValid();
        };

        GoldRes.GetMaxFunction = () =>
        {
            return GoldRes.GetConfigMax() + GoldResMax.GetValid();
        };

        WoodRes.GetMaxFunction = () =>
        {
            return WoodRes.GetConfigMax() + WoodResMax.GetValid();
        };

        WaterRes.GetMaxFunction = () =>
        {
            return WaterRes.GetConfigMax() + WaterResMax.GetValid();
        };

        FireRes.GetMaxFunction = () =>
        {
            return FireRes.GetConfigMax() + FireResMax.GetValid();
        };

        EarthRes.GetMaxFunction = () =>
        {
            return EarthRes.GetConfigMax() + EarthResMax.GetValid();
        };
    }

    public T Copy<T>() where T : BattlePropertys, new()
    {
        T t_Object = new();
        t_Object.InitPropertyList();
        foreach(int t_Id in PropertyList.Keys)
        {
            t_Object.GetProperty(t_Id).Set(GetProperty(t_Id).Get());
        }
        t_Object.InitAllProperty();
        return t_Object;
    }
}

