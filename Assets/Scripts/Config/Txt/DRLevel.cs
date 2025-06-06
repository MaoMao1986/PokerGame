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
	/// 怪物列表
	/// </summary>
	public string[] Monsterlist {get; private set;}
	/// <summary>
	/// 额外胜利条件
	/// </summary>
	public string[] Successcondition {get; private set;}
	/// <summary>
	/// 额外失败条件
	/// </summary>
	public string[] Faildcondition {get; private set;}
	/// <summary>
	/// 首杀奖励
	/// </summary>
	public string Firstkillreward {get; private set;}
	/// <summary>
	/// 奖励
	/// </summary>
	public string Reward {get; private set;}

	public void ParseDataRow(string[] p_dataRowString, string[] p_Type)
	{
		int t_Index = 0;
		Id = ConfigManager.TransToString(p_dataRowString[t_Index]); t_Index++;
		Monsterlist = ConfigManager.TransToStringArray(p_dataRowString[t_Index]); t_Index++;
		Successcondition = ConfigManager.TransToStringArray(p_dataRowString[t_Index]); t_Index++;
		Faildcondition = ConfigManager.TransToStringArray(p_dataRowString[t_Index]); t_Index++;
		Firstkillreward = ConfigManager.TransToString(p_dataRowString[t_Index]); t_Index++;
		Reward = ConfigManager.TransToString(p_dataRowString[t_Index]); t_Index++;
	}
}
