using System.Collections.Generic;


/// <summary>
/// DU_Player对象列表，工具自动生成，勿手动修改
/// </summary>
public partial class DU_Player
{
	/// <summary>
	/// 角色初始属性结构
	/// </summary>
	public DU_PlayerInit PlayerInit{ get; set;}
	/// <summary>
	/// 角色等级属性结构
	/// </summary>
	public DU_PlayerLevel PlayerLevel{ get; set;}
	
	
	
	public override void Init()
	{
		DevelopUnitList ??= new();
		
		// 初始化子对象
		{
			PlayerInit ??= new();
			PlayerInit.Init();
			DevelopUnitList.Add(PlayerInit.Name, PlayerInit);
			
			PlayerLevel ??= new();
			PlayerLevel.Init();
			DevelopUnitList.Add(PlayerLevel.Name, PlayerLevel);
			
		}
		
		// 初始化属性列表
		{
			m_BattlePropertys ??= new();
			m_BattlePropertys.InitPropertyList();
			
		}
		
		InitData();
		CalculateBattlePropertys();
		InitEvent();
	}
}
