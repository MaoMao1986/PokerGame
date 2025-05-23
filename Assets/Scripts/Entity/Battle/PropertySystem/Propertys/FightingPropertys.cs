using System.Collections.Generic;


/// <summary>
/// Fighting属性组代码逻辑
/// 在战斗中使用的属性组，主要和战斗属性不同的点在于会多一些当前属性，例如当前血量，当前蓝量等
/// </summary>
public partial class FightingPropertys : Propertys, IPropertysOthers
{
    public override void InitPropertyData()
    {
        // 按照HP初始化当前HP
        CurrentHp.Set(Hp.GetValidValue());

        // 按照MP初始化当前MP
        CurrentMp.Set(Mp.GetValidValue());
    }

    public override void InitPropertyEvent()
	{
        CurrentHp.GetMaxFunction = () =>
        {
            return Hp.GetValidValue();
        };
        
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

	public void LoadFromOtherPropertys<T>(T p_Propertys) where T : Propertys
    {
        // 初始化属性列表
        InitPropertyList();

        // 初始化属性数据
        Copy(p_Propertys);

        // 初始化当前属性数据
        InitPropertyData();

        // 初始化属性事件
        InitPropertyEvent();
    }
}
