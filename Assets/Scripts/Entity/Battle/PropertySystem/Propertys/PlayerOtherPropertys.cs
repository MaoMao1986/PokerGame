using System.Collections.Generic;
using UnityEngine;



/// <summary>
/// PlayerOther属性组代码逻辑
/// 角色身上的其他经济属性，例如等级，经验之类的
/// </summary>
public partial class PlayerOtherPropertys : Propertys, IPropertysOthers
{
    public void Init()
    {
        // 初始化当前属性数据
        InitPropertyData();

        // 初始化属性事件
        InitPropertyEvent();
    }

    public void LoadFromOtherPropertys<T>(T p_Propertys) where T : Propertys
    {
        // 初始化属性数据
        Copy(p_Propertys);

        // 初始化当前属性数据
        InitPropertyData();

        // 初始化属性事件
        InitPropertyEvent();
    }

    /// <summary>
    /// 初始化属性值（需要特殊处理的，其他的要么按照配置初始化，要么按照其他属性的数值初始化）
    /// 例如【当前生命】的值需要按照【生命】的有效值初始化
    /// </summary>
    public override void InitPropertyData()
	{
        SetValueInit();

        ExpMax.Set(LoadExpMaxFromConfig(Lv.GetValidValue().ToString()));

		CurrentEnergy.Set(EnergyMax.GetValidValue());
	}
	/// <summary>
	/// 初始化属性的事件（需要特殊处理的）
	/// 例如【当前生命】的最大值，需要取【生命】的当前有效值作为最大值
	/// </summary>
	public override void InitPropertyEvent()
	{
		CurrentEnergy.GetMaxFunction = () =>
        {
            return EnergyMax.GetValidValue();
        };

        Lv.GetMaxFunction = () =>
        {
            return ConfigManager.GetMaxId<DRLevel>();
        };

		Lv.OnValueChanged += (p_Id) =>
        {
            ExpMax.Set(LoadExpMaxFromConfig(Lv.Value.ToString()));
            // 等级变化时，触发属性变化事件
            PropertyChangedEvent?.Invoke(p_Id.Id);
        };

        CurrentExp.OnValueChanged += (p_Id) =>
        {
            UpDateLvAndExp(p_Id.Id);
        };
	}

    /// <summary>
    /// 从配置中加载经验最大值
    /// </summary>
    /// <param name="p_Id"></param>
	public int LoadExpMaxFromConfig(string p_Id)
    {
        DRLevel t_Row = ConfigManager.GetRow<DRLevel>(p_Id);
        if (t_Row == null)
        {
            Debug.LogError($"LoadExpMaxFromConfig: Cannot find DRLevel with ID {p_Id}");
            return 0;
        }
        return t_Row.Expmax;
    }

    public void UpDateLvAndExp(string p_Id)
    {
        int t_Level = Lv.GetValidValue();
        int t_Exp = CurrentExp.GetValidValue();
        int t_ExpMax = ExpMax.GetValidValue();
        if(t_Exp > t_ExpMax && t_Level < Lv.GetMax())
        {
            while (t_Exp > t_ExpMax && t_Level < Lv.GetMax())
            {
                t_Level++;
                t_Exp -= t_ExpMax;
            }
            Lv.Set(t_Level);
            CurrentExp.Set(t_Exp);
        }
    }
}
