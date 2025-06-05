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
	
	
	public void RaiseDataChanged() => DataChangedEvent?.Invoke();
	
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
		}
		
		// 子对象的改变事件挂上父对象的改变事件
		{
			PlayerInit.DataChangedEvent += RaiseDataChanged;
			PlayerLevel.DataChangedEvent += RaiseDataChanged;
		}
		
	}
}
