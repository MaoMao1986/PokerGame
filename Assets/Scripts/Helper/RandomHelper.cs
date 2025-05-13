using System;


public static class RandomHelper
{
    /// <summary>
    /// 返回一个指定范围内的随机数。
    /// </summary>
    /// <param name="p_MinValue">返回的随机数的下界（随机数可取该下界值）。</param>
    /// <param name="p_MaxValue">返回的随机数的上界（随机数不能取该上界值）。maxValue 必须大于等于 minValue。</param>
    /// <returns>一个大于等于 minValue 且小于 maxValue 的 32 位带符号整数，即：返回的值范围包括 minValue 但不包括 maxValue。如果 minValue 等于 maxValue，则返回 minValue。</returns>
    public static int GetRandom(this Random p_Random,int p_MinValue, int p_MaxValue)
    {
        int t_MinValue = p_MinValue;
        int t_MaxValue = p_MaxValue + 1;
        if (p_MaxValue < p_MinValue)
        {
            t_MinValue = p_MaxValue;
            t_MaxValue = p_MinValue + 1;
        }
        return p_Random.Next(t_MinValue, t_MaxValue);
    }
}

