using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Playables;

[CustomEditor(typeof(SkillTimeline))]
public class SkillTimelineEditor : Editor
{
	private SkillTimeline _target;
	private void OnEnable()
	{
		_target = target as SkillTimeline;
	}
	public override void OnInspectorGUI()
	{
		if (GUILayout.Button("生成技能配置"))
		{
			PlayableDirector playableDirector = _target.GetComponent<PlayableDirector>();
			SkillEditorTool.Generate(playableDirector.playableAsset, _target.gameObject.name);
		}
	}
}
