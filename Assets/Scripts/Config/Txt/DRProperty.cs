/// <summary>
/// 由Property.txt生成，工具自动生成，勿手动修改
/// </summary>
public class DRProperty : IConfigRow
{
	/// <summary>
	/// id
	/// </summary>
	public string Id {get; private set;}
	/// <summary>
	/// 枚举名称
	/// </summary>
	public string Enumname {get; private set;}
	/// <summary>
	/// 显示名称
	/// </summary>
	public string Displayname {get; private set;}
	/// <summary>
	/// 显示颜色
	/// </summary>
	public string Displaycolor {get; private set;}
	/// <summary>
	/// 初始化值
	/// </summary>
	public int Initvalue {get; private set;}
	/// <summary>
	/// 显示生效值
	/// </summary>
	public int Validvalue {get; private set;}
	/// <summary>
	/// 显示格式
	/// </summary>
	public string Displaytype {get; private set;}
	/// <summary>
	/// 下限
	/// </summary>
	public int Min {get; private set;}
	/// <summary>
	/// 超下限处理
	/// </summary>
	public Enum_PropertyLimitSet Minmethod {get; private set;}
	/// <summary>
	/// 上限
	/// </summary>
	public int Max {get; private set;}
	/// <summary>
	/// 超上限处理
	/// </summary>
	public Enum_PropertyLimitSet Maxmethod {get; private set;}
	/// <summary>
	/// 属性描述
	/// </summary>
	public string Content {get; private set;}

	public void ParseDataRow(string[] p_dataRowString, string[] p_Type)
	{
		int t_Index = 0;
		Id = ConfigManager.TransToString(p_dataRowString[t_Index]); t_Index++;
		Enumname = ConfigManager.TransToString(p_dataRowString[t_Index]); t_Index++;
		Displayname = ConfigManager.TransToString(p_dataRowString[t_Index]); t_Index++;
		Displaycolor = ConfigManager.TransToString(p_dataRowString[t_Index]); t_Index++;
		Initvalue = ConfigManager.TransToInt(p_dataRowString[t_Index]); t_Index++;
		Validvalue = ConfigManager.TransToInt(p_dataRowString[t_Index]); t_Index++;
		Displaytype = ConfigManager.TransToString(p_dataRowString[t_Index]); t_Index++;
		Min = ConfigManager.TransToInt(p_dataRowString[t_Index]); t_Index++;
		Minmethod = ConfigManager.TransToEnum<Enum_PropertyLimitSet>(p_dataRowString[t_Index]); t_Index++;
		Max = ConfigManager.TransToInt(p_dataRowString[t_Index]); t_Index++;
		Maxmethod = ConfigManager.TransToEnum<Enum_PropertyLimitSet>(p_dataRowString[t_Index]); t_Index++;
		Content = ConfigManager.TransToString(p_dataRowString[t_Index]); t_Index++;
	}
}
