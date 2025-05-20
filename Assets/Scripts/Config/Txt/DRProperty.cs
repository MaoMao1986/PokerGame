/// <summary>
/// 由Property.txt生成，工具自动生成，勿手动修改
/// </summary>
public class DRProperty : IConfigRow
{
	public string Id {get; private set;}
	public string Type {get; private set;}
	public string Subtype {get; private set;}
	public string Enumname {get; private set;}
	public string Displayname {get; private set;}
	public string Displaycolor {get; private set;}
	public string Displaytype {get; private set;}
	public int Validvalue {get; private set;}
	public string Initvalue {get; private set;}
	public string Maxvalue {get; private set;}
	public string Content {get; private set;}

	public void ParseDataRow(string[] p_dataRowString, string[] p_Type)
	{
		int t_Index = 0;
		Id = ConfigManager.TransToString(p_dataRowString[t_Index]); t_Index++;
		Type = ConfigManager.TransToString(p_dataRowString[t_Index]); t_Index++;
		Subtype = ConfigManager.TransToString(p_dataRowString[t_Index]); t_Index++;
		Enumname = ConfigManager.TransToString(p_dataRowString[t_Index]); t_Index++;
		Displayname = ConfigManager.TransToString(p_dataRowString[t_Index]); t_Index++;
		Displaycolor = ConfigManager.TransToString(p_dataRowString[t_Index]); t_Index++;
		Displaytype = ConfigManager.TransToString(p_dataRowString[t_Index]); t_Index++;
		Validvalue = ConfigManager.TransToInt(p_dataRowString[t_Index]); t_Index++;
		Initvalue = ConfigManager.TransToString(p_dataRowString[t_Index]); t_Index++;
		Maxvalue = ConfigManager.TransToString(p_dataRowString[t_Index]); t_Index++;
		Content = ConfigManager.TransToString(p_dataRowString[t_Index]); t_Index++;
	}
}
