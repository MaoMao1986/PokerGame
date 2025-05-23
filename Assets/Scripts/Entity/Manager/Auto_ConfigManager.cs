/// <summary>
/// 配置文件列表，工具自动生成，勿手动修改
/// </summary>
public static partial class ConfigManager
{
	public static void LoadConfig()
	{
		LoadData<DRFightingdisplay>("FightingDisplay.txt");
		LoadData<DRFightingproperty>("FightingProperty.txt");
		LoadData<DRFightingunit>("FightingUnit.txt");
		LoadData<DRPokercard>("PokerCard.txt");
		LoadData<DRProperty>("Property.txt");
		LoadData<DRRandom>("Random.txt");
	}
}
