// 以下内容为工具生成，请勿自行修改

/// <summary>
/// 关卡结算条件类型，胜利条件/失败条件
/// </summary>
public enum Enum_LevelEndType
{
	/// <summary>
	/// 胜利
	/// </summary>
	Success = 0,
	/// <summary>
	/// 失败
	/// </summary>
	Faild = 1
}

/// <summary>
/// 关卡胜利类型，默认为敌方全部死亡，其他待扩展
/// </summary>
public enum Enum_LevelSuccessType
{
	/// <summary>
	/// 击杀所有敌人
	/// </summary>
	KillAllMonster = 0
}

/// <summary>
/// 关卡失败类型，默认为己方全部阵亡，其他待扩展
/// </summary>
public enum Enum_LevelFaildType
{
	/// <summary>
	/// 己方全部阵亡
	/// </summary>
	SelfAllDead = 0
}
