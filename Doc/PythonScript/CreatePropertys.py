# coding=utf-8

#m_PythonScope.SetVariable("Project", ProjectController.ActiveProject);
#m_PythonScope.SetVariable("Log", LogController.GetInstance());
#m_PythonScope.SetVariable("Values", SystemValues.GetInstance());
#m_PythonScope.SetVariable("Sheets", ProjectSheets.GetInstance());
#m_PythonScope.SetVariable("DataOperate", DataOperateController.GetInstance());
#m_PythonScope.SetVariable("MapDataInfos", MapDataInfos.GetInstance());
#m_PythonScope.SetVariable("Reward", RewardController.GetInstance());


def CreatePropertyFile():
    t_MD_Property = MapDataInfos.ReadOnly("Property")
    
    m_CreatePropertyEnumFile(t_MD_Property)
    
    m_CreatePropertyUnitFile("战斗", t_MD_Property)
    m_CreatePropertyUnitFile("经济", t_MD_Property)
  
#生成属性枚举相关文件  
def m_CreatePropertyEnumFile(p_Md):       
    t_idList = p_Md.DataData.GetRowKeyList()
    #生成枚举文件
    propertyEnumCSPath = ProjectPath + "\\..\\..\\Assets\\Script\\Enum\\Auto_EnumProperty.cs"
    fileContent = ""
    fileContent += "/// <summary>\n"
    fileContent += "/// 属性枚举，工具自动生成，勿手动修改\n"
    fileContent += "/// </summary>\n"
    fileContent += "public enum EmProperty\n"
    fileContent += "{\n"
    for t_id in t_idList:
        fileContent += "\t" + str(p_Md.DataData.GetData(t_id,"枚举名称")) + " = " + str(p_Md.DataData.GetData(t_id,"id")) + ",\n"
    fileContent += "}\n"
    File.Write(propertyEnumCSPath,fileContent)
    Log.WriteSuccess("生成" + propertyEnumCSPath + "成功")    
    
#生成属性对象相关文件
def m_CreatePropertyUnitFile(p_Name, p_Md):
    t_Name = p_Name
    t_Type = ""
    if(p_Name == "战斗"):
        t_Type = "Battle"
    if(p_Name == "经济"):
        t_Type = "Economy"

    t_idList = p_Md.DataData.GetRowKeyList()
    #生成属性组的所有属性
    propertyUnitCSPath = ProjectPath + "\\..\\..\\Assets\\Script\\Logic\\Entity\\Propertys\\Auto_" + t_Type + "Propertys.cs"
    fileContent = "using System.Collections.Generic;\n\n\n"
    fileContent += "/// <summary>\n"
    fileContent += "/// " + t_Name + "属性列表，工具自动生成，勿手动修改\n"
    fileContent += "/// </summary>\n"
    fileContent += "public partial class " + t_Type + "Propertys\n"
    fileContent += "{\n"
    for t_id in t_idList:
        if p_Md.DataData.GetData(t_id,"类型") == t_Name:
            fileContent += "\t/// <summary>\n"
            fileContent += "\t/// " + str(p_Md.DataData.GetData(t_id,"显示名称")) + "\n"
            fileContent += "\t/// </summary>\n"
            className = ""
            if str(p_Md.DataData.GetData(t_id,"子类型")) == "上限":
                className = "LimitedProperty"
            else:
                className = "Property"
            fileContent += "\tpublic " + className + " " + str(p_Md.DataData.GetData(t_id,"枚举名称")) + " { get; set; } = " + className + ".New(" + t_id + ");\n"
    
    #枚举和属性对应的字典
    fileContent += "\t\n"
    fileContent += "\tpublic override void InitPropertyList()\n"
    fileContent += "\t{\n"
    fileContent += "\t\tPropertyList = new()\n"
    fileContent += "\t\t{\n"
    for t_id in t_idList:
        if p_Md.DataData.GetData(t_id,"类型") == t_Name:
            fileContent += "\t\t\t{" + t_id + " , " + str(p_Md.DataData.GetData(t_id,"枚举名称")) + "},\n"
    fileContent += "\t\t};\n"
    fileContent += "\t}\n"
                
    fileContent += "}\n"
    File.Write(propertyUnitCSPath,fileContent)
    Log.WriteSuccess("生成" + propertyUnitCSPath + "成功")