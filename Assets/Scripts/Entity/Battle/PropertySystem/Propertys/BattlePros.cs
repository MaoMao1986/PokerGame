using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Battle属性组代码逻辑
/// 可以参与战斗的单位身上的属性组，主要是养成结果
/// </summary>
public partial class BattlePros : Propertys, ISaveDataBase
{
    public ISaveDataBase.DataChangedEventHandler DataChangedEvent { get; set; }

    public static BattlePros LoadConfig(string p_Id)
    {
        BattlePros t_Propertys = new();
        // 读取配置
        DRFightingproperty t_Row = ConfigManager.GetRow<DRFightingproperty>(p_Id);
        if (t_Row != null)
        {
            t_Propertys.Hp.Set(t_Row.Hp);
            t_Propertys.Mp.Set(t_Row.Mp);
            t_Propertys.AtkMin.Set(t_Row.Minatk);
            t_Propertys.AtkMax.Set(t_Row.Maxatk);
            t_Propertys.Def.Set(t_Row.Def);

            // 设置其他属性
            if (t_Row.Others.Length > 0)
            {
                for (int i = 0; i < t_Row.Others.GetLength(0); i++)
                {
                    Property t_Property = t_Propertys.GetProperty(t_Row.Others[i, 0].ToString());
                    t_Property.Set(t_Row.Others[i, 1]);
                }
            }
        }

        // 初始化其他补充属性
        t_Propertys.InitData();

        // 初始化事件
        t_Propertys.InitEvent();

        return t_Propertys;
    }

    public void InitData()
    {
        
    }

    public void InitEvent()
	{
		
		// 待实现，各个属性的事件回调
		//PhyRes.GetMaxFunction = () =>
		//{
		//	return PhyRes.GetConfigMax() + PhyResMax.GetValid();
		//};
		
	}
}
