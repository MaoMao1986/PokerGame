using System.Collections;
using UnityEngine;

public delegate void DataChangedEventHandler();

public interface ISaveDataBase
{
    public void InitData();
    public void InitEvent();
    public DataChangedEventHandler DataChangedEvent { get; set; }
}

public interface ISaveData : ISaveDataBase
{
    public string FileName { get; set; }
    public void LoadFromJson();
    public void SaveToJson();
}