using System;

public static class NumberHelper
{
    public static double Floor(this double p_Num,int p_Digit)
    {
        double t_multiplier = Math.Pow(10, p_Digit);
        return Math.Floor(p_Num * t_multiplier) / t_multiplier;
    }
}

