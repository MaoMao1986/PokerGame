using System.Collections.Generic;


/// <summary>
/// DU_PlayerInit对象列表，工具自动生成，勿手动修改
/// </summary>
public partial class DU_PlayerInit
{
	
	public void RaiseDataChanged() => DataChangedEvent?.Invoke();
	
	public override void Init()
	{
		DevelopUnitList ??= new();
		
		// 初始化属性列表
		{
			BattlePropertys ??= new();
			BattlePropertys.InitPropertyList(Enum_PropertyInitType.InitValue);
		}
		
		InitData();
		
		 // 先计算属性，之后再增加属性变化的事件
		CalculateBattlePropertys();
		
		InitEvent();
		
		// 嵌套事件触发（子对象改变触发父对象改变）
		DataChangedEvent += CalculateBattlePropertys;
		
		// 属性组的改变事件挂上父对象的改变事件
		{
		}
		
	}
}
