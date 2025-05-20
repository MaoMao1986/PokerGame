using System;

public sealed class RandomController
{
    private static System.Random m_Random = new System.Random((int)DateTime.Now.Ticks);
    private static readonly RandomController instance = new RandomController();
    private RandomController() { }

    public static RandomController GetInstance()
    {
        return instance;
    }

    /// <summary>
    /// 返回一个指定范围内的随机数。
    /// </summary>
    /// <param name="p_MinValue">返回的随机数的下界（随机数可取该下界值）。</param>
    /// <param name="p_MaxValue">返回的随机数的上界（随机数不能取该上界值）。maxValue 必须大于等于 minValue。</param>
    /// <returns>一个大于等于 minValue 且小于 maxValue 的 32 位带符号整数，即：返回的值范围包括 minValue 但不包括 maxValue。如果 minValue 等于 maxValue，则返回 minValue。</returns>
    public static int GetRandom(int p_MinValue, int p_MaxValue)
    {
        return m_Random.GetRandom(p_MinValue, p_MaxValue);
    }
}
