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
		LoadData<DRLevel>("Level.txt");
		LoadData<DRLevelendcondition>("LevelEndCondition.txt");
		LoadData<DRMap>("Map.txt");
		LoadData<DRMappoint>("MapPoint.txt");
		LoadData<DRPlayerlv>("PlayerLv.txt");
		LoadData<DRPointprefix>("PointPrefix.txt");
		LoadData<DRPokercard>("PokerCard.txt");
		LoadData<DRProperty>("Property.txt");
		LoadData<DRRandom>("Random.txt");
	}
}
