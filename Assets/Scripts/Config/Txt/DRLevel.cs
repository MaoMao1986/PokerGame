/// <summary>
/// 由Level.txt生成，工具自动生成，勿手动修改
/// </summary>
public class DRLevel : IConfigRow
{
	/// <summary>
	/// id
	/// </summary>
	public string Id {get; private set;}
	/// <summary>
	/// 经验上限
	/// </summary>
	public int Expmax {get; private set;}

	public void ParseDataRow(string[] p_dataRowString, string[] p_Type)
	{
		int t_Index = 0;
		Id = ConfigManager.TransToString(p_dataRowString[t_Index]); t_Index++;
		Expmax = ConfigManager.TransToInt(p_dataRowString[t_Index]); t_Index++;
	}
}
