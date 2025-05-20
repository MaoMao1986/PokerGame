# coding=utf-8

#m_PythonScope.SetVariable("Project", ProjectController.ActiveProject);
#m_PythonScope.SetVariable("Log", LogController.GetInstance());
#m_PythonScope.SetVariable("Values", SystemValues.GetInstance());
#m_PythonScope.SetVariable("Sheets", ProjectSheets.GetInstance());
#m_PythonScope.SetVariable("DataOperate", DataOperateController.GetInstance());
#m_PythonScope.SetVariable("MapDataInfos", MapDataInfos.GetInstance());
#m_PythonScope.SetVariable("Reward", RewardController.GetInstance());


def Run(p_Array):
    txtPath = ProjectPath + "\\..\\..\\Assets\\Table\\"
    Log.WriteLog(txtPath,True)
    configCSPath = ProjectPath + "\\..\\..\\Assets\\Scripts\\Config\\Txt\\"
    Log.WriteLog(configCSPath,True)
    
    #生成配置表对应的cs解析文件    
    CreateConfigCS(txtPath,configCSPath)
    #生成所有配置表的总加载文件
    CreateConfigMainCS(txtPath)
                
                        
def CreateConfigCS(txtFilePath,csFilePath):
    File.DeleteFiles(csFilePath,"cs")
    typeArray = {"int":"TransToInt","string":"TransToString","bool":"TransToBool","double":"TransToDouble","int[]":"TransToIntArray","string[]":"TransToStringArray","bool[]":"TransToBoolArray","double[]":"TransToDoubleArray","int[,]":"TransToIntArray2","string[,]":"TransToStringArray2","bool[,]":"TransToBoolArray2","double[,]":"TransToDoubleArray2"}
    EnumData = Project.EnumFile
    for file in File.GetFiles(txtFilePath,"txt"):
        titles = File.GetLines(file,2,4)
        fieldName = titles[0].split("\t")
        fieldType = titles[1].split("\t")
        if len(fieldName) != len(fieldType):
            Log.WriteError("fieldName:{0}",True,len(fieldName))
            Log.WriteError("fieldType:{0}",True,len(fieldType))
            Log.WriteError("{0}文件的第2行字段配置和第4行数据类型配置的列数量不相等",True,file)
        fullname = File.GetFullName(file)
        name = File.GetFileName(file)
        fileContent = ""
        fileContent += "/// <summary>\n"
        fileContent += "/// 由" + fullname + "生成，工具自动生成，勿手动修改\n"
        fileContent += "/// </summary>\n"
        fileContent += "public class " + "DR" + name.title() + " : IConfigRow\n"
        fileContent += "{\n"
        
        #循环写入字段
        for i in range(len(fieldType)):
            if not (fieldType[i] in typeArray):
                if not (fieldType[i] in EnumData.GetRowKeyList()):
                    Log.WriteError("{0}文件的第4行数据类型字段的第{1}列{2}配置错误，无该类型",True,file,str(i),fieldType[i])
                else:
                    t_EnumType = EnumData.GetCodeName(fieldType[i])
                    fileContent += "\tpublic " + t_EnumType + " " + fieldName[i].title() + " {get; private set;}\n"
            else:
                fileContent += "\tpublic " + fieldType[i] + " " + fieldName[i].title() + " {get; private set;}\n"
        
        fileContent += "\n"
        fileContent += "\tpublic void ParseDataRow(string[] p_dataRowString, string[] p_Type)\n"
        fileContent += "\t{\n"
        fileContent += "\t\tint t_Index = 0;\n"
        
        #循环写入字段解析
        for i in range(len(fieldType)):
            if not (fieldType[i] in typeArray):
                t_EnumType = EnumData.GetCodeName(fieldType[i])
                fileContent += "\t\t" + fieldName[i].title() + " = ConfigManager.TransToEnum<" + t_EnumType + ">(p_dataRowString[t_Index]); t_Index++;\n"
            else:
                fileContent += "\t\t" + fieldName[i].title() + " = ConfigManager." + typeArray[fieldType[i]] + "(p_dataRowString[t_Index]); t_Index++;\n"
        
        fileContent += "\t}\n"
        fileContent += "}\n"
        File.Write(csFilePath + "DR" + name.title() + ".cs",fileContent)
        Log.WriteSuccess("生成" + csFilePath + "DR" + name.title() + ".cs" + "成功",True)
        
def CreateConfigMainCS(txtFilePath):
    configMainCSPath = ProjectPath + "\\..\\..\\Assets\\Scripts\\Entity\\Manager\\ConfigManagerEx.cs"
    
    fileContent = ""
    fileContent += "/// <summary>\n"
    fileContent += "/// 配置文件列表，工具自动生成，勿手动修改\n"
    fileContent += "/// </summary>\n"
    fileContent += "public static partial class ConfigManager\n"
    fileContent += "{\n"
    fileContent += "\tpublic static void LoadConfig()\n"
    fileContent += "\t{\n"
    for file in File.GetFiles(txtFilePath,"txt"):
        fullname = File.GetFullName(file)
        name = File.GetFileName(file)
        fileContent += "\t\tLoadData<DR" + name.title() + ">(\"" + fullname + "\");\n"
    fileContent += "\t}\n}\n"
    File.Write(configMainCSPath,fileContent)
    Log.WriteSuccess("生成" + configMainCSPath + "成功",True)
