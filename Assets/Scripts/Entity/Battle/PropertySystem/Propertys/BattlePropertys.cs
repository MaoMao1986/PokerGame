using System.Collections.Generic;


/// <summary>
/// Battle属性组代码逻辑
/// 可以参与战斗的单位身上的属性组，主要是养成结果
/// </summary>
public partial class BattlePropertys : Propertys
{
	public override void InitAllProperty()
	{
		// 初始化属性字典
		InitPropertyList();
		
		// 待实现，各个属性的事件回调
		//PhyRes.GetMaxFunction = () =>
		//{
		//	return PhyRes.GetConfigMax() + PhyResMax.GetValid();
		//};
		
	}
}
