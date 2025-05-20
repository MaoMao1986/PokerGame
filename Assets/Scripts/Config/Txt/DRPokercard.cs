/// <summary>
/// 由PokerCard.txt生成，工具自动生成，勿手动修改
/// </summary>
public class DRPokercard : IConfigRow
{
	public string Id {get; private set;}
	public Enum_CardSuit Suit {get; private set;}
	public Enum_CardRank Rank {get; private set;}
	public string Name {get; private set;}
	public string Icon {get; private set;}

	public void ParseDataRow(string[] p_dataRowString, string[] p_Type)
	{
		int t_Index = 0;
		Id = ConfigManager.TransToString(p_dataRowString[t_Index]); t_Index++;
		Suit = ConfigManager.TransToEnum<Enum_CardSuit>(p_dataRowString[t_Index]); t_Index++;
		Rank = ConfigManager.TransToEnum<Enum_CardRank>(p_dataRowString[t_Index]); t_Index++;
		Name = ConfigManager.TransToString(p_dataRowString[t_Index]); t_Index++;
		Icon = ConfigManager.TransToString(p_dataRowString[t_Index]); t_Index++;
	}
}
