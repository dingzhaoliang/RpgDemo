using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Reflection;
using System;

[CustomEditor(typeof(BaseSkillEvent), true)]
public class BaseSkillEventEditor : Editor
{
	private BaseSkillEvent m_target;
	private bool _showBaseParams;
	private bool _showCustomParams;
	private bool _showEventTriggerParams;
	bool _createEventTriggerParams = false;
	EventTriggerType curTriggerType = EventTriggerType.time;
	private void OnEnable()
	{
		m_target = (BaseSkillEvent)target;
		_showBaseParams = SessionState.GetBool("showBaseParams", true);
		_showCustomParams = SessionState.GetBool("showCustomParams", true);
		_showEventTriggerParams = SessionState.GetBool("showEventTriggerParams", true);
		_createEventTriggerParams = false;
	}
	public override void OnInspectorGUI()
	{
		serializedObject.Update();

		GUILayout.BeginVertical("box");
		ShowBaseParams();
		GUILayout.EndVertical();
		GUILayout.Space(5);


		GUILayout.BeginVertical("box");
		ShowCustomParams();
		GUILayout.EndVertical();
		GUILayout.Space(5);

		GUILayout.BeginVertical("box");
		ShowEventTrrggerParams();
		GUILayout.EndVertical();

		serializedObject.ApplyModifiedProperties();
		if (GUI.changed)
		{
			EditorUtility.SetDirty(target);
		}
	}

	public void ShowBaseParams()
	{

		_showBaseParams = EditorGUILayout.Foldout(_showBaseParams, "BaseParams", true);
		if (!_showBaseParams)
		{
			return;
		}
		EditorGUI.indentLevel++;
		SerializedProperty serializedProperty = serializedObject.GetIterator();
		bool enterChildren = true;
		while (serializedProperty.NextVisible(enterChildren))
		{
			if (enterChildren)
			{
				enterChildren = false;
			}
			else
			{
				FieldInfo fieldInfo = m_target.GetType().GetField(serializedProperty.name);
				if (fieldInfo != null && fieldInfo.IsDefined(typeof(SkillBaseParamsAttribute)))
				{
					if (serializedProperty.name == "eventID")
					{
						using (new EditorGUILayout.HorizontalScope())
						{
							GUI.enabled = false;
							EditorGUILayout.PropertyField(serializedProperty);
							GUI.enabled = true;
							if (GUILayout.Button("Copy"))
							{
								GUIUtility.systemCopyBuffer = m_target.eventID.ToString();
							}
							if (GUILayout.Button("New"))
							{
								m_target.NewEventID();
							}
						}
						continue;
					}
					else if (serializedProperty.name == "preID")
					{
						using (new GUILayout.HorizontalScope())
						{
							GUI.enabled = false;
							EditorGUILayout.PropertyField(serializedProperty);
							GUI.enabled = true;
							if (GUILayout.Button("Paste"))
							{
								long preID = 0;
								if (long.TryParse(GUIUtility.systemCopyBuffer, out preID))
								{
									m_target.preID = preID;
								}
							}
							if (GUILayout.Button("Reset"))
							{
								m_target.preID = 0;
							}
						}
						continue;
					}
					EditorGUILayout.PropertyField(serializedProperty);
				}
			}
		}
		EditorGUI.indentLevel--;
	}


	void ShowCustomParams()
	{
		_showCustomParams = EditorGUILayout.Foldout(_showCustomParams, "Custom Params", true);
		if (!_showCustomParams)
			return;

		EditorGUI.indentLevel++;
		SerializedProperty serializedProperty = serializedObject.GetIterator();
		bool enterChildren = true;
		while (serializedProperty.NextVisible(enterChildren))
		{
			if (enterChildren)
			{
				enterChildren = false;
			}
			else
			{
				FieldInfo fieldInfo = m_target.GetType().GetField(serializedProperty.name);
				if(fieldInfo != null && fieldInfo.IsDefined(typeof(SkillCustomParamsAttribute)))
				{
					EditorGUILayout.PropertyField(serializedProperty);
				}
			}
		}
		EditorGUI.indentLevel--;
	}

	public void ShowEventTrrggerParams()
	{
		_showEventTriggerParams = EditorGUILayout.Foldout(_showEventTriggerParams, "Event Trigger Params");
		if (!_showEventTriggerParams)
			return;
		EditorGUI.indentLevel++;
		SerializedProperty serializedProperty = serializedObject.FindProperty("triggerType");
		
		EditorGUILayout.PropertyField(serializedProperty);
		if (m_target.triggerParams.Count > 0)
		{
			if ((EventTriggerType)serializedProperty.intValue != EventTriggerType.time)
			{
				SerializedProperty property = serializedObject.FindProperty("triggerParams");
				EditorGUILayout.PropertyField(property.GetArrayElementAtIndex(0), EditorGUIUtility.TrTextContent("Params"), true);
			}
		}
		if (!_createEventTriggerParams)
		{
			_createEventTriggerParams = true;
			curTriggerType = (EventTriggerType)serializedProperty.intValue;
			Type triggerParamsType = GetEventTriggerParamsClass((EventTriggerType)serializedProperty.intValue);
			BaseEventTriggerParams baseSkillEventParams = (BaseEventTriggerParams)Activator.CreateInstance(triggerParamsType);
			m_target.triggerParams.Clear();
			m_target.triggerParams.Add(baseSkillEventParams);
		}
		if(curTriggerType != (EventTriggerType)serializedProperty.intValue && _createEventTriggerParams)
		{
			_createEventTriggerParams = false;
		}
	}
	
	Type GetEventTriggerParamsClass(EventTriggerType triggerType)
	{
		Assembly assembly = Assembly.GetAssembly(typeof(EventTriggerAttribute));
		Type[] types = assembly.GetExportedTypes();

		foreach(var type in types)
		{
			Attribute[] attrs = Attribute.GetCustomAttributes(type, true);
			foreach(var attr in attrs)
			{
				if(attr is EventTriggerAttribute && ((EventTriggerAttribute)attr).triggerType == triggerType)
				{
					return type;
				}
			}
		}

		return typeof(TimeParams);
	}
}
