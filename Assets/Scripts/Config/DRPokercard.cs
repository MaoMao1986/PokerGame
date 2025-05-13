/// <summary>
/// 由PokerCard.txt生成，工具自动生成，勿手动修改
/// </summary>
public class DRPokercard : IConfigRow
{
	public string Id {get; private set;}
	public int Suit {get; private set;}
	public int Rank {get; private set;}
	public string Name {get; private set;}
	public string Icon {get; private set;}

	public void ParseDataRow(string[] p_dataRowString, string[] p_Type)
	{
		int t_Index = 0;
		Id = CfgTableMgr.TransToString(p_dataRowString[t_Index]); t_Index++;
		Suit = CfgTableMgr.TransToInt(p_dataRowString[t_Index]); t_Index++;
		Rank = CfgTableMgr.TransToInt(p_dataRowString[t_Index]); t_Index++;
		Name = CfgTableMgr.TransToString(p_dataRowString[t_Index]); t_Index++;
		Icon = CfgTableMgr.TransToString(p_dataRowString[t_Index]); t_Index++;
	}
}
