// 以下内容为工具生成，请勿自行修改

/// <summary>
/// 属性超限处理，增加/减少/设置属性时，超过上下限的处理方式
/// </summary>
public enum Enum_PropertyLimitSet
{
	/// <summary>
	/// 成功，可超过限制
	/// </summary>
	Success = 0,
	/// <summary>
	/// 失败，无法增加/减少/设置属性，返回错误码
	/// </summary>
	Faild = 1,
	/// <summary>
	/// 将属性值设置为限制值
	/// </summary>
	SetToLimit = 2
}
