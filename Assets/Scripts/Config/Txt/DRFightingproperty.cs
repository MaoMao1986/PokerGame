/// <summary>
/// 由FightingProperty.txt生成，工具自动生成，勿手动修改
/// </summary>
public class DRFightingproperty : IConfigRow
{
	/// <summary>
	/// id
	/// </summary>
	public string Id {get; private set;}
	/// <summary>
	/// 最小攻击
	/// </summary>
	public int Minatk {get; private set;}
	/// <summary>
	/// 最大攻击
	/// </summary>
	public int Maxatk {get; private set;}
	/// <summary>
	/// 防
	/// </summary>
	public int Def {get; private set;}
	/// <summary>
	/// 血
	/// </summary>
	public int Hp {get; private set;}
	/// <summary>
	/// 蓝
	/// </summary>
	public int Mp {get; private set;}
	/// <summary>
	/// 其他属性
	/// </summary>
	public int[,] Others {get; private set;}

	public void ParseDataRow(string[] p_dataRowString, string[] p_Type)
	{
		int t_Index = 0;
		Id = ConfigManager.TransToString(p_dataRowString[t_Index]); t_Index++;
		Minatk = ConfigManager.TransToInt(p_dataRowString[t_Index]); t_Index++;
		Maxatk = ConfigManager.TransToInt(p_dataRowString[t_Index]); t_Index++;
		Def = ConfigManager.TransToInt(p_dataRowString[t_Index]); t_Index++;
		Hp = ConfigManager.TransToInt(p_dataRowString[t_Index]); t_Index++;
		Mp = ConfigManager.TransToInt(p_dataRowString[t_Index]); t_Index++;
		Others = ConfigManager.TransToIntArray2(p_dataRowString[t_Index]); t_Index++;
	}
}
