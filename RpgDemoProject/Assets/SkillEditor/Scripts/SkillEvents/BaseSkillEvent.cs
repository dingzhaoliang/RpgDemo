using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEditor;
using UnityEngine.Timeline;
using System;

[HideInMenu]
public class BaseSkillEvent : PlayableAsset
{
	[SkillBaseParams]
	public long eventID;
	[SkillBaseParams]
	public long preID;
	[SkillBaseParams]
	public int loopCount;
	[SkillBaseParams]
	public float loopInterval;
	[SkillBaseParams]
	[Range(0, 1)]
	public float prob = 1f;

	public EventTriggerType triggerType = EventTriggerType.time;
	[SerializeReference]
	public List<BaseEventTriggerParams> triggerParams = new List<BaseEventTriggerParams>();

	public BaseSkillEvent()
	{
		NewEventID();
	}

	public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
	{
		return Playable.Null;
	}

	public void NewEventID()
	{
		eventID = Convert.ToInt64((DateTime.UtcNow - new DateTime(2021, 1, 1, 0, 0, 0, 0)).TotalMilliseconds);
	}
}

[SerializeField]
public class BaseSkillEventParams
{
	public int eventID;
	public int preID;
}