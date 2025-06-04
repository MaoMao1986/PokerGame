using Newtonsoft.Json;


/// <summary>
/// PlayerPros属性列表，工具自动生成，勿手动修改
/// 角色身上的其他经济属性，例如等级，经验之类的
/// </summary>
public partial class PlayerPros
{
	/// <summary>
	/// 等级
	/// </summary>
	[JsonIgnore]
	public Property Lv{ get { return GetProperty("10000"); } }
	/// <summary>
	/// 经验
	/// </summary>
	[JsonIgnore]
	public Property CurrentExp{ get { return GetProperty("10001"); } }
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
			m_InitDefault("10000" , Property.New("10000", p_Type));
			m_InitDefault("10001" , Property.New("10001", p_Type));
			m_InitDefault("10010" , Property.New("10010", p_Type));
			m_InitDefault("10011" , Property.New("10011", p_Type));
		}
		
		InitData();
		InitEvent();
	}
}
