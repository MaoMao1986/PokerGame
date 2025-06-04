/// <summary>
/// 由MapPoint.txt生成，工具自动生成，勿手动修改
/// </summary>
public class DRMappoint : IConfigRow
{
	/// <summary>
	/// id
	/// </summary>
	public string Id {get; private set;}
	/// <summary>
	/// 类型
	/// </summary>
	public string Type {get; private set;}
	/// <summary>
	/// x
	/// </summary>
	public double X {get; private set;}
	/// <summary>
	/// y
	/// </summary>
	public double Y {get; private set;}
	/// <summary>
	/// 名称
	/// </summary>
	public string Name {get; private set;}
	/// <summary>
	/// 图片
	/// </summary>
	public string Icon {get; private set;}
	/// <summary>
	/// 前置挂机点列表
	/// </summary>
	public string[] Prefixpoints {get; private set;}

	public void ParseDataRow(string[] p_dataRowString, string[] p_Type)
	{
		int t_Index = 0;
		Id = ConfigManager.TransToString(p_dataRowString[t_Index]); t_Index++;
		Type = ConfigManager.TransToString(p_dataRowString[t_Index]); t_Index++;
		X = ConfigManager.TransToDouble(p_dataRowString[t_Index]); t_Index++;
		Y = ConfigManager.TransToDouble(p_dataRowString[t_Index]); t_Index++;
		Name = ConfigManager.TransToString(p_dataRowString[t_Index]); t_Index++;
		Icon = ConfigManager.TransToString(p_dataRowString[t_Index]); t_Index++;
		Prefixpoints = ConfigManager.TransToStringArray(p_dataRowString[t_Index]); t_Index++;
	}
}
