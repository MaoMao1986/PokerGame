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
    t_MD_PropertysObject = MapDataInfos.ReadOnly("辅助_属性组实例")
    t_MD_Unit = MapDataInfos.ReadOnly("辅助_对象结构")
    
    for t_id in t_MD_PropertyAssist.DataData.GetRowKeyList():
        t_DataGroupString = t_MD_PropertyAssist.DataData.GetData(t_id,"数据分组")
        t_FileName = t_MD_PropertyAssist.DataData.GetData(t_id,"文件名")
        t_ClassName = t_MD_PropertyAssist.DataData.GetData(t_id,"类名")
        t_FileDesc = t_MD_PropertyAssist.DataData.GetData(t_id,"说明")
        t_DataGroupList = t_MD_Property.DataData.GetRowListByGroup(DataTrans.ListString(t_DataGroupString.split("|")))
        #创建属性组属性列表文件
        m_CreatePropertysFile(t_ClassName, t_FileName, t_DataGroupList, t_FileDesc)
        #创建属性组代码文件
        m_CreatePropertyCodeFile(t_ClassName, t_FileName, t_FileDesc)
        
    for t_id in t_MD_Unit.DataData.GetRowKeyList():
        t_FileName = t_MD_Unit.DataData.GetData(t_id,"文件名")
        t_ClassName = t_MD_Unit.DataData.GetData(t_id,"类名")
        t_UnitList = t_MD_Unit.DataData.GetData(t_id,"嵌套对象")
        t_PropertyList = t_MD_Unit.DataData.GetData(t_id,"嵌套属性组")
        m_CreateAutoUnitFile(t_ClassName, t_FileName, t_UnitList, t_PropertyList)
        m_CreateUnitFile(t_ClassName, t_FileName, t_UnitList, t_PropertyList)
    
  
#生成属性组中代码继承的文件（如果文件不存在则生成，如果存在，则跳过）
def m_CreatePropertyCodeFile(p_ClassName, p_FileName, p_FileDesc):
    propertyUnitCSPath = ProjectPath + "\\..\\..\\Assets\\Scripts\\Entity\\Battle\\PropertySystem\\Propertys\\" + p_FileName + ".cs"
    if not File.Exists(propertyUnitCSPath):
        fileContent = "using System.Collections.Generic;\n\n\n"
        fileContent += "/// <summary>\n"
        fileContent += "/// " + p_ClassName + "属性组代码逻辑\n"
        fileContent += "/// " + p_FileDesc + "\n"
        fileContent += "/// </summary>\n"
        fileContent += "public partial class " + p_ClassName + " : Propertys, ISaveDataBase\n"
        fileContent += "{\n"
        
        fileContent += "\tpublic DataChangedEventHandler DataChangedEvent { get; set; }\n"
        fileContent += "\t\n"
        
        fileContent += "\t/// <summary>\n"
        fileContent += "\t/// 初始化属性值（需要特殊处理的，其他的要么按照配置初始化，要么按照其他属性的数值初始化）\n"
        fileContent += "\t/// 例如【当前生命】的值需要按照【生命】的有效值初始化\n"
        fileContent += "\t/// </summary>\n"
        fileContent += "\tpublic void InitData()\n"
        fileContent += "\t{\n"
        fileContent += "\t\n"
        fileContent += "\t}\n"
        
        fileContent += "\t/// <summary>\n"
        fileContent += "\t/// 初始化属性的事件（需要特殊处理的）\n"
        fileContent += "\t/// 例如【当前生命】的最大值，需要取【生命】的当前有效值作为最大值\n"
        fileContent += "\t/// </summary>\n"
        fileContent += "\tpublic void InitEvent()\n"
        fileContent += "\t{\n"
        fileContent += "\t\n"
        fileContent += "\t}\n"
        
        fileContent += "}\n"
        File.Write(propertyUnitCSPath,fileContent)
        Log.WriteSuccess("生成" + propertyUnitCSPath + "成功")
    
    
