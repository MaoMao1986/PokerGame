/// <summary>
/// 由PointPrefix.txt生成，工具自动生成，勿手动修改
/// </summary>
public class DRPointprefix : IConfigRow
{
	/// <summary>
	/// id
	/// </summary>
	public string Id {get; private set;}
	/// <summary>
	/// 需求前置挂机点
	/// </summary>
	public string Prefixpoint {get; private set;}
	/// <summary>
	/// 需求前置挂机点等级
	/// </summary>
	public int Lv {get; private set;}

	public void ParseDataRow(string[] p_dataRowString, string[] p_Type)
	{
		int t_Index = 0;
		Id = ConfigManager.TransToString(p_dataRowString[t_Index]); t_Index++;
		Prefixpoint = ConfigManager.TransToString(p_dataRowString[t_Index]); t_Index++;
		Lv = ConfigManager.TransToInt(p_dataRowString[t_Index]); t_Index++;
	}
}
