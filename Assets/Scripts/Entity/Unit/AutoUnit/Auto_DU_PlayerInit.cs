using System.Collections.Generic;


/// <summary>
/// DU_PlayerInit对象列表，工具自动生成，勿手动修改
/// </summary>
public partial class DU_PlayerInit
{
	
	
	public override void Init()
	{
		DevelopUnitList ??= new();
		
		
		// 初始化属性列表
		{
			BattlePropertys ??= new();
			BattlePropertys.InitPropertyList(Enum_PropertyInitType.InitValue);
			
		}
		
		InitData();
		CalculateBattlePropertys();
		InitEvent();
	}
}
