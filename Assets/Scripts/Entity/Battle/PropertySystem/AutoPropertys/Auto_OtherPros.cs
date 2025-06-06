using Newtonsoft.Json;


/// <summary>
/// OtherPros属性列表，工具自动生成，勿手动修改
/// 体力、经验获得提升等其他杂属性
/// </summary>
public partial class OtherPros
{
	/// <summary>
	/// 体力
	/// </summary>
	[JsonIgnore]
	public Property CurrentEnergy{ get { return GetProperty("10010"); } }
	/// <summary>
	/// 体力上限
	/// </summary>
	[JsonIgnore]
	public Property EnergyMax{ get { return GetProperty("10011"); } }
	
	public override void InitPropertyList(Enum_PropertyInitType p_Type = Enum_PropertyInitType.Zero)
	{
		PropertyList ??= new();
		{
			m_InitDefault("10010" , Property.New("10010", p_Type));
			m_InitDefault("10011" , Property.New("10011", p_Type));
		}
		
		InitData();
		InitEvent();
	}
}
