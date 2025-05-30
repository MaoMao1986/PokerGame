using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class JsonHelper
{
    /// <summary>
    /// 属性选项
    /// </summary>
    public enum PropertyOption
    {
        /// <summary>
        /// 包含/展示 某些属性
        /// </summary>
        Include,
        /// <summary>
        /// 排除/隐藏 某些属性
        /// </summary>
        Exclude
    }

    /// <summary>
    /// 属性展示设置
    /// 设置包含某个属性或排除某个属性
    /// </summary>
    public class PropertyDisplayResolver : DefaultContractResolver
    {
        private readonly PropertyOption _propertyOption;
        private readonly IEnumerable<string> _propertyNames;

        /// <summary>
        /// 初始化实例
        /// </summary>
        /// <param name="propertyOption">属性选项：Include包含/Exclude排除</param>
        /// <param name="propertyNames">属性名称列表</param>
        public PropertyDisplayResolver(PropertyOption propertyOption, IEnumerable<string> propertyNames)
        {
            _propertyOption = propertyOption;
            _propertyNames = propertyNames;
        }

        /// <summary>
        /// 初始化实例
        /// </summary>
        /// <param name="propertyOption">属性选项：Include包含/Exclude排除</param>
        /// <param name="propertyNames">属性名称</param>
        public PropertyDisplayResolver(PropertyOption propertyOption, params string[] propertyNames)
        {
            _propertyOption = propertyOption;
            _propertyNames = propertyNames;
        }

        /// <summary>
        /// 重写 DefaultContractResolver 里的 CreateProperties创建属性方法
        /// </summary>
        /// <param name="type">The type to create properties for.</param>
        /// <param name="memberSerialization">The member serialization mode for the type.</param>
        /// <returns>Properties for the given <see cref="T:Newtonsoft.Json.Serialization.JsonContract" />.</returns>
        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            var properties = base.CreateProperties(type, memberSerialization).ToList();
            //此处进行筛选
            return _propertyOption == PropertyOption.Include ? properties.FindAll(p => _propertyNames.Contains(p.PropertyName))
                : properties.FindAll(p => !_propertyNames.Contains(p.PropertyName));
        }

    }

    /// <summary>
    /// json文件格式化
    /// </summary>
    /// <param name="p_Object"></param>
    /// <returns></returns>
    public static string JsonFormat(string p_String)
    {
        JsonSerializer t_Serializer = new JsonSerializer();
        TextReader t_TextReader = new StringReader(p_String);
        JsonTextReader t_JsonReader = new JsonTextReader(t_TextReader);
        object t_Object = t_Serializer.Deserialize(t_JsonReader);
        if (t_Object != null)
        {
            StringWriter t_TextWriter = new StringWriter();
            JsonTextWriter t_JsonWriter = new JsonTextWriter(t_TextWriter)
            {
                Formatting = Formatting.Indented,
                Indentation = 1,
                IndentChar = '\t'
            };
            t_Serializer.Serialize(t_JsonWriter, t_Object);
            return t_TextWriter.ToString();
        }
        else
        {
            return p_String;
        }
    }

    /// <summary>
    /// json文件格式化
    /// </summary>
    /// <param name="p_Object"></param>
    /// <returns></returns>
    public static string JsonFormat(object p_Object)
    {
        if (p_Object != null)
        {
            JsonSerializer t_Serializer = new JsonSerializer();
            StringWriter t_TextWriter = new StringWriter();
            JsonTextWriter t_JsonWriter = new JsonTextWriter(t_TextWriter)
            {
                Formatting = Formatting.Indented,
                Indentation = 1,
                IndentChar = '\t'
            };
            t_Serializer.Serialize(t_JsonWriter, p_Object);
            return t_TextWriter.ToString();
        }
        else
        {
            return JsonConvert.SerializeObject(p_Object);
        }
    }

    public static string JsonFormat(object p_Object, PropertyOption p_Type, params string[] p_Array)
    {
        string t_JsonString = JsonConvert.SerializeObject(p_Object, new JsonSerializerSettings
        {
            ContractResolver = new PropertyDisplayResolver(p_Type, p_Array)
        });
        return JsonFormat(t_JsonString);
    }
}
