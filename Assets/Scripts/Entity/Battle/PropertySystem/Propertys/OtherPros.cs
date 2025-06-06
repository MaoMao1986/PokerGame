using System.Collections.Generic;


/// <summary>
/// OtherPros属性组代码逻辑
/// 体力、经验获得提升等其他杂属性
/// </summary>
public partial class OtherPros : Propertys, ISaveDataBase
{
	public DataChangedEventHandler DataChangedEvent { get; set; }
	
	/// <summary>
	/// 初始化属性值（需要特殊处理的，其他的要么按照配置初始化，要么按照其他属性的数值初始化）
	/// 例如【当前生命】的值需要按照【生命】的有效值初始化
	/// </summary>
	public void InitData()
	{
	
	}
	/// <summary>
	/// 初始化属性的事件（需要特殊处理的）
	/// 例如【当前生命】的最大值，需要取【生命】的当前有效值作为最大值
	/// </summary>
	public void InitEvent()
	{
	
	}
}
