using Newtonsoft.Json;


/// <summary>
/// LvPros属性列表，工具自动生成，勿手动修改
/// 特指等级和经验，包含升级的各种模板（自动、手动升级），消耗的内容使用其他东西替代等
/// </summary>
public partial class LvPros
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
	
	public override void InitPropertyList(Enum_PropertyInitType p_Type = Enum_PropertyInitType.Zero)
	{
		PropertyList ??= new();
		{
			m_InitDefault("10000" , Property.New("10000", p_Type));
			m_InitDefault("10001" , Property.New("10001", p_Type));
		}
		
		InitData();
		InitEvent();
	}
}
