using Newtonsoft.Json;


/// <summary>
/// FightPros属性列表，工具自动生成，勿手动修改
/// 在战斗中使用的属性组，主要和战斗属性不同的点在于会多一些当前属性，例如当前血量，当前蓝量等
/// </summary>
public partial class FightPros
{
	/// <summary>
	/// 生命
	/// </summary>
	[JsonIgnore]
	public Property Hp{ get { return GetProperty("1"); } }
	/// <summary>
	/// 法力
	/// </summary>
	[JsonIgnore]
	public Property Mp{ get { return GetProperty("2"); } }
	/// <summary>
	/// 当前生命
	/// </summary>
	[JsonIgnore]
	public Property CurrentHp{ get { return GetProperty("10"); } }
	/// <summary>
	/// 当前法力
	/// </summary>
	[JsonIgnore]
	public Property CurrentMp{ get { return GetProperty("11"); } }
	/// <summary>
	/// 最小攻击
	/// </summary>
	[JsonIgnore]
	public Property AtkMin{ get { return GetProperty("100"); } }
	/// <summary>
	/// 最大攻击
	/// </summary>
	[JsonIgnore]
	public Property AtkMax{ get { return GetProperty("101"); } }
	/// <summary>
	/// 防御
	/// </summary>
	[JsonIgnore]
	public Property Def{ get { return GetProperty("103"); } }
	/// <summary>
	/// 坚韧概率
	/// </summary>
	[JsonIgnore]
	public Property DefMultPro{ get { return GetProperty("104"); } }
	/// <summary>
	/// 坚韧倍率
	/// </summary>
	[JsonIgnore]
	public Property DefMult{ get { return GetProperty("105"); } }
	/// <summary>
	/// 致命一击
	/// </summary>
	[JsonIgnore]
	public Property FatalAtk{ get { return GetProperty("106"); } }
	/// <summary>
	/// 出手速度
	/// </summary>
	[JsonIgnore]
	public Property AttackSpeed{ get { return GetProperty("107"); } }
	/// <summary>
	/// 增加伤害
	/// </summary>
	[JsonIgnore]
	public Property DmgAdd{ get { return GetProperty("200"); } }
	/// <summary>
	/// 减少伤害
	/// </summary>
	[JsonIgnore]
	public Property DmgReduce{ get { return GetProperty("201"); } }
	/// <summary>
	/// 增加伤害百分比
	/// </summary>
	[JsonIgnore]
	public Property DmgAddPercent{ get { return GetProperty("202"); } }
	/// <summary>
	/// 减少伤害百分比
	/// </summary>
	[JsonIgnore]
	public Property DmgReducePercent{ get { return GetProperty("203"); } }
	/// <summary>
	/// 命中
	/// </summary>
	[JsonIgnore]
	public Property Hit{ get { return GetProperty("300"); } }
	/// <summary>
	/// 闪避
	/// </summary>
	[JsonIgnore]
	public Property Dodge{ get { return GetProperty("301"); } }
	/// <summary>
	/// 暴击
	/// </summary>
	[JsonIgnore]
	public Property Critical{ get { return GetProperty("400"); } }
	/// <summary>
	/// 韧性
	/// </summary>
	[JsonIgnore]
	public Property Tough{ get { return GetProperty("401"); } }
	/// <summary>
	/// 暴击倍率
	/// </summary>
	[JsonIgnore]
	public Property CriticalMult{ get { return GetProperty("402"); } }
	/// <summary>
	/// 生命恢复速度
	/// </summary>
	[JsonIgnore]
	public Property HpRestore{ get { return GetProperty("500"); } }
	/// <summary>
	/// 生命恢复速度百分比
	/// </summary>
	[JsonIgnore]
	public Property HpRestorePercent{ get { return GetProperty("501"); } }
	/// <summary>
	/// 降低生命恢复速度
	/// </summary>
	[JsonIgnore]
	public Property ReduceHpRestore{ get { return GetProperty("502"); } }
	/// <summary>
	/// 降低生命恢复速度百分比
	/// </summary>
	[JsonIgnore]
	public Property ReduceHpRestorePercent{ get { return GetProperty("503"); } }
	/// <summary>
	/// 法力恢复速度
	/// </summary>
	[JsonIgnore]
	public Property MpRestore{ get { return GetProperty("508"); } }
	/// <summary>
	/// 法力恢复速度百分比
	/// </summary>
	[JsonIgnore]
	public Property MpRestorePercent{ get { return GetProperty("509"); } }
	/// <summary>
	/// 降低法力恢复速度
	/// </summary>
	[JsonIgnore]
	public Property ReduceMpRestore{ get { return GetProperty("510"); } }
	/// <summary>
	/// 降低法力恢复速度百分比
	/// </summary>
	[JsonIgnore]
	public Property ReduceMpRestorePercent{ get { return GetProperty("511"); } }
	/// <summary>
	/// 降低法力消耗
	/// </summary>
	[JsonIgnore]
	public Property ReduceMpCost{ get { return GetProperty("600"); } }
	/// <summary>
	/// 降低法力消耗百分比
	/// </summary>
	[JsonIgnore]
	public Property ReduceMpCostPercent{ get { return GetProperty("601"); } }
	/// <summary>
	/// 增加法力消耗
	/// </summary>
	[JsonIgnore]
	public Property AddMpCost{ get { return GetProperty("602"); } }
	/// <summary>
	/// 增加法力消耗百分比
	/// </summary>
	[JsonIgnore]
	public Property AddMpCostPercent{ get { return GetProperty("603"); } }
	/// <summary>
	/// 物理伤害百分比
	/// </summary>
	[JsonIgnore]
	public Property PhyPercent{ get { return GetProperty("700"); } }
	/// <summary>
	/// 物理抗性
	/// </summary>
	[JsonIgnore]
	public Property PhyRes{ get { return GetProperty("701"); } }
	/// <summary>
	/// 物理抗性上限
	/// </summary>
	[JsonIgnore]
	public Property PhyResMax{ get { return GetProperty("702"); } }
	/// <summary>
	/// 降低敌人物理抗性
	/// </summary>
	[JsonIgnore]
	public Property ReducePhyRes{ get { return GetProperty("703"); } }
	/// <summary>
	/// 金系伤害百分比
	/// </summary>
	[JsonIgnore]
	public Property GoldPercent{ get { return GetProperty("800"); } }
	/// <summary>
	/// 金系抗性
	/// </summary>
	[JsonIgnore]
	public Property GoldRes{ get { return GetProperty("801"); } }
	/// <summary>
	/// 金系抗性上限
	/// </summary>
	[JsonIgnore]
	public Property GoldResMax{ get { return GetProperty("802"); } }
	/// <summary>
	/// 降低金系抗性
	/// </summary>
	[JsonIgnore]
	public Property ReduceGoldRes{ get { return GetProperty("803"); } }
	/// <summary>
	/// 木系伤害百分比
	/// </summary>
	[JsonIgnore]
	public Property WoodPercent{ get { return GetProperty("900"); } }
	/// <summary>
	/// 木系抗性
	/// </summary>
	[JsonIgnore]
	public Property WoodRes{ get { return GetProperty("901"); } }
	/// <summary>
	/// 木系抗性上限
	/// </summary>
	[JsonIgnore]
	public Property WoodResMax{ get { return GetProperty("902"); } }
	/// <summary>
	/// 降低木系抗性
	/// </summary>
	[JsonIgnore]
	public Property ReduceWoodRes{ get { return GetProperty("903"); } }
	/// <summary>
	/// 水系伤害百分比
	/// </summary>
	[JsonIgnore]
	public Property WaterPercent{ get { return GetProperty("1000"); } }
	/// <summary>
	/// 水系抗性
	/// </summary>
	[JsonIgnore]
	public Property WaterRes{ get { return GetProperty("1001"); } }
	/// <summary>
	/// 水系抗性上限
	/// </summary>
	[JsonIgnore]
	public Property WaterResMax{ get { return GetProperty("1002"); } }
	/// <summary>
	/// 降低水系抗性
	/// </summary>
	[JsonIgnore]
	public Property ReduceWaterRes{ get { return GetProperty("1003"); } }
	/// <summary>
	/// 火系伤害百分比
	/// </summary>
	[JsonIgnore]
	public Property FirePercent{ get { return GetProperty("1100"); } }
	/// <summary>
	/// 火系抗性
	/// </summary>
	[JsonIgnore]
	public Property FireRes{ get { return GetProperty("1101"); } }
	/// <summary>
	/// 火系抗性上限
	/// </summary>
	[JsonIgnore]
	public Property FireResMax{ get { return GetProperty("1102"); } }
	/// <summary>
	/// 降低火系抗性
	/// </summary>
	[JsonIgnore]
	public Property ReduceFireRes{ get { return GetProperty("1103"); } }
	/// <summary>
	/// 土系伤害百分比
	/// </summary>
	[JsonIgnore]
	public Property EarthPercent{ get { return GetProperty("1200"); } }
	/// <summary>
	/// 土系抗性
	/// </summary>
	[JsonIgnore]
	public Property EarthRes{ get { return GetProperty("1201"); } }
	/// <summary>
	/// 土系抗性上限
	/// </summary>
	[JsonIgnore]
	public Property EarthResMax{ get { return GetProperty("1202"); } }
	/// <summary>
	/// 降低土系抗性
	/// </summary>
	[JsonIgnore]
	public Property ReduceEarthRes{ get { return GetProperty("1203"); } }
	
