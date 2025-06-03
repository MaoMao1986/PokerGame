using System.Collections.Generic;


/// <summary>
/// Fighting属性组代码逻辑
/// 在战斗中使用的属性组，主要和战斗属性不同的点在于会多一些当前属性，例如当前血量，当前蓝量等
/// </summary>
public partial class FightPros : Propertys, ISaveDataBase
{
    public ISaveDataBase.DataChangedEventHandler DataChangedEvent { get; set; }

    public void LoadFromOtherPropertys<T>(T p_Propertys) where T : Propertys
    {
        // 初始化属性数据
        Copy(p_Propertys);

        // 初始化当前属性数据
        InitData();

        // 初始化属性事件
        InitEvent();
    }

    public void InitData()
    {
        // 按照HP初始化当前HP
        CurrentHp.Set(Hp.GetValidValue());

        // 按照MP初始化当前MP
        CurrentMp.Set(Mp.GetValidValue());
    }

    public void InitEvent()
	{
        CurrentHp.GetMaxFunction = () =>
        {
            return Hp.GetValidValue();
        };
        
        CurrentMp.GetMaxFunction = () =>
        {
            return Mp.GetValidValue();
        };
    }
}
