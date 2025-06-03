# coding=utf-8

#m_PythonScope.SetVariable("Project", ProjectController.ActiveProject);
#m_PythonScope.SetVariable("Log", LogController.GetInstance());
#m_PythonScope.SetVariable("Values", SystemValues.GetInstance());
#m_PythonScope.SetVariable("Sheets", ProjectSheets.GetInstance());
#m_PythonScope.SetVariable("DataOperate", DataOperateController.GetInstance());
#m_PythonScope.SetVariable("MapDataInfos", MapDataInfos.GetInstance());
#m_PythonScope.SetVariable("Reward", RewardController.GetInstance());


def CreatePropertyFile(p_Array):
    
    t_MD_PropertyAssist = MapDataInfos.ReadOnly("辅助_属性分组")
    t_MD_Property = MapDataInfos.ReadOnly("Property")
    
    for t_id in t_MD_PropertyAssist.DataData.GetRowKeyList():
        t_DataGroupString = t_MD_PropertyAssist.DataData.GetData(t_id,"数据分组")
        t_FileName = t_MD_PropertyAssist.DataData.GetData(t_id,"文件名")
        t_ClassName = t_MD_PropertyAssist.DataData.GetData(t_id,"类名")
        t_FileDesc = t_MD_PropertyAssist.DataData.GetData(t_id,"说明")
        t_DataGroupList = t_MD_Property.DataData.GetRowListByGroup(DataTrans.ListString(t_DataGroupString.split("|")))
        m_CreatePropertyUnitFile(t_ClassName, t_FileName, t_DataGroupList, t_FileDesc)
        m_CreatePropertyCodeFile(t_ClassName, t_FileName, t_FileDesc)
  
#生成属性组中代码继承的文件（如果文件不存在则生成，如果存在，则跳过）
def m_CreatePropertyCodeFile(p_ClassName, p_FileName, p_FileDesc):
    propertyUnitCSPath = ProjectPath + "\\..\\..\\Assets\\Scripts\\Entity\\Battle\\PropertySystem\\Propertys\\" + p_FileName + "Pros.cs"
    if not File.Exists(propertyUnitCSPath):
        fileContent = "using System.Collections.Generic;\n\n\n"
        fileContent += "/// <summary>\n"
        fileContent += "/// " + p_ClassName + "属性组代码逻辑\n"
        fileContent += "/// " + p_FileDesc + "\n"
        fileContent += "/// </summary>\n"
        fileContent += "public partial class " + p_ClassName + "Pros : Propertys\n"
        fileContent += "{\n"
        
        fileContent += "\t/// <summary>\n"
        fileContent += "\t/// 初始化属性值（需要特殊处理的，其他的要么按照配置初始化，要么按照其他属性的数值初始化）\n"
        fileContent += "\t/// 例如【当前生命】的值需要按照【生命】的有效值初始化\n"
        fileContent += "\t/// </summary>\n"
        fileContent += "\tpublic override void InitPropertyData()\n"
        fileContent += "\t{\n"
        fileContent += "\t\n"
        fileContent += "\t}\n"
        
        fileContent += "\t/// <summary>\n"
        fileContent += "\t/// 初始化属性的事件（需要特殊处理的）\n"
        fileContent += "\t/// 例如【当前生命】的最大值，需要取【生命】的当前有效值作为最大值\n"
        fileContent += "\t/// </summary>\n"
        fileContent += "\tpublic override void InitPropertyEvent()\n"
        fileContent += "\t{\n"
        fileContent += "\t\n"
        fileContent += "\t}\n"
        
        fileContent += "}\n"
        File.Write(propertyUnitCSPath,fileContent)
        Log.WriteSuccess("生成" + propertyUnitCSPath + "成功")
    
    
#生成属性组中枚举属性的文件
def m_CreatePropertyUnitFile(p_ClassName, p_FileName, p_IdList, p_FileDesc):
    t_MD_Property = MapDataInfos.ReadOnly("Property")

    #生成属性组的所有属性
    propertyUnitCSPath = ProjectPath + "\\..\\..\\Assets\\Scripts\\Entity\\Battle\\PropertySystem\\Propertys\\Auto_" + p_FileName + "Pros.cs"
    fileContent = "using System.Collections.Generic;\n\n\n"
    fileContent += "/// <summary>\n"
    fileContent += "/// " + p_ClassName + "属性列表，工具自动生成，勿手动修改\n"
    fileContent += "/// " + p_FileDesc + "\n"
    fileContent += "/// </summary>\n"
    fileContent += "public partial class " + p_ClassName + "Pros\n"
    fileContent += "{\n"
    for t_id in p_IdList:
        t_PropertyName = t_MD_Property.DataData.GetData(t_id,"枚举名称")
        fileContent += "\t/// <summary>\n"
        fileContent += "\t/// " + str(t_MD_Property.DataData.GetData(t_id,"显示名称")) + "\n"
        fileContent += "\t/// </summary>\n"
        fileContent += "\tpublic Property " + str(t_PropertyName) + " { get; set; } = Property.New(\"" + t_MD_Property.DataData.GetData(t_id,"id") + "\");\n"
    
    #枚举和属性对应的字典
    fileContent += "\t\n"
    fileContent += "\tpublic override void InitPropertyList()\n"
    fileContent += "\t{\n"
    fileContent += "\t\tm_PropertyList = new()\n"
    fileContent += "\t\t{\n"
    for t_id in p_IdList:
        t_PropertyName = t_MD_Property.DataData.GetData(t_id,"枚举名称")
        fileContent += "\t\t\t{\"" + t_MD_Property.DataData.GetData(t_id,"id") + "\" , " + str(t_PropertyName) + "},\n"
    fileContent += "\t\t};\n"
    fileContent += "\t}\n"
                
    fileContent += "}\n"
    File.Write(propertyUnitCSPath,fileContent)
    Log.WriteSuccess("生成" + propertyUnitCSPath + "成功")