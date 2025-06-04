using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Battle属性组代码逻辑
/// 可以参与战斗的单位身上的属性组，主要是养成结果
/// </summary>
public partial class BattlePros : Propertys, ISaveDataBase, IConfigLoad
{
    public DataChangedEventHandler DataChangedEvent { get; set; }

    public void LoadConfig(string p_Id)
    {
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
                for (int i = 0; i < t_Row.Others.GetLength(0); i++)
                {
                    Property t_Property = GetProperty(t_Row.Others[i, 0].ToString());
                    t_Property.Set(t_Row.Others[i, 1]);
                }
            }
        }
    }

    public void InitData()
    {
        // 初始化其他属性

    }

    public void InitEvent()
	{
		
	}
}
