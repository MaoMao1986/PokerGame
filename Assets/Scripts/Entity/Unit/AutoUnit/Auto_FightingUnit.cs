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
	
	public void RaiseDataChanged() => DataChangedEvent?.Invoke();
	
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
		
		 // 先计算属性，之后再增加属性变化的事件
		CalculateBattlePropertys();
		
		InitEvent();
		
		// 嵌套事件触发（子对象改变触发父对象改变）
		DataChangedEvent += CalculateBattlePropertys;
		
		// 属性组的改变事件挂上父对象的改变事件
		{
			FightingPropertys.DataChangedEvent += RaiseDataChanged;
		}
		
	}
}
