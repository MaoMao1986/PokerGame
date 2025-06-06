using Newtonsoft.Json;


/// <summary>
/// CurrencyPros属性列表，工具自动生成，勿手动修改
/// 钻石、金币等纯累积性的消耗数值
/// </summary>
public partial class CurrencyPros
{
	/// <summary>
	/// 金币
	/// </summary>
	[JsonIgnore]
	public Property Diamond{ get { return GetProperty("20000"); } }
	/// <summary>
	/// 钻石
	/// </summary>
	[JsonIgnore]
	public Property Gold{ get { return GetProperty("20001"); } }
	
	public override void InitPropertyList(Enum_PropertyInitType p_Type = Enum_PropertyInitType.Zero)
	{
		PropertyList ??= new();
		{
			m_InitDefault("20000" , Property.New("20000", p_Type));
			m_InitDefault("20001" , Property.New("20001", p_Type));
		}
		
		InitData();
		InitEvent();
	}
}
