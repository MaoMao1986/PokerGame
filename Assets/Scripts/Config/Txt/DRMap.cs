/// <summary>
/// 由Map.txt生成，工具自动生成，勿手动修改
/// </summary>
public class DRMap : IConfigRow
{
	/// <summary>
	/// id
	/// </summary>
	public string Id {get; private set;}
	/// <summary>
	/// 名称
	/// </summary>
	public string Name {get; private set;}
	/// <summary>
	/// 背景图片
	/// </summary>
	public string Bg {get; private set;}
	/// <summary>
	/// 挂机点列表
	/// </summary>
	public string[] Pointlist {get; private set;}

	public void ParseDataRow(string[] p_dataRowString, string[] p_Type)
	{
		int t_Index = 0;
		Id = ConfigManager.TransToString(p_dataRowString[t_Index]); t_Index++;
		Name = ConfigManager.TransToString(p_dataRowString[t_Index]); t_Index++;
		Bg = ConfigManager.TransToString(p_dataRowString[t_Index]); t_Index++;
		Pointlist = ConfigManager.TransToStringArray(p_dataRowString[t_Index]); t_Index++;
	}
}
