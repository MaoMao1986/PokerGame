using System.Collections.Generic;


/// <summary>
/// Fighting属性组代码逻辑
/// 在战斗中使用的属性组，主要和战斗属性不同的点在于会多一些当前属性，例如当前血量，当前蓝量等
/// </summary>
public partial class FightingPropertys : Propertys
{
	public override void InitAllProperty()
	{
		// 初始化属性字典
		InitPropertyList();
		
		// 待实现，各个属性的事件回调
		//PhyRes.GetMaxFunction = () =>
		//{
		//	return PhyRes.GetConfigMax() + PhyResMax.GetValid();
		//};
		
	}

	public void InitFromBattlePropertys(BattlePropertys p_BattlePropertys)
    {
        Copy(p_BattlePropertys);

        // 按照HP初始化当前HP
		CurrentHp.Set(p_BattlePropertys.Hp.GetValidValue());
		CurrentHp.GetMaxFunction = () =>
        {
            return Hp.GetValidValue();
        };

        // 按照MP初始化当前MP
		CurrentMp.Set(p_BattlePropertys.Mp.GetValidValue());
		CurrentMp.GetMaxFunction = () =>
        {
            return Mp.GetValidValue();
        };

        // 设置物理抗性最大值函数
        PhyRes.GetMaxFunction = () =>
        {
            return PhyRes.GetMax() + PhyResMax.GetValidValue();
        };

        // 设置金系抗性最大值函数
        GoldRes.GetMaxFunction = () =>
        {
            return GoldRes.GetMax() + GoldResMax.GetValidValue();
        };

        // 设置木系抗性最大值函数
        WoodRes.GetMaxFunction = () =>
        {
            return WoodRes.GetMax() + WoodResMax.GetValidValue();
        };

        // 设置水系抗性最大值函数
        WaterRes.GetMaxFunction = () =>
        {
            return WaterRes.GetMax() + WaterResMax.GetValidValue();
        };

        // 设置火系抗性最大值函数
        FireRes.GetMaxFunction = () =>
        {
            return FireRes.GetMax() + FireResMax.GetValidValue();
        };

        // 设置土系抗性最大值函数
        EarthRes.GetMaxFunction = () =>
        {
            return EarthRes.GetMax() + EarthResMax.GetValidValue();
        };
    }
}
