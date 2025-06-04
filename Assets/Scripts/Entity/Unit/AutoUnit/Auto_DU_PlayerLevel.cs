using System.Collections.Generic;


/// <summary>
/// DU_PlayerLevel对象列表，工具自动生成，勿手动修改
/// </summary>
public partial class DU_PlayerLevel
{
	/// <summary>
	/// 角色经济属性
	/// </summary>
	public PlayerPros PlayerPros { get; set; }
	
	
	public override void Init()
	{
		DevelopUnitList ??= new();
		
		
		// 初始化属性列表
		{
			m_BattlePropertys ??= new();
			m_BattlePropertys.InitPropertyList();
			
			PlayerPros ??= new();
			PlayerPros.InitPropertyList(Enum_PropertyInitType.InitValue);
			
		}
		
		InitData();
		CalculateBattlePropertys();
		InitEvent();
	}
}
