using System.Collections.Generic;


/// <summary>
/// 战斗属性列表，工具自动生成，勿手动修改
/// </summary>
public partial class BattlePropertys
{
	/// <summary>
	/// 生命
	/// </summary>
	public Property Hp { get; set; } = Property.New(1);
	/// <summary>
	/// 法力
	/// </summary>
	public Property Mp { get; set; } = Property.New(2);
	/// <summary>
	/// 最小攻击
	/// </summary>
	public Property AtkMin { get; set; } = Property.New(100);
	/// <summary>
	/// 最大攻击
	/// </summary>
	public Property AtkMax { get; set; } = Property.New(101);
	/// <summary>
	/// 攻击
	/// </summary>
	public Property Akt { get; set; } = Property.New(102);
	/// <summary>
	/// 防御
	/// </summary>
	public Property Def { get; set; } = Property.New(103);
	/// <summary>
	/// 坚韧概率
	/// </summary>
	public LimitedProperty DefMultPro { get; set; } = LimitedProperty.New(104);
	/// <summary>
	/// 坚韧倍率
	/// </summary>
	public Property DefMult { get; set; } = Property.New(105);
	/// <summary>
	/// 致命一击
	/// </summary>
	public LimitedProperty FatalAtk { get; set; } = LimitedProperty.New(106);
	/// <summary>
	/// 出手速度
	/// </summary>
	public Property AttackSpeed { get; set; } = Property.New(107);
	/// <summary>
	/// 增加伤害
	/// </summary>
	public Property DmgAdd { get; set; } = Property.New(200);
	/// <summary>
	/// 减少伤害
	/// </summary>
	public Property DmgReduce { get; set; } = Property.New(201);
	/// <summary>
	/// 增加伤害百分比
	/// </summary>
	public Property DmgAddPercent { get; set; } = Property.New(202);
	/// <summary>
	/// 减少伤害百分比
	/// </summary>
	public Property DmgReducePercent { get; set; } = Property.New(203);
	/// <summary>
	/// 命中
	/// </summary>
	public Property Hit { get; set; } = Property.New(300);
	/// <summary>
	/// 闪避
	/// </summary>
	public Property Dodge { get; set; } = Property.New(301);
	/// <summary>
	/// 暴击
	/// </summary>
	public Property Critical { get; set; } = Property.New(400);
	/// <summary>
	/// 韧性
	/// </summary>
	public Property Tough { get; set; } = Property.New(401);
	/// <summary>
	/// 暴击倍率
	/// </summary>
	public Property CriticalMult { get; set; } = Property.New(402);
	/// <summary>
	/// 生命恢复速度
	/// </summary>
	public Property HpRestore { get; set; } = Property.New(500);
	/// <summary>
	/// 生命恢复速度百分比
	/// </summary>
	public Property HpRestorePercent { get; set; } = Property.New(501);
	/// <summary>
	/// 降低生命恢复速度
	/// </summary>
	public Property ReduceHpRestore { get; set; } = Property.New(502);
	/// <summary>
	/// 降低生命恢复速度百分比
	/// </summary>
	public Property ReduceHpRestorePercent { get; set; } = Property.New(503);
	/// <summary>
	/// 法力恢复速度
	/// </summary>
	public Property MpRestore { get; set; } = Property.New(508);
	/// <summary>
	/// 法力恢复速度百分比
	/// </summary>
	public Property MpRestorePercent { get; set; } = Property.New(509);
	/// <summary>
	/// 降低法力恢复速度
	/// </summary>
	public Property ReduceMpRestore { get; set; } = Property.New(510);
	/// <summary>
	/// 降低法力恢复速度百分比
	/// </summary>
	public Property ReduceMpRestorePercent { get; set; } = Property.New(511);
	/// <summary>
	/// 降低法力消耗
	/// </summary>
	public Property ReduceMpCost { get; set; } = Property.New(600);
	/// <summary>
	/// 降低法力消耗百分比
	/// </summary>
	public Property ReduceMpCostPercent { get; set; } = Property.New(601);
	/// <summary>
	/// 增加法力消耗
	/// </summary>
	public Property AddMpCost { get; set; } = Property.New(602);
	/// <summary>
	/// 增加法力消耗百分比
	/// </summary>
	public Property AddMpCostPercent { get; set; } = Property.New(603);
	/// <summary>
	/// 物理伤害百分比
	/// </summary>
	public Property PhyPercent { get; set; } = Property.New(700);
	/// <summary>
	/// 物理抗性
	/// </summary>
	public LimitedProperty PhyRes { get; set; } = LimitedProperty.New(701);
	/// <summary>
	/// 物理抗性上限
	/// </summary>
	public LimitedProperty PhyResMax { get; set; } = LimitedProperty.New(702);
	/// <summary>
	/// 降低敌人物理抗性
	/// </summary>
	public Property ReducePhyRes { get; set; } = Property.New(703);
	/// <summary>
	/// 金系伤害百分比
	/// </summary>
	public Property GoldPercent { get; set; } = Property.New(800);
	/// <summary>
	/// 金系抗性
	/// </summary>
	public LimitedProperty GoldRes { get; set; } = LimitedProperty.New(801);
	/// <summary>
	/// 金系抗性上限
	/// </summary>
	public LimitedProperty GoldResMax { get; set; } = LimitedProperty.New(802);
	/// <summary>
	/// 降低金系抗性
	/// </summary>
	public Property ReduceGoldRes { get; set; } = Property.New(803);
	/// <summary>
	/// 木系伤害百分比
	/// </summary>
	public Property WoodPercent { get; set; } = Property.New(900);
	/// <summary>
	/// 木系抗性
	/// </summary>
	public LimitedProperty WoodRes { get; set; } = LimitedProperty.New(901);
	/// <summary>
	/// 木系抗性上限
	/// </summary>
	public LimitedProperty WoodResMax { get; set; } = LimitedProperty.New(902);
	/// <summary>
	/// 降低木系抗性
	/// </summary>
	public Property ReduceWoodRes { get; set; } = Property.New(903);
	/// <summary>
	/// 水系伤害百分比
	/// </summary>
	public Property WaterPercent { get; set; } = Property.New(1000);
	/// <summary>
	/// 水系抗性
	/// </summary>
	public LimitedProperty WaterRes { get; set; } = LimitedProperty.New(1001);
	/// <summary>
	/// 水系抗性上限
	/// </summary>
	public LimitedProperty WaterResMax { get; set; } = LimitedProperty.New(1002);
	/// <summary>
	/// 降低水系抗性
	/// </summary>
	public Property ReduceWaterRes { get; set; } = Property.New(1003);
	/// <summary>
	/// 火系伤害百分比
	/// </summary>
	public Property FirePercent { get; set; } = Property.New(1100);
	/// <summary>
	/// 火系抗性
	/// </summary>
	public LimitedProperty FireRes { get; set; } = LimitedProperty.New(1101);
	/// <summary>
	/// 火系抗性上限
	/// </summary>
	public LimitedProperty FireResMax { get; set; } = LimitedProperty.New(1102);
	/// <summary>
	/// 降低火系抗性
	/// </summary>
	public Property ReduceFireRes { get; set; } = Property.New(1103);
	/// <summary>
	/// 土系伤害百分比
	/// </summary>
	public Property EarthPercent { get; set; } = Property.New(1200);
	/// <summary>
	/// 土系抗性
	/// </summary>
	public LimitedProperty EarthRes { get; set; } = LimitedProperty.New(1201);
	/// <summary>
	/// 土系抗性上限
	/// </summary>
	public LimitedProperty EarthResMax { get; set; } = LimitedProperty.New(1202);
	/// <summary>
	/// 降低土系抗性
	/// </summary>
	public Property ReduceEarthRes { get; set; } = Property.New(1203);
	
