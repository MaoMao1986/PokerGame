using System.Collections.Generic;


/// <summary>
/// FightingUnit对象列表，工具自动生成，勿手动修改
/// </summary>
public partial class FightingUnit
{
	/// <summary>
	/// 战时属性
	/// </summary>
	public FightPros FightingPropertys { get; set; }
	
	
	public override void Init()
	{
		DevelopUnitList ??= new();
		
		
		// 初始化属性列表
		{
			FightingPropertys ??= new();
			FightingPropertys.InitPropertyList();
			
			BattlePropertys ??= new();
			BattlePropertys.InitPropertyList();
			
		}
		
		InitData();
		CalculateBattlePropertys();
		InitEvent();
	}
}
