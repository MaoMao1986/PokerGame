/// <summary>
/// 由FightingUnit.txt生成，工具自动生成，勿手动修改
/// </summary>
public class DRFightingunit : IConfigRow
{
	/// <summary>
	/// id
	/// </summary>
	public string Id {get; private set;}
	/// <summary>
	/// 外观id
	/// </summary>
	public string Display {get; private set;}
	/// <summary>
	/// 属性id
	/// </summary>
	public string Property {get; private set;}
	/// <summary>
	/// 普攻
	/// </summary>
	public string Normalskill {get; private set;}
	/// <summary>
	/// 主动技能序列
	/// </summary>
	public string[] Activeskilllist {get; private set;}
	/// <summary>
	/// 被动技能列表
	/// </summary>
	public string[] Negativeskilllist {get; private set;}

	public void ParseDataRow(string[] p_dataRowString, string[] p_Type)
	{
		int t_Index = 0;
		Id = ConfigManager.TransToString(p_dataRowString[t_Index]); t_Index++;
		Display = ConfigManager.TransToString(p_dataRowString[t_Index]); t_Index++;
		Property = ConfigManager.TransToString(p_dataRowString[t_Index]); t_Index++;
		Normalskill = ConfigManager.TransToString(p_dataRowString[t_Index]); t_Index++;
		Activeskilllist = ConfigManager.TransToStringArray(p_dataRowString[t_Index]); t_Index++;
		Negativeskilllist = ConfigManager.TransToStringArray(p_dataRowString[t_Index]); t_Index++;
	}
}
