using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AttributeUsage(AttributeTargets.Field)]
public class SkillBaseParamsAttribute : Attribute
{

}

[AttributeUsage(AttributeTargets.Field)]

public class SkillCustomParamsAttribute : Attribute
{

}

[AttributeUsage(AttributeTargets.Class)]
public class EventTriggerAttribute:Attribute
{
	public EventTriggerType triggerType;

	public EventTriggerAttribute(EventTriggerType type)
	{
		triggerType = type;
	}
}