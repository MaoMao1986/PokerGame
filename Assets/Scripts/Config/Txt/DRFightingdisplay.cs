/// <summary>
/// 由FightingDisplay.txt生成，工具自动生成，勿手动修改
/// </summary>
public class DRFightingdisplay : IConfigRow
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
	/// 图标
	/// </summary>
	public string Icon {get; private set;}

	public void ParseDataRow(string[] p_dataRowString, string[] p_Type)
	{
		int t_Index = 0;
		Id = ConfigManager.TransToString(p_dataRowString[t_Index]); t_Index++;
		Name = ConfigManager.TransToString(p_dataRowString[t_Index]); t_Index++;
		Icon = ConfigManager.TransToString(p_dataRowString[t_Index]); t_Index++;
	}
}