	public override void InitPropertyList()
	{
		PropertyList = new()
		{
			{1 , Hp},
			{2 , Mp},
			{100 , AtkMin},
			{101 , AtkMax},
			{102 , Akt},
			{103 , Def},
			{104 , DefMultPro},
			{105 , DefMult},
			{106 , FatalAtk},
			{107 , AttackSpeed},
			{200 , DmgAdd},
			{201 , DmgReduce},
			{202 , DmgAddPercent},
			{203 , DmgReducePercent},
			{300 , Hit},
			{301 , Dodge},
			{400 , Critical},
			{401 , Tough},
			{402 , CriticalMult},
			{500 , HpRestore},
			{501 , HpRestorePercent},
			{502 , ReduceHpRestore},
			{503 , ReduceHpRestorePercent},
			{508 , MpRestore},
			{509 , MpRestorePercent},
			{510 , ReduceMpRestore},
			{511 , ReduceMpRestorePercent},
			{600 , ReduceMpCost},
			{601 , ReduceMpCostPercent},
			{602 , AddMpCost},
			{603 , AddMpCostPercent},
			{700 , PhyPercent},
			{701 , PhyRes},
			{702 , PhyResMax},
			{703 , ReducePhyRes},
			{800 , GoldPercent},
			{801 , GoldRes},
			{802 , GoldResMax},
			{803 , ReduceGoldRes},
			{900 , WoodPercent},
			{901 , WoodRes},
			{902 , WoodResMax},
			{903 , ReduceWoodRes},
			{1000 , WaterPercent},
			{1001 , WaterRes},
			{1002 , WaterResMax},
			{1003 , ReduceWaterRes},
			{1100 , FirePercent},
			{1101 , FireRes},
			{1102 , FireResMax},
			{1103 , ReduceFireRes},
			{1200 , EarthPercent},
			{1201 , EarthRes},
			{1202 , EarthResMax},
			{1203 , ReduceEarthRes},
		};
	}
}
