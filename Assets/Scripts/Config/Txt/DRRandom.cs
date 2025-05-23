/// <summary>
/// 由Random.txt生成，工具自动生成，勿手动修改
/// </summary>
public class DRRandom : IConfigRow
{
	/// <summary>
	/// ID
	/// </summary>
	public string Id {get; private set;}
	/// <summary>
	/// 掉率模式
	/// </summary>
	public Enum_RandomType Randomtype {get; private set;}
	/// <summary>
	/// 掉落配置
	/// </summary>
	public string[,] Randomconfig {get; private set;}

	public void ParseDataRow(string[] p_dataRowString, string[] p_Type)
	{
		int t_Index = 0;
		Id = ConfigManager.TransToString(p_dataRowString[t_Index]); t_Index++;
		Randomtype = ConfigManager.TransToEnum<Enum_RandomType>(p_dataRowString[t_Index]); t_Index++;
		Randomconfig = ConfigManager.TransToStringArray2(p_dataRowString[t_Index]); t_Index++;
	}
}
