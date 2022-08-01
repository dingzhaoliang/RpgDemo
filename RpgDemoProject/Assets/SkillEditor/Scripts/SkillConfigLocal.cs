using System.IO;
using UnityEngine;

public static class SkillConfigLocal
{
	//public const string JSON_PATH = UnityEngine.Application.dataPath + "../../../ConfigExcel/";

	public static string GetPath()
	{
		string path = System.Environment.CurrentDirectory + "/Assets/GameAssets/Config/Json/Skill/";
		if (!Directory.Exists(path))
		{
			Directory.CreateDirectory(path);
		}
		return path;
	}

	public static string GetLuaPath()
	{
		return Application.dataPath + "/Config/Lua/Skill/";               //lua逻辑代码目录

	}
}