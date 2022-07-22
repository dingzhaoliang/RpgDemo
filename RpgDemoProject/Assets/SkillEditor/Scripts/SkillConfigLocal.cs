using UnityEngine;

public static class SkillConfigLocal
{
	//public const string JSON_PATH = UnityEngine.Application.dataPath + "../../../ConfigExcel/";

	public static string GetPath()
	{
		return System.Environment.CurrentDirectory + "/Assets/Config/Json/Skill/";
	}

	public static string GetLuaPath()
	{
		return Application.dataPath + "/Config/Lua/Skill/";               //lua逻辑代码目录

	}
}