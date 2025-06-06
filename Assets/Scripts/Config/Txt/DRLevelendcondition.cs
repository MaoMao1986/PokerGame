/// <summary>
/// 由LevelEndCondition.txt生成，工具自动生成，勿手动修改
/// </summary>
public class DRLevelendcondition : IConfigRow
{
	/// <summary>
	/// id
	/// </summary>
	public string Id {get; private set;}
	/// <summary>
	/// 胜负
	/// </summary>
	public Enum_LevelEndType Result {get; private set;}
	/// <summary>
	/// 条件类型
	/// </summary>
	public int Conditiontype {get; private set;}
	/// <summary>
	/// 条件参数列表
	/// </summary>
	public string[] Paramlist {get; private set;}

	public void ParseDataRow(string[] p_dataRowString, string[] p_Type)
	{
		int t_Index = 0;
		Id = ConfigManager.TransToString(p_dataRowString[t_Index]); t_Index++;
		Result = ConfigManager.TransToEnum<Enum_LevelEndType>(p_dataRowString[t_Index]); t_Index++;
		Conditiontype = ConfigManager.TransToInt(p_dataRowString[t_Index]); t_Index++;
		Paramlist = ConfigManager.TransToStringArray(p_dataRowString[t_Index]); t_Index++;
	}
}
