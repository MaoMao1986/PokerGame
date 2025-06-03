using System.Collections.Generic;


/// <summary>
/// Battle属性列表，工具自动生成，勿手动修改
/// 可以参与战斗的单位身上的属性组，主要是养成结果
/// </summary>
public partial class BattlePros
{
	/// <summary>
	/// 生命
	/// </summary>
	public Property Hp { get; set; } = new();
	/// <summary>
	/// 法力
	/// </summary>
	public Property Mp { get; set; } = new();
	/// <summary>
	/// 最小攻击
	/// </summary>
	public Property AtkMin { get; set; } = new();
	/// <summary>
	/// 最大攻击
	/// </summary>
	public Property AtkMax { get; set; } = new();
	/// <summary>
	/// 防御
	/// </summary>
	public Property Def { get; set; } = new();
	/// <summary>
	/// 坚韧概率
	/// </summary>
	public Property DefMultPro { get; set; } = new();
	/// <summary>
	/// 坚韧倍率
	/// </summary>
	public Property DefMult { get; set; } = new();
	/// <summary>
	/// 致命一击
	/// </summary>
	public Property FatalAtk { get; set; } = new();
	/// <summary>
	/// 出手速度
	/// </summary>
	public Property AttackSpeed { get; set; } = new();
	/// <summary>
	/// 增加伤害
	/// </summary>
	public Property DmgAdd { get; set; } = new();
	/// <summary>
	/// 减少伤害
	/// </summary>
	public Property DmgReduce { get; set; } = new();
	/// <summary>
	/// 增加伤害百分比
	/// </summary>
	public Property DmgAddPercent { get; set; } = new();
	/// <summary>
	/// 减少伤害百分比
	/// </summary>
	public Property DmgReducePercent { get; set; } = new();
	/// <summary>
	/// 命中
	/// </summary>
	public Property Hit { get; set; } = new();
	/// <summary>
	/// 闪避
	/// </summary>
	public Property Dodge { get; set; } = new();
	/// <summary>
	/// 暴击
	/// </summary>
	public Property Critical { get; set; } = new();
	/// <summary>
	/// 韧性
	/// </summary>
	public Property Tough { get; set; } = new();
	/// <summary>
	/// 暴击倍率
	/// </summary>
	public Property CriticalMult { get; set; } = new();
	/// <summary>
	/// 生命恢复速度
	/// </summary>
	public Property HpRestore { get; set; } = new();
	/// <summary>
	/// 生命恢复速度百分比
	/// </summary>
	public Property HpRestorePercent { get; set; } = new();
	/// <summary>
	/// 降低生命恢复速度
	/// </summary>
	public Property ReduceHpRestore { get; set; } = new();
	/// <summary>
	/// 降低生命恢复速度百分比
	/// </summary>
	public Property ReduceHpRestorePercent { get; set; } = new();
	/// <summary>
	/// 法力恢复速度
	/// </summary>
	public Property MpRestore { get; set; } = new();
	/// <summary>
	/// 法力恢复速度百分比
	/// </summary>
	public Property MpRestorePercent { get; set; } = new();
	/// <summary>
	/// 降低法力恢复速度
	/// </summary>
	public Property ReduceMpRestore { get; set; } = new();
	/// <summary>
	/// 降低法力恢复速度百分比
	/// </summary>
	public Property ReduceMpRestorePercent { get; set; } = new();
	/// <summary>
	/// 降低法力消耗
	/// </summary>
	public Property ReduceMpCost { get; set; } = new();
	/// <summary>
	/// 降低法力消耗百分比
	/// </summary>
	public Property ReduceMpCostPercent { get; set; } = new();
	/// <summary>
	/// 增加法力消耗
	/// </summary>
	public Property AddMpCost { get; set; } = new();
	/// <summary>
	/// 增加法力消耗百分比
	/// </summary>
	public Property AddMpCostPercent { get; set; } = new();
	/// <summary>
	/// 物理伤害百分比
	/// </summary>
	public Property PhyPercent { get; set; } = new();
	/// <summary>
	/// 物理抗性
	/// </summary>
	public Property PhyRes { get; set; } = new();
	/// <summary>
	/// 物理抗性上限
	/// </summary>
	public Property PhyResMax { get; set; } = new();
	/// <summary>
	/// 降低敌人物理抗性
	/// </summary>
	public Property ReducePhyRes { get; set; } = new();
	/// <summary>
	/// 金系伤害百分比
	/// </summary>
	public Property GoldPercent { get; set; } = new();
	/// <summary>
	/// 金系抗性
	/// </summary>
	public Property GoldRes { get; set; } = new();
	/// <summary>
	/// 金系抗性上限
	/// </summary>
	public Property GoldResMax { get; set; } = new();
	/// <summary>
	/// 降低金系抗性
	/// </summary>
	public Property ReduceGoldRes { get; set; } = new();
	/// <summary>
	/// 木系伤害百分比
	/// </summary>
	public Property WoodPercent { get; set; } = new();
	/// <summary>
	/// 木系抗性
	/// </summary>
	public Property WoodRes { get; set; } = new();
	/// <summary>
	/// 木系抗性上限
	/// </summary>
	public Property WoodResMax { get; set; } = new();
	/// <summary>
	/// 降低木系抗性
	/// </summary>
	public Property ReduceWoodRes { get; set; } = new();
	/// <summary>
	/// 水系伤害百分比
	/// </summary>
	public Property WaterPercent { get; set; } = new();
	/// <summary>
	/// 水系抗性
	/// </summary>
	public Property WaterRes { get; set; } = new();
	/// <summary>
	/// 水系抗性上限
	/// </summary>
	public Property WaterResMax { get; set; } = new();
	/// <summary>
	/// 降低水系抗性
	/// </summary>
	public Property ReduceWaterRes { get; set; } = new();
	/// <summary>
	/// 火系伤害百分比
	/// </summary>
	public Property FirePercent { get; set; } = new();
	/// <summary>
	/// 火系抗性
	/// </summary>
	public Property FireRes { get; set; } = new();
	/// <summary>
	/// 火系抗性上限
	/// </summary>
	public Property FireResMax { get; set; } = new();
	/// <summary>
	/// 降低火系抗性
	/// </summary>
	public Property ReduceFireRes { get; set; } = new();
	/// <summary>
	/// 土系伤害百分比
	/// </summary>
	public Property EarthPercent { get; set; } = new();
	/// <summary>
	/// 土系抗性
	/// </summary>
	public Property EarthRes { get; set; } = new();
	/// <summary>
	/// 土系抗性上限
	/// </summary>
	public Property EarthResMax { get; set; } = new();
	/// <summary>
	/// 降低土系抗性
	/// </summary>
	public Property ReduceEarthRes { get; set; } = new();
	
