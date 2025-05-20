using System.Collections.Generic;


/// <summary>
/// 经济属性列表，工具自动生成，勿手动修改
/// </summary>
public partial class EconomyPropertys
{
	/// <summary>
	/// 幸运
	/// </summary>
	public Property Luck { get; set; } = Property.New(10000);
	
	public override void InitPropertyList()
	{
		PropertyList = new()
		{
			{10000 , Luck},
		};
	}
}