#生成属性组中枚举属性的文件
def m_CreatePropertysFile(p_ClassName, p_FileName, p_IdList, p_FileDesc):
    t_MD_Property = MapDataInfos.ReadOnly("Property")

    #生成属性组的所有属性
    propertyUnitCSPath = ProjectPath + "\\..\\..\\Assets\\Scripts\\Entity\\Battle\\PropertySystem\\AutoPropertys\\Auto_" + p_FileName + ".cs"
    fileContent = "using System.Collections.Generic;\n"
    fileContent = "using Newtonsoft.Json;\n\n\n"
    fileContent += "/// <summary>\n"
    fileContent += "/// " + p_ClassName + "属性列表，工具自动生成，勿手动修改\n"
    fileContent += "/// " + p_FileDesc + "\n"
    fileContent += "/// </summary>\n"
    fileContent += "public partial class " + p_ClassName + "\n"
    fileContent += "{\n"
    for t_id in p_IdList:
        t_PropertyName = t_MD_Property.DataData.GetData(t_id,"枚举名称")
        fileContent += "\t/// <summary>\n"
        fileContent += "\t/// " + str(t_MD_Property.DataData.GetData(t_id,"显示名称")) + "\n"
        fileContent += "\t/// </summary>\n"
        fileContent += "\t[JsonIgnore]\n"
        fileContent += "\tpublic Property " + str(t_PropertyName) + "{ get { return GetProperty(\"" + t_MD_Property.DataData.GetData(t_id,"id") + "\"); } }\n"
    
    #枚举和属性对应的字典
    fileContent += "\t\n"
    fileContent += "\tpublic override void InitPropertyList(Enum_PropertyInitType p_Type = Enum_PropertyInitType.Zero)\n"
    fileContent += "\t{\n"
    fileContent += "\t\tPropertyList ??= new();\n"
    fileContent += "\t\t{\n"
    for t_id in p_IdList:
        t_PropertyName = t_MD_Property.DataData.GetData(t_id,"枚举名称")
        fileContent += "\t\t\tm_InitDefault(\"" + t_MD_Property.DataData.GetData(t_id,"id") + "\" , Property.New(\"" + t_MD_Property.DataData.GetData(t_id,"id") + "\", p_Type));\n"
    fileContent += "\t\t}\n"
    
    #调用初始化属性值和事件
    fileContent += "\t\t\n"
    fileContent += "\t\tInitData();\n"
    fileContent += "\t\tInitEvent();\n"
    
    fileContent += "\t}\n"                
    fileContent += "}\n"
    File.Write(propertyUnitCSPath,fileContent)
    Log.WriteSuccess("生成" + propertyUnitCSPath + "成功")
    
    
