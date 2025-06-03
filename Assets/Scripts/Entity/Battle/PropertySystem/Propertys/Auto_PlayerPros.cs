using System.Collections.Generic;


/// <summary>
/// PlayerOther属性列表，工具自动生成，勿手动修改
/// 角色身上的其他经济属性，例如等级，经验之类的
/// </summary>
public partial class PlayerPros
{
	/// <summary>
	/// 等级
	/// </summary>
	public Property Lv { get; set; } = Property.New("10000");
	/// <summary>
	/// 经验
	/// </summary>
	public Property CurrentExp { get; set; } = Property.New("10001");
	/// <summary>
	/// 经验上限
	/// </summary>
	public Property ExpMax { get; set; } = Property.New("10002");
	/// <summary>
	/// 体力
	/// </summary>
	public Property CurrentEnergy { get; set; } = Property.New("10010");
	/// <summary>
	/// 体力上限
	/// </summary>
	public Property EnergyMax { get; set; } = Property.New("10011");
	
	public override void InitPropertyList()
	{
		m_PropertyList = new()
		{
			{"10000" , Lv},
			{"10001" , CurrentExp},
			{"10002" , ExpMax},
			{"10010" , CurrentEnergy},
			{"10011" , EnergyMax},
		};
	}
}