	public override void InitPropertyList()
	{
		m_PropertyList = new()
		{
			{"1" , Hp},
			{"2" , Mp},
			{"100" , AtkMin},
			{"101" , AtkMax},
			{"103" , Def},
			{"104" , DefMultPro},
			{"105" , DefMult},
			{"106" , FatalAtk},
			{"107" , AttackSpeed},
			{"200" , DmgAdd},
			{"201" , DmgReduce},
			{"202" , DmgAddPercent},
			{"203" , DmgReducePercent},
			{"300" , Hit},
			{"301" , Dodge},
			{"400" , Critical},
			{"401" , Tough},
			{"402" , CriticalMult},
			{"500" , HpRestore},
			{"501" , HpRestorePercent},
			{"502" , ReduceHpRestore},
			{"503" , ReduceHpRestorePercent},
			{"508" , MpRestore},
			{"509" , MpRestorePercent},
			{"510" , ReduceMpRestore},
			{"511" , ReduceMpRestorePercent},
			{"600" , ReduceMpCost},
			{"601" , ReduceMpCostPercent},
			{"602" , AddMpCost},
			{"603" , AddMpCostPercent},
			{"700" , PhyPercent},
			{"701" , PhyRes},
			{"702" , PhyResMax},
			{"703" , ReducePhyRes},
			{"800" , GoldPercent},
			{"801" , GoldRes},
			{"802" , GoldResMax},
			{"803" , ReduceGoldRes},
			{"900" , WoodPercent},
			{"901" , WoodRes},
			{"902" , WoodResMax},
			{"903" , ReduceWoodRes},
			{"1000" , WaterPercent},
			{"1001" , WaterRes},
			{"1002" , WaterResMax},
			{"1003" , ReduceWaterRes},
			{"1100" , FirePercent},
			{"1101" , FireRes},
			{"1102" , FireResMax},
			{"1103" , ReduceFireRes},
			{"1200" , EarthPercent},
			{"1201" , EarthRes},
			{"1202" , EarthResMax},
			{"1203" , ReduceEarthRes},
		};
	}
}
