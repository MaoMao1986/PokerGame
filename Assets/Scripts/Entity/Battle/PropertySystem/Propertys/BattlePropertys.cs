using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Battle属性组代码逻辑
/// 可以参与战斗的单位身上的属性组，主要是养成结果
/// </summary>
public partial class BattlePropertys : Propertys, IConfigLoad, IPropertysOthers
{
    public void LoadFromConfig(string p_Id)
    {
        InitPropertyList();

        // 读取配置
        DRFightingproperty t_Row = ConfigManager.GetRow<DRFightingproperty>(p_Id);
        if (t_Row != null)
        {
            Hp.Set(t_Row.Hp);
            Mp.Set(t_Row.Mp);
            AtkMin.Set(t_Row.Minatk);
            AtkMax.Set(t_Row.Maxatk);
            Def.Set(t_Row.Def);

            // 设置其他属性
            if (t_Row.Others.Length > 0)
            {
                for (int i = 0; i < t_Row.Others.Length; i++)
                {
                    GetProperty(t_Row.Others[i, 0].ToString()).Set(t_Row.Others[i, 1]);
                }
            }
        }

        // 初始化其他补充属性
        InitPropertyData();

        // 初始化事件
        InitPropertyEvent();
    }

    public override void InitPropertyData()
    {
        
    }

    public override void InitPropertyEvent()
	{
		
		// 待实现，各个属性的事件回调
		//PhyRes.GetMaxFunction = () =>
		//{
		//	return PhyRes.GetConfigMax() + PhyResMax.GetValid();
		//};
		
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