	public override void InitPropertyList(Enum_PropertyInitType p_Type = Enum_PropertyInitType.Zero)
	{
		PropertyList ??= new();
		{
			m_InitDefault("1" , Property.New("1", p_Type));
			m_InitDefault("2" , Property.New("2", p_Type));
			m_InitDefault("10" , Property.New("10", p_Type));
			m_InitDefault("11" , Property.New("11", p_Type));
			m_InitDefault("100" , Property.New("100", p_Type));
			m_InitDefault("101" , Property.New("101", p_Type));
			m_InitDefault("103" , Property.New("103", p_Type));
			m_InitDefault("104" , Property.New("104", p_Type));
			m_InitDefault("105" , Property.New("105", p_Type));
			m_InitDefault("106" , Property.New("106", p_Type));
			m_InitDefault("107" , Property.New("107", p_Type));
			m_InitDefault("200" , Property.New("200", p_Type));
			m_InitDefault("201" , Property.New("201", p_Type));
			m_InitDefault("202" , Property.New("202", p_Type));
			m_InitDefault("203" , Property.New("203", p_Type));
			m_InitDefault("300" , Property.New("300", p_Type));
			m_InitDefault("301" , Property.New("301", p_Type));
			m_InitDefault("400" , Property.New("400", p_Type));
			m_InitDefault("401" , Property.New("401", p_Type));
			m_InitDefault("402" , Property.New("402", p_Type));
			m_InitDefault("500" , Property.New("500", p_Type));
			m_InitDefault("501" , Property.New("501", p_Type));
			m_InitDefault("502" , Property.New("502", p_Type));
			m_InitDefault("503" , Property.New("503", p_Type));
			m_InitDefault("508" , Property.New("508", p_Type));
			m_InitDefault("509" , Property.New("509", p_Type));
			m_InitDefault("510" , Property.New("510", p_Type));
			m_InitDefault("511" , Property.New("511", p_Type));
			m_InitDefault("600" , Property.New("600", p_Type));
			m_InitDefault("601" , Property.New("601", p_Type));
			m_InitDefault("602" , Property.New("602", p_Type));
			m_InitDefault("603" , Property.New("603", p_Type));
			m_InitDefault("700" , Property.New("700", p_Type));
			m_InitDefault("701" , Property.New("701", p_Type));
			m_InitDefault("702" , Property.New("702", p_Type));
			m_InitDefault("703" , Property.New("703", p_Type));
			m_InitDefault("800" , Property.New("800", p_Type));
			m_InitDefault("801" , Property.New("801", p_Type));
			m_InitDefault("802" , Property.New("802", p_Type));
			m_InitDefault("803" , Property.New("803", p_Type));
			m_InitDefault("900" , Property.New("900", p_Type));
			m_InitDefault("901" , Property.New("901", p_Type));
			m_InitDefault("902" , Property.New("902", p_Type));
			m_InitDefault("903" , Property.New("903", p_Type));
			m_InitDefault("1000" , Property.New("1000", p_Type));
			m_InitDefault("1001" , Property.New("1001", p_Type));
			m_InitDefault("1002" , Property.New("1002", p_Type));
			m_InitDefault("1003" , Property.New("1003", p_Type));
			m_InitDefault("1100" , Property.New("1100", p_Type));
			m_InitDefault("1101" , Property.New("1101", p_Type));
			m_InitDefault("1102" , Property.New("1102", p_Type));
			m_InitDefault("1103" , Property.New("1103", p_Type));
			m_InitDefault("1200" , Property.New("1200", p_Type));
			m_InitDefault("1201" , Property.New("1201", p_Type));
			m_InitDefault("1202" , Property.New("1202", p_Type));
			m_InitDefault("1203" , Property.New("1203", p_Type));
		}
		
		InitData();
		InitEvent();
	}
}
