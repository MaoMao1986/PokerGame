using UnityEngine;

/// <summary>
/// PlayerOther属性组代码逻辑
/// 角色身上的其他经济属性，例如等级，经验之类的
/// </summary>
public partial class PlayerPros : Propertys, ISaveDataBase
{
    public DataChangedEventHandler DataChangedEvent { get; set; }

    public void CreateNew()
    {
        SetValueInit();
        CurrentEnergy.Set(EnergyMax.GetValidValue());
    }

    /// <summary>
    /// 初始化属性值（需要特殊处理的，其他的要么按照配置初始化，要么按照其他属性的数值初始化）
    /// 例如【当前生命】的值需要按照【生命】的有效值初始化
    /// </summary>
    public void InitData()
	{
        SetValueInit();
        // 初始化事件
        InitEvent();
    }
	/// <summary>
	/// 初始化属性的事件（需要特殊处理的）
	/// 例如【当前生命】的最大值，需要取【生命】的当前有效值作为最大值
	/// </summary>
	public void InitEvent()
	{
		CurrentEnergy.GetMaxFunction = () =>
        {
            return EnergyMax.GetValidValue();
        };

        CurrentExp.GetMaxFunction = () =>
        {
            return LoadExpMaxFromConfig(Lv.GetValidValue().ToString());
        };

        Lv.GetMaxFunction = () =>
        {
            return ConfigManager.GetMaxId<DRLevel>();
        };

        CurrentExp.OnValueAdded += (p_Id) =>
        {
            UpDateLvAndExp();
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

    /// <summary>
    /// 更新等级和经验值
    /// </summary>
    public void UpDateLvAndExp()
    {
        int t_Level = Lv.GetValidValue();
        int t_Exp = CurrentExp.GetValidValue();
        int t_ReducedExp = LoadExpMaxFromConfig(t_Level.ToString());
        int t_MaxExp = 0;
        int t_AddLv = 0;
        if(t_Exp >= (t_ReducedExp + t_MaxExp) && t_Level < Lv.GetMax())
        {
            while (t_Exp >= (t_ReducedExp + t_MaxExp) && t_Level < Lv.GetMax())
            {
                t_AddLv++;
                t_ReducedExp += t_MaxExp;
                t_MaxExp = ConfigManager.GetRow<DRLevel>((t_Level + t_AddLv).ToString()).Expmax;
            }
            Lv.Add(t_AddLv,out int t_AddValue);
            CurrentExp.Reduce(t_ReducedExp, out int t_RedeceValue);
        }
    }
}
