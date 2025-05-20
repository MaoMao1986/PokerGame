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
                Debug.LogError($"���ñ�{typeof(T).ToString()}������id:{p_Id}");
                return default(T);
            }
        }
        else
        {
            Debug.LogError($"���ñ�{typeof(T).ToString()}������");
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
            Debug.LogError($"���ñ�{typeof(T).ToString()}������");
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
            Debug.LogError($"���ñ�{typeof(T).ToString()}������");
            return null;
        }
    }

    private static void LoadData<T>(string p_Name) where T : IConfigRow, new()
    {
        string t_fullPath = Path.Combine(Application.dataPath, m_ConfigPath, p_Name);
        if (!File.Exists(t_fullPath)) 
        {
            Debug.LogError($"���ñ�·��{t_fullPath}������");
            return ;
        }
        if (TableExists<T>())
        {
            Debug.LogError($"���ñ�{t_fullPath}�Ѵ���");
            return;
        }
        string[] t_Lines = File.ReadAllLines(t_fullPath);
        if (t_Lines.Length < 4)
        {
            Debug.LogError($"���ñ�{t_fullPath}���ݲ������У�������Ч");
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
                Debug.LogError($"���ñ�{t_fullPath}��{t_RowCount}�����ֶκ��������ֶ�����һ��");
                return;
            }
            if (t_RowCount > 4)
            {
                T t_object = new T();
                t_object.ParseDataRow(t_Row, t_Type);
                if (t_Data.ContainsKey(t_object.Id))
                {
                    Debug.LogError($"���ñ�{t_fullPath}��{t_RowCount}��id�ظ�");
                    return;
                }
                t_Data.Add(t_object.Id, t_object);
            }
        }
        m_Data.Add(typeof(T), t_Data);
    }

    public static string TransToString(object p_Object)
    {
        return p_Object.ToString();
    }

    public static int TransToInt(object p_Object)
    {
        try
        {
            int t_Value = Convert.ToInt32(p_Object);
            return t_Value;
        }
        catch (Exception e) 
        {
            Debug.Log(e.ToString());
            Debug.LogError($"��ȡ����ʱTransToInt�����Ĵ���ֵ{p_Object}�޷�ת��Ϊint");
        }
        return -1;
    }

    public static T TransToEnum<T>(object p_Object) where T : Enum
    {
        try
        {
            int t_Value = Convert.ToInt32(p_Object);
            return (T)Enum.ToObject(typeof(T), t_Value);
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
            Debug.LogError($"��ȡ����ʱTransToEnum�����Ĵ���ֵ{p_Object}�޷�ת��Ϊ{typeof(T).ToString()}");
        }
        return default(T);
    }

    public static bool TransToBool(object p_Object)
    {
        try
        {
            bool t_Value = Convert.ToBoolean(p_Object);
            return t_Value;
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
            Debug.LogError($"��ȡ����ʱTransToBool�����Ĵ���ֵ{p_Object}�޷�ת��Ϊbool");
        }
        return false;
    }

    public static double TransToDouble(object p_Object)
    {
        try
        {
            double t_Value = Convert.ToDouble(p_Object);
            return t_Value;
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
            Debug.LogError($"��ȡ����ʱTransToDouble�����Ĵ���ֵ{p_Object}�޷�ת��Ϊdouble");
        }
        return -1;
    }

    public static string[] TransToStringArray(object p_Object)
    {
        return p_Object.ToString().Split(m_SplitChar1);
    }

    public static int[] TransToIntArray(object p_Object)
    {
        string[] t_Array = p_Object.ToString().Split(m_SplitChar1);
        int[] t_Int = new int[t_Array.Length];
        for (int i = 0; i < t_Array.Length; i++)
        {
            t_Int[i] = TransToInt(t_Array[i]);
        }
        return t_Int;
    }

    public static bool[] TransToBoolArray(object p_Object)
    {
        string[] t_Array = p_Object.ToString().Split(m_SplitChar1);
        bool[] t_Bool = new bool[t_Array.Length];
        for (int i = 0; i < t_Array.Length; i++)
        {
            t_Bool[i] = TransToBool(t_Array[i]);
        }
        return t_Bool;
    }

    public static double[] TransToDoubleArray(object p_Object)
    {
        string[] t_Array = p_Object.ToString().Split(m_SplitChar1);
        double[] t_Double = new double[t_Array.Length];
        for (int i = 0; i < t_Array.Length; i++)
        {
            t_Double[i] = TransToDouble(t_Array[i]);
        }
        return t_Double;
    }

    public static string[,] TransToStringArray2(object p_Object)
    {
        string[] t_Array = p_Object.ToString().Split(m_SplitChar1);
        int t_RowCount = t_Array.Length;
        string[] t_Column = t_Array[0].Split(m_SplitChar2);
        int t_ColumnCount = t_Column.Count();
        string[,] t_ReturnArray = new string[t_RowCount,t_ColumnCount];
        for (int i = 0; i < t_RowCount; i++) 
        { 
            string[] t_ColumnArray = t_Array[i].Split(m_SplitChar2);
            if (t_ColumnArray.Length != t_ColumnCount)
            {
                Debug.LogError($"���ô���ĳ�ֶ�����Ϊstring[][]���������������õ�{i + 1}�е��������͵�1�е������������");
                return null;
            }
            for (int j = 0; j < t_ColumnCount; j++) 
            {
                t_ReturnArray[i,j] = t_ColumnArray[j];
            }
        }
        return t_ReturnArray;
    }

    public static int[,] TransToIntArray2(object p_Object)
    {
        string[] t_Array = p_Object.ToString().Split(m_SplitChar1);
        int t_RowCount = t_Array.Length;
        string[] t_Column = t_Array[0].Split(m_SplitChar2);
        int t_ColumnCount = t_Column.Count();
        int[,] t_ReturnArray = new int[t_RowCount, t_ColumnCount];
        for (int i = 0; i < t_RowCount; i++)
        {
            string[] t_ColumnArray = t_Array[i].Split(m_SplitChar2);
            if (t_ColumnArray.Length != t_ColumnCount)
            {
                Debug.LogError($"���ô���ĳ�ֶ�����Ϊstring[][]���������������õ�{i + 1}�е��������͵�1�е������������");
                return null;
            }
            for (int j = 0; j < t_ColumnCount; j++)
            {
                t_ReturnArray[i, j] = TransToInt(t_ColumnArray[j]);
            }
        }
        return t_ReturnArray;
    }

    public static bool[,] TransToBoolArray2(object p_Object)
    {
        string[] t_Array = p_Object.ToString().Split(m_SplitChar1);
        int t_RowCount = t_Array.Length;
        string[] t_Column = t_Array[0].Split(m_SplitChar2);
        int t_ColumnCount = t_Column.Count();
        bool[,] t_ReturnArray = new bool[t_RowCount, t_ColumnCount];
        for (int i = 0; i < t_RowCount; i++)
        {
            string[] t_ColumnArray = t_Array[i].Split(m_SplitChar2);
            if (t_ColumnArray.Length != t_ColumnCount)
            {
                Debug.LogError($"���ô���ĳ�ֶ�����Ϊstring[][]���������������õ�{i + 1}�е��������͵�1�е������������");
                return null;
            }
            for (int j = 0; j < t_ColumnCount; j++)
            {
                t_ReturnArray[i, j] = TransToBool(t_ColumnArray[j]);
            }
        }
        return t_ReturnArray;
    }

    public static double[,] TransToDoubleArray2(object p_Object)
    {
        string[] t_Array = p_Object.ToString().Split(m_SplitChar1);
        int t_RowCount = t_Array.Length;
        string[] t_Column = t_Array[0].Split(m_SplitChar2);
        int t_ColumnCount = t_Column.Count();
        double[,] t_ReturnArray = new double[t_RowCount, t_ColumnCount];
        for (int i = 0; i < t_RowCount; i++)
        {
            string[] t_ColumnArray = t_Array[i].Split(m_SplitChar2);
            if (t_ColumnArray.Length != t_ColumnCount)
            {
                Debug.LogError($"���ô���ĳ�ֶ�����Ϊstring[][]���������������õ�{i + 1}�е��������͵�1�е������������");
                return null;
            }
            for (int j = 0; j < t_ColumnCount; j++)
            {
                t_ReturnArray[i, j] = TransToDouble(t_ColumnArray[j]);
            }
        }
        return t_ReturnArray;
    }
}
