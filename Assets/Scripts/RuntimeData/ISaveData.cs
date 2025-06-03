using System.Collections;
using UnityEngine;

public interface ISaveDataBase
{
    public void InitData();
    public void InitEvent();
    public delegate void DataChangedEventHandler();
    public DataChangedEventHandler DataChangedEvent { get; set; }
}

public interface ISaveData : ISaveDataBase
{
    public void LoadFromJson();
    public void SaveToJson();
}