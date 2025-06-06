using Newtonsoft.Json;
using System.Text;
using UnityEngine;

public static class RuntimeData
{
    public static DU_Player Player { get; set; }
    public static BattleManager BattleManager { get; set; }

    public static void LoadAll()
    {
        Player = new(); Player.LoadFromJson();
    }

    /// <summary>
    /// 读取数据
    /// </summary>
    /// <param name="p_Path"></param>
    /// <returns></returns>
    public static void Load<T>(this T p_Data, string p_Name) where T: UnitBase, ISaveData, new()
    {
        string t_Path = GetSaveDataPath(p_Name);
        //文件路径不存在则跳出
        if (!System.IO.File.Exists(t_Path))
        {
            p_Data.Init();
            p_Data.SaveToJson();
        }
        else
        {
            JsonConvert.PopulateObject(System.IO.File.ReadAllText(t_Path, Encoding.Default), p_Data);
            p_Data.Init();
        }
    }

    /// <summary>
    /// 保存数据
    /// </summary>
    public static void Save<T>(T p_Object, string p_Name)where T : ISaveData
    {
        string t_Path = GetSaveDataPath(p_Name);
        if (!System.IO.File.Exists(t_Path))
        {
            System.IO.File.Create(t_Path).Close();
        }
        System.IO.File.WriteAllText(t_Path, JsonHelper.JsonFormat(p_Object), Encoding.UTF8);
    }

    public static string GetSaveDataPath(string p_Name)
    {
        string t_RelativePath = $"LocalData/{p_Name}.json";
        string t_FullPath = System.IO.Path.Combine(Application.dataPath, t_RelativePath);
        return t_FullPath;
    }
}
