using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class SimpleFightPropertys
{
    public CurrentProperty CurrentHp { get; set; } = new();
    public CurrentProperty CurrentMp { get; set; } = new();

    /// <summary>
    /// 复制对象
    /// </summary>
    /// <returns></returns>
    public SimpleFightPropertys Copy()
    {
        SimpleFightPropertys t_Propertys = new()
        {
            CurrentHp = CurrentHp.Copy(),
            CurrentMp = CurrentMp.Copy()
        };
        return t_Propertys;
    }

    /// <summary>
    /// 复制值，主要是把最大值函数运算成值，主要用于战报中
    /// </summary>
    /// <returns></returns>
    public SimpleFightPropertys CopyValue()
    {
        SimpleFightPropertys t_Propertys = new()
        {
            CurrentHp = CurrentHp.CopyValue(),
            CurrentMp = CurrentMp.CopyValue()
        };
        return t_Propertys;
    }

    /// <summary>
    /// 两个属性集合的差值
    /// </summary>
    /// <param name="p_A"></param>
    /// <param name="p_B"></param>
    /// <returns></returns>
    public static Dictionary<int,int> operator -(SimpleFightPropertys p_A, SimpleFightPropertys p_B)
    {
        Dictionary<int, int> t_Dic = new();

        //当前生命差值
        {
            int t_Value = p_A.CurrentHp.Get() - p_B.CurrentHp.Get();
            if(t_Value != 0)
            {
                t_Dic.Add(p_A.CurrentHp.Id, t_Value);
            }
        }
        //当前法力差值
        {
            int t_Value = p_A.CurrentMp.Get() - p_B.CurrentMp.Get();
            if (t_Value != 0)
            {
                t_Dic.Add(p_A.CurrentMp.Id, t_Value);
            }
        }

        return t_Dic;
    }
}
