/// <summary>
/// 配置文件列表，工具自动生成，勿手动修改
/// </summary>
public static partial class ConfigManager
{
	public static void LoadConfig()
	{
		LoadData<DRPokercard>("PokerCard.txt");
		LoadData<DRProperty>("Property.txt");
	}
}
