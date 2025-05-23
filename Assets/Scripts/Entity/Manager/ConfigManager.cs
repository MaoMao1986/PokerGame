using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public static partial class ConfigManager
{
    private const string m_ConfigPath = "Table";
    private const char m_SplitString = '\t';
    private const char m_SplitChar1 = '|';
    private const char m_SplitChar2 = ':';
    private static Dictionary<Type, Dictionary<string, IConfigRow>> m_Data = new Dictionary<Type, Dictionary<string, IConfigRow>>();
    public static T GetRow<T>(string p_Id) where T : IConfigRow
    {
        if (TableExists<T>())
        {
            Dictionary<string, IConfigRow> t_Data = GetTable<T>();
            if (t_Data.ContainsKey(p_Id))
            {
                return (T)t_Data[p_Id];
            }
            else
            {
                Debug.LogError($"配置表{typeof(T).ToString()}不存在id:{p_Id}");
                return default(T);
            }
        }
        else
        {
            Debug.LogError($"配置表{typeof(T).ToString()}不存在");
            return default(T);
        }
    }

    public static List<string> GetIdList<T>()
    {
        if (TableExists<T>())
        {
            Dictionary<string, IConfigRow> t_Data = GetTable<T>();
            return t_Data.Keys.ToList();
        }
        else
        {
            Debug.LogError($"配置表{typeof(T).ToString()}不存在");
            return null;
        }
    }

    private static bool TableExists<T>()
    {
        if (m_Data.ContainsKey(typeof(T))) { return true; }
        return false;
    }

    private static Dictionary<string, IConfigRow> GetTable<T>()
    {
        if (TableExists<T>())
        {
            return m_Data[typeof(T)];
        }
        else
        {
            Debug.LogError($"配置表{typeof(T).ToString()}不存在");
            return null;
        }
    }

    private static void LoadData<T>(string p_Name) where T : IConfigRow, new()
    {
        string t_fullPath = Path.Combine(Application.dataPath, m_ConfigPath, p_Name);
        if (!File.Exists(t_fullPath)) 
        {
            Debug.LogError($"配置表路径{t_fullPath}不存在");
            return ;
        }
        if (TableExists<T>())
        {
            Debug.LogError($"配置表{t_fullPath}已存在");
            return;
        }
        string[] t_Lines = File.ReadAllLines(t_fullPath);
        if (t_Lines.Length < 4)
        {
            Debug.LogError($"配置表{t_fullPath}内容不足四行，配置无效");
            return;
        }
        Dictionary<string, IConfigRow> t_Data = new Dictionary<string, IConfigRow>();
        int t_RowCount = 0;
        int t_ColumnNum = t_Lines[0].Split(m_SplitString).Length;
        string[] t_Field = t_Lines[1].Split(m_SplitString);
        string[] t_Type = t_Lines[3].Split(m_SplitString);
        foreach (string t_Line in t_Lines)
        {
            t_RowCount++;
            string[] t_Row = t_Line.Split(m_SplitString);
            if(t_Row.Length != t_ColumnNum)
            {
                Debug.LogError($"配置表{t_fullPath}第{t_RowCount}行列字段和首行列字段数不一致");
                return;
            }
            if (t_RowCount > 4)
            {
                T t_object = new T();
                t_object.ParseDataRow(t_Row, t_Type);
                if (t_Data.ContainsKey(t_object.Id))
                {
                    Debug.LogError($"配置表{t_fullPath}第{t_RowCount}行id重复");
                    return;
                }
                t_Data.Add(t_object.Id, t_object);
            }
        }
        m_Data.Add(typeof(T), t_Data);
    }

    public static string TransToString(string p_String)
    {
        string t_Return = "";
        if(!string.IsNullOrEmpty(p_String))
        {
            t_Return = p_String.ToString();
        }
        return t_Return;
    }

    public static int TransToInt(string p_String)
    {
        try
        {
            int t_Value = 0;
            if (!string.IsNullOrEmpty(p_String))
            {
                t_Value = Convert.ToInt32(p_String);
            }
            return t_Value;
        }
        catch (Exception e) 
        {
            Debug.Log(e.ToString());
            Debug.LogError($"读取配置时TransToInt方法的传入值{p_String}无法转化为int");
        }
        return 0;
    }

    public static T TransToEnum<T>(string p_String) where T : Enum
    {
        try
        {
            T t_Enum = default(T);
            if(!string.IsNullOrEmpty(p_String))
            {
                int t_Value = Convert.ToInt32(p_String);
                t_Enum = (T)Enum.ToObject(typeof(T), t_Value);
            }
            return t_Enum;
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
            Debug.LogError($"读取配置时TransToEnum方法的传入值{p_String}无法转化为{typeof(T).ToString()}");
        }
        return default(T);
    }

    public static bool TransToBool(string p_String)
    {
        try
        {
            bool t_Value = false;
            if(!string.IsNullOrEmpty(p_String))
            {
                t_Value = Convert.ToBoolean(p_String);
            }
            return t_Value;
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
            Debug.LogError($"读取配置时TransToBool方法的传入值{p_String}无法转化为bool");
        }
        return false;
    }

    public static double TransToDouble(string p_String)
    {
        try
        {
            double t_Value = 0;
            if (!string.IsNullOrEmpty(p_String))
            {
                t_Value = Convert.ToDouble(p_String);
            }
            return t_Value;
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
            Debug.LogError($"读取配置时TransToDouble方法的传入值{p_String}无法转化为double");
        }
        return -1;
    }

    public static string[] TransToStringArray(string p_String)
    {
        string[] t_Array = Array.Empty<string>();
        if(!string.IsNullOrEmpty(p_String))
        {
            t_Array = p_String.ToString().Split(m_SplitChar1);
        }
        return t_Array;
    }

    public static int[] TransToIntArray(string p_String)
    {
        int[] t_Int = Array.Empty<int>();
        if(!string.IsNullOrEmpty(p_String))
        {
            string[] t_Array = p_String.ToString().Split(m_SplitChar1);
            t_Int = new int[t_Array.Length];
            for (int i = 0; i < t_Array.Length; i++)
            {
                t_Int[i] = TransToInt(t_Array[i]);
            }
        }
        return t_Int;
    }

    public static bool[] TransToBoolArray(string p_String)
    {
        bool[] t_Bool = Array.Empty<bool>();
        if(!string.IsNullOrEmpty(p_String))
        {
            string[] t_Array = p_String.ToString().Split(m_SplitChar1);
            t_Bool = new bool[t_Array.Length];
            for (int i = 0; i < t_Array.Length; i++)
            {
                t_Bool[i] = TransToBool(t_Array[i]);
            }
        }
        return t_Bool;
    }

    public static double[] TransToDoubleArray(string p_String)
    {
        double[] t_Double = Array.Empty<double>();
        if(!string.IsNullOrEmpty(p_String))
        {
            string[] t_Array = p_String.ToString().Split(m_SplitChar1);
            t_Double = new double[t_Array.Length];
            for (int i = 0; i < t_Array.Length; i++)
            {
                t_Double[i] = TransToDouble(t_Array[i]);
            }
        }
        return t_Double;
    }

    public static string[,] TransToStringArray2(string p_String)
    {
        string[,] t_ReturnArray = new string[0, 0];
        if(!string.IsNullOrEmpty(p_String))
        {
            string[] t_Array = p_String.ToString().Split(m_SplitChar1);
            int t_RowCount = t_Array.Length;
            if (!string.IsNullOrEmpty(t_Array[0]))
            {
                string[] t_Column = t_Array[0].Split(m_SplitChar2);
                int t_ColumnCount = t_Column.Count();
                t_ReturnArray = new string[t_RowCount, t_ColumnCount];
                for (int i = 0; i < t_RowCount; i++)
                {
                    string[] t_ColumnArray = t_Array[i].Split(m_SplitChar2);
                    if (t_ColumnArray.Length != t_ColumnCount)
                    {
                        Debug.LogError($"配置错误：某字段配置为string[][]，但其中数据配置第{i + 1}行的列数量和第1行的列数量不相等");
                        return null;
                    }
                    for (int j = 0; j < t_ColumnCount; j++)
                    {
                        t_ReturnArray[i, j] = t_ColumnArray[j];
                    }
                }
            }
        }
        return t_ReturnArray;
    }

    public static int[,] TransToIntArray2(string p_String)
    {
        int[,] t_ReturnArray = new int[0, 0];
        if(!string.IsNullOrEmpty(p_String))
        {
            string[] t_Array = p_String.ToString().Split(m_SplitChar1);
            int t_RowCount = t_Array.Length;
            if (!string.IsNullOrEmpty(t_Array[0]))
            {
                string[] t_Column = t_Array[0].Split(m_SplitChar2);
                int t_ColumnCount = t_Column.Count();
                t_ReturnArray = new int[t_RowCount, t_ColumnCount];
                for (int i = 0; i < t_RowCount; i++)
                {
                    string[] t_ColumnArray = t_Array[i].Split(m_SplitChar2);
                    if (t_ColumnArray.Length != t_ColumnCount)
                    {
                        Debug.LogError($"配置错误：某字段配置为string[][]，但其中数据配置第{i + 1}行的列数量和第1行的列数量不相等");
                        return null;
                    }
                    for (int j = 0; j < t_ColumnCount; j++)
                    {
                        t_ReturnArray[i, j] = TransToInt(t_ColumnArray[j]);
                    }
                }
            }
        }
        return t_ReturnArray;
    }

    public static bool[,] TransToBoolArray2(string p_String)
    {
        bool[,] t_ReturnArray = new bool[0, 0];
        if (!string.IsNullOrEmpty(p_String))
        {
            string[] t_Array = p_String.ToString().Split(m_SplitChar1);
            int t_RowCount = t_Array.Length;
            if (!string.IsNullOrEmpty(t_Array[0]))
            {
                string[] t_Column = t_Array[0].Split(m_SplitChar2);
                int t_ColumnCount = t_Column.Count();
                t_ReturnArray = new bool[t_RowCount, t_ColumnCount];
                for (int i = 0; i < t_RowCount; i++)
                {
                    string[] t_ColumnArray = t_Array[i].Split(m_SplitChar2);
                    if (t_ColumnArray.Length != t_ColumnCount)
                    {
                        Debug.LogError($"配置错误：某字段配置为string[][]，但其中数据配置第{i + 1}行的列数量和第1行的列数量不相等");
                        return null;
                    }
                    for (int j = 0; j < t_ColumnCount; j++)
                    {
                        t_ReturnArray[i, j] = TransToBool(t_ColumnArray[j]);
                    }
                }
            }
        }
        return t_ReturnArray;
    }

    public static double[,] TransToDoubleArray2(string p_String)
    {
        double[,] t_ReturnArray = new double[0, 0];
        if (!string.IsNullOrEmpty(p_String))
        {
            string[] t_Array = p_String.ToString().Split(m_SplitChar1);
            int t_RowCount = t_Array.Length;
            if (!string.IsNullOrEmpty(t_Array[0]))
            {
                string[] t_Column = t_Array[0].Split(m_SplitChar2);
                int t_ColumnCount = t_Column.Count();
                t_ReturnArray = new double[t_RowCount, t_ColumnCount];
                for (int i = 0; i < t_RowCount; i++)
                {
                    string[] t_ColumnArray = t_Array[i].Split(m_SplitChar2);
                    if (t_ColumnArray.Length != t_ColumnCount)
                    {
                        Debug.LogError($"配置错误：某字段配置为string[][]，但其中数据配置第{i + 1}行的列数量和第1行的列数量不相等");
                        return null;
                    }
                    for (int j = 0; j < t_ColumnCount; j++)
                    {
                        t_ReturnArray[i, j] = TransToDouble(t_ColumnArray[j]);
                    }
                }
            }
        }
        return t_ReturnArray;
    }
}
