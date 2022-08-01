using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Playables;
using Newtonsoft;
using Newtonsoft.Json.Linq;
using UnityEngine.Timeline;
using System.Reflection;
using System.Linq;
using System.IO;

namespace RpgDemo
{
	public class SkillEditorTool
	{
		[MenuItem("Assets/快速导出技能", true, -101)]
		public static bool ExportSkillEventCondition()
		{
			if (Selection.assetGUIDs.Length > 1)
			{
				return true;
			}
			return Selection.activeObject is GameObject;
		}

		[MenuItem("Assets/快速导出技能", false, -101)]
		public static void ExportSkillEvent()
		{
			for (int i = 0; i < Selection.assetGUIDs.Length; i++)
			{
				EditorUtility.DisplayProgressBar("快速导出技能", "进度 :" + i + "/" + Selection.assetGUIDs.Length, (float)i / Selection.assetGUIDs.Length);

				string pathGUID = Selection.assetGUIDs[i];
				string path = AssetDatabase.GUIDToAssetPath(pathGUID);
				GameObject asset = AssetDatabase.LoadAssetAtPath<GameObject>(path);
				if (asset != null)
				{
					PlayableDirector playableDirector = asset.GetComponent<PlayableDirector>();
					if (playableDirector != null)
					{
						SkillEditorTool.Generate(playableDirector.playableAsset, asset.name);
					}
				}
			}
			EditorUtility.ClearProgressBar();
			EditorUtility.DisplayDialog("快速导出技能", "导出完毕", "OK");
		}
		public static void Generate(PlayableAsset playableAsset, string fileName)
		{
			Dictionary<long, bool> repeatCheck = new Dictionary<long, bool>();
			long count = 0;
			JArray root = new JArray();

			foreach (PlayableBinding bind in playableAsset.outputs)
			{
				if (bind.sourceObject is PlayableTrack)
				{
					PlayableTrack playableTrack = (bind.sourceObject as PlayableTrack);
					List<TimelineClip> clips = new List<TimelineClip>();
					foreach (TimelineClip clip in (playableTrack.GetClips()))
					{
						clips.Add(clip);
					}
					clips.Sort((a, b) =>
					{
						if (a.start < b.start)
						{
							return -1;
						}
						return 1;
					});
					foreach (TimelineClip clip in clips)
					{
						JObject jobj = ToJObject(clip.displayName, clip.asset as SkillEventBase, clip.start, playableAsset, repeatCheck, ref count);
						root.Add(jobj);
					}
				}
			}

			string json = root.ToString();

			//生成Json
			string eventConfigFilePath_json = SkillConfigLocal.GetPath() + fileName + ".json";
			if (!File.Exists(eventConfigFilePath_json))
			{
				File.Create(eventConfigFilePath_json).Dispose();
			}
			File.WriteAllText(eventConfigFilePath_json, json);

			//生成Lua
			//string json2lua = Json2Lua.ToLua(json);
			//string luaString = "local events = {json2lua}\n";
			//luaString += "return events";
			//luaString = luaString.Replace("{json2lua}", json2lua);
			//string eventConfigFilePath_lua = SkillConfigLocal.GetLuaPath() + fileName + ".lua";
			//if (!File.Exists(eventConfigFilePath_lua))
			//{
			//	File.Create(eventConfigFilePath_lua).Dispose();
			//}
			//File.WriteAllText(eventConfigFilePath_lua, luaString);
		}

		private static JObject ToJObject(string eventName, SkillEventBase evt, double start, PlayableAsset playableAsset, Dictionary<long, bool> repeatCheck, ref long count)
		{
			JObject jObj = new JObject();

			bool repeatCheckValue;
			if (repeatCheck.TryGetValue(evt.eventID, out repeatCheckValue))
			{
				//重复了
				evt.NewEventID();
				evt.eventID += count;
				count++;
			}

			repeatCheck[evt.eventID] = true;
			jObj.Add("id", evt.eventID);
			jObj.Add("preId", evt.preID);
			jObj.Add("prob", evt.prob);
			jObj.Add("event", eventName);
			jObj.Add("type", (int)evt.triggerManager.triggerType);

			EventTriggerType triggerType = evt.triggerManager.triggerType;
			if (triggerType == EventTriggerType.time)
			{
				(evt.triggerManager.timeParams).time = start;
			}
			jObj.Add("type_param", GetTriggerTypeParams(evt.triggerManager, triggerType));

			JArray jArr = new JArray();
			jArr.Add(evt.loopCount);
			jArr.Add(evt.loopInterval);
			jObj.Add("loop", jArr);

			return jObj;
		}

		private static JObject GetTriggerTypeParams(EventTriggerManager triggerManager, EventTriggerType triggerType)
		{
			FieldInfo[] fieldInfos = triggerManager.GetType().GetFields();
			foreach (FieldInfo fieldInfo in fieldInfos)
			{
				if (fieldInfo != null && fieldInfo.IsDefined(typeof(EventTriggerAttribute)))
				{
					EventTriggerAttribute eventTrigger = fieldInfo.GetCustomAttribute<EventTriggerAttribute>();
					if (eventTrigger.triggerType == triggerType)
					{
						return JObject.FromObject(fieldInfo.GetValue(triggerManager));
					}
				}
			}
			return null;
		}
	}
}