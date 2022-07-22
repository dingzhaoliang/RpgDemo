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

public class SkillEditorTool
{
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
					JObject jobj = ToJObject(clip.displayName, clip.asset as BaseSkillEvent, clip.start, playableAsset, repeatCheck, ref count);
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

	private static JObject ToJObject(string eventName, BaseSkillEvent evt, double start, PlayableAsset playableAsset, Dictionary<long, bool> repeatCheck, ref long count)
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
		jObj.Add("type", (int)evt.triggerType);

		EventTriggerType triggerType = evt.triggerType;
		if(triggerType == EventTriggerType.time)
		{
			if (evt.triggerParams[0] is TimeParams)
			{
				(evt.triggerParams[0] as TimeParams).time = start;
			}
		}
		jObj.Add("type_param", GetTriggerTypeParams(evt.triggerParams, triggerType));

		JArray jArr = new JArray();
		jArr.Add(evt.loopCount);
		jArr.Add(evt.loopInterval);
		jObj.Add("loop", jArr);

		return jObj;
	}

	private static JObject GetTriggerTypeParams(List<BaseEventTriggerParams> triggerParams, EventTriggerType triggerType)
	{
		FieldInfo[] fieldInfos = triggerParams.GetType().GetFields();
		foreach (FieldInfo fieldInfo in fieldInfos)
		{
			if (fieldInfo != null && fieldInfo.IsDefined(typeof(EventTriggerAttribute)))
			{
				EventTriggerAttribute eventTrigger = fieldInfo.GetCustomAttributes<EventTriggerAttribute>().First();
				if (eventTrigger.triggerType == triggerType)
				{
					return JObject.FromObject(fieldInfo.GetValue(triggerParams));
				}
			}
		}
		return null;
	}
}
