using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// LimitedProperty和CurrentProperty的父类，有最大值的逻辑，但对最大值的处理不一样，该类不直接出现
/// </summary>
public abstract class MaxProperty : Property
{
    public Func<int> GetMaxFunction { get; set; } = null;
    public int MaxValue {  get; set; }

    /// <summary>
    /// 获取配置中的最大值
    /// </summary>
    /// <returns></returns>
    public int GetConfigMax()
    {
        return ConfigManager.GetRow<DRProperty>(Id).max;
    }

    /// <summary>
    /// 获取最大属性数值，如果超过最大值则为最大值，如果未超过或者未配置最大值，则为当前值
    /// </summary>
    /// <returns></returns>
    public int GetMax()
    {
        int t_Value = 0;
        if(MaxValue == 0) { MaxValue = GetConfigMax(); }
        if(GetMaxFunction != null)
        {
            t_Value = GetMaxFunction();
            MaxValue = t_Value;
        }
        else
        {
            t_Value = MaxValue;
        }
        return t_Value;
    }
}