def m_CreateAutoUnitFile(p_ClassName, p_FileName, p_UnitList, p_PropertyList):
    t_MD_PropertysObject = MapDataInfos.ReadOnly("辅助_属性组实例")
    t_MD_Unit = MapDataInfos.ReadOnly("辅助_对象结构")
    t_MD_PropertyAssist = MapDataInfos.ReadOnly("辅助_属性分组")
    t_UnitList = p_UnitList.split("|")
    t_PropertyList = p_PropertyList.split("|")
    
    unitCSPath = ProjectPath + "\\..\\..\\Assets\\Scripts\\Entity\\Unit\\AutoUnit\\Auto_" + p_FileName + ".cs"
    
    fileContent = "using System.Collections.Generic;\n\n\n"
    fileContent += "/// <summary>\n"
    fileContent += "/// " + p_ClassName + "对象列表，工具自动生成，勿手动修改\n"
    fileContent += "/// </summary>\n"
    fileContent += "public partial class " + p_ClassName + "\n"
    fileContent += "{\n"
    
    #所有的嵌套子对象
    if p_UnitList != "":
        for t_id in t_UnitList:
            t_ClassName = t_MD_Unit.DataData.GetData(t_id,"类名")
            t_VariableName = t_MD_Unit.DataData.GetData(t_id,"变量名")
            t_Desc = t_MD_Unit.DataData.GetData(t_id,"说明")
            fileContent += "\t/// <summary>\n"
            fileContent += "\t/// " + t_Desc + "\n"
            fileContent += "\t/// </summary>\n"
            fileContent += "\tpublic " + t_ClassName + " " + t_VariableName + "{ get; set;}\n"
        
        fileContent += "\t\n"
    
    #所有的属性组列表
    if p_PropertyList != "":
        for t_id in t_PropertyList:
            t_ClassName = t_MD_PropertyAssist.DataData.GetData(t_MD_PropertysObject.DataData.GetData(t_id,"所属属性类型"), "类名")
            t_VariableName = t_MD_PropertysObject.DataData.GetData(t_id,"变量名")
            t_Type = t_MD_PropertysObject.DataData.GetData(t_id,"类型")
            t_isShow = t_MD_PropertysObject.DataData.GetData(t_id,"是否声明")
            t_Desc = t_MD_PropertysObject.DataData.GetData(t_id,"说明")
            if t_isShow == "" :
                fileContent += "\t/// <summary>\n"
                fileContent += "\t/// " + t_Desc + "\n"
                fileContent += "\t/// </summary>\n"
                fileContent += "\t" + t_Type + " " + t_ClassName + " " + t_VariableName + " { get; set; }\n"
        
        fileContent += "\t\n"
    
    
    #枚举和属性对应的字典
    fileContent += "\t\n"
    fileContent += "\tpublic override void Init()\n"
    fileContent += "\t{\n"
    
    fileContent += "\t\tDevelopUnitList ??= new();\n"
    fileContent += "\t\t\n"
    
    #对象为空则创建
    if p_UnitList != "":
        fileContent += "\t\t// 初始化子对象\n"
        fileContent += "\t\t{\n"
        for t_id in t_UnitList:
            t_ClassName = t_MD_Unit.DataData.GetData(t_id,"类名")
            t_VariableName = t_MD_Unit.DataData.GetData(t_id,"变量名")
            t_Desc = t_MD_Unit.DataData.GetData(t_id,"说明")
            fileContent += "\t\t\t" + t_VariableName +" ??= new();\n"
            fileContent += "\t\t\t" + t_VariableName +".Init();\n"
            fileContent += "\t\t\tDevelopUnitList.Add(" + t_VariableName + ".Name, " + t_VariableName + ");\n"
            fileContent += "\t\t\t\n"
        fileContent += "\t\t}\n"
    
    #初始化属性列表
    if p_PropertyList != "":
        fileContent += "\t\t\n"
        fileContent += "\t\t// 初始化属性列表\n"
        fileContent += "\t\t{\n"
        for t_id in t_PropertyList:
            t_ClassName = t_MD_PropertyAssist.DataData.GetData(t_MD_PropertysObject.DataData.GetData(t_id,"所属属性类型"), "类名")
            t_VariableName = t_MD_PropertysObject.DataData.GetData(t_id,"变量名")
            t_Type = t_MD_PropertysObject.DataData.GetData(t_id,"类型")
            t_InitType = t_MD_PropertysObject.DataData.GetData(t_id,"初始化类型")
            t_Desc = t_MD_PropertysObject.DataData.GetData(t_id,"说明")
            fileContent += "\t\t\t" + t_VariableName +" ??= new();\n"
            if t_InitType == "初始化" :
                fileContent += "\t\t\t" + t_VariableName + ".InitPropertyList(Enum_PropertyInitType.InitValue);\n"
            else:
                fileContent += "\t\t\t" + t_VariableName + ".InitPropertyList();\n"
            fileContent += "\t\t\t\n"
        fileContent += "\t\t}\n"
    
    #调用初始化属性值和事件
    fileContent += "\t\t\n"
    fileContent += "\t\tInitData();\n"
    fileContent += "\t\tCalculateBattlePropertys();\n"
    fileContent += "\t\tInitEvent();\n"
    
    fileContent += "\t}\n"                
    fileContent += "}\n"
    
    File.Write(unitCSPath,fileContent)
    Log.WriteSuccess("生成" + unitCSPath + "成功")
    
    
    
def m_CreateUnitFile(p_ClassName, p_FileName, p_UnitList, p_PropertyList):
    t_MD_PropertysObject = MapDataInfos.ReadOnly("辅助_属性组实例")
    t_MD_Unit = MapDataInfos.ReadOnly("辅助_对象结构")
    t_MD_PropertyAssist = MapDataInfos.ReadOnly("辅助_属性分组")
    t_UnitList = p_UnitList.split("|")
    t_PropertyList = p_PropertyList.split("|")
    
    unitCSPath = ProjectPath + "\\..\\..\\Assets\\Scripts\\Entity\\Unit\\SubUnit\\" + p_FileName + ".cs"
    if not File.Exists(unitCSPath):
        fileContent = "using System.Collections.Generic;\n\n\n"
        fileContent += "public partial class " + p_ClassName + " : UnitBase, ISaveDataBase\n"
        fileContent += "{\n"
        fileContent += "\tpublic DataChangedEventHandler DataChangedEvent{ get; set; }\n"
        fileContent += "\t\n"
        fileContent += "\tpublic override void CalculateBattlePropertys()\n"
        fileContent += "\t{\n"
        fileContent += "\t\t\n"    
        fileContent += "\t}\n"
        fileContent += "\t\n"
        fileContent += "\tpublic void InitData()\n"
        fileContent += "\t{\n"
        fileContent += "\t\tName = " + p_FileName + ";\n"
        fileContent += "\t}\n"
        fileContent += "\t\n"
        fileContent += "\tpublic void InitEvent()\n"
        fileContent += "\t{\n"
        fileContent += "\t\t\n"  
        fileContent += "\t}\n"
        fileContent += "}\n"
        File.Write(unitCSPath,fileContent)
        Log.WriteSuccess("生成" + unitCSPath + "成功")