using UnityEngine;

public abstract class Character : MonoBehaviour 
{
    public int Atk { get; set;}
    public int Def {  get; set;}
    public int Hp {  get; set;}


    /// <summary>
    /// 初始化数据
    /// </summary>
    public abstract void InitData();
}
