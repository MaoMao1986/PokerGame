using System.Collections.Generic;


/// <summary>
/// DU_Player对象列表，工具自动生成，勿手动修改
/// </summary>
public partial class DU_Player 
{
	/// <summary>
	/// 角色初始
	/// </summary>
	public DU_PlayerInit PlayerInit{ get; set;}
	/// <summary>
	/// 角色等级
	/// </summary>
	public DU_PlayerLevel PlayerLevel{ get; set;}
	/// <summary>
	/// 地图点组
	/// </summary>
	public DU_Group<string , DU_MapPoint> MapPointGroup{ get; set;}
	/// <summary>
	/// 地图点1
	/// </summary>
	public DU_MapPoint MapPoint1{ get; set;}
	
	/// <summary>
	/// 角色其他属性
	/// </summary>
	public OtherPros OtherPros { get; set; }
	/// <summary>
	/// 货币
	/// </summary>
	public CurrencyPros CurrencyPros { get; set; }
	
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
			
			MapPointGroup ??= new();
			MapPointGroup.Init();
			DevelopUnitList.Add(MapPointGroup.Name, MapPointGroup);
			
			MapPoint1 ??= new();
			MapPoint1.Init();
			DevelopUnitList.Add(MapPoint1.Name, MapPoint1);
		}
		
		// 初始化属性列表
		{
			BattlePropertys ??= new();
			BattlePropertys.InitPropertyList();
			
			OtherPros ??= new();
			OtherPros.InitPropertyList(Enum_PropertyInitType.InitValue);
			
			CurrencyPros ??= new();
			CurrencyPros.InitPropertyList(Enum_PropertyInitType.InitValue);
		}
		
		InitData();
		
		 // 先计算属性，之后再增加属性变化的事件
		CalculateBattlePropertys();
		
		InitEvent();
		
		// 嵌套事件触发（子对象改变触发父对象改变）
		DataChangedEvent += CalculateBattlePropertys;
		
		// 属性组的改变事件挂上父对象的改变事件
		{
			OtherPros.DataChangedEvent += RaiseDataChanged;
			CurrencyPros.DataChangedEvent += RaiseDataChanged;
		}
		
		// 子对象的改变事件挂上父对象的改变事件
		{
			PlayerInit.DataChangedEvent += RaiseDataChanged;
			PlayerLevel.DataChangedEvent += RaiseDataChanged;
			MapPointGroup.DataChangedEvent += RaiseDataChanged;
			MapPoint1.DataChangedEvent += RaiseDataChanged;
		}
		
	}
}
