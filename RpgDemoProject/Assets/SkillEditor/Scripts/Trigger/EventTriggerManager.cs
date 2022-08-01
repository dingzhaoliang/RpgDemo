using System;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RpgDemo {
	[Serializable]
	public class EventTriggerManager
	{
		public EventTriggerType triggerType = EventTriggerType.time;

		[HideInInspector]
		[EventTrigger(EventTriggerType.time)]
		public TimeParams timeParams;

		[ShowIf("triggerType", EventTriggerType.addBuff)]
		[EventTrigger(EventTriggerType.addBuff)]
		public AddBuffParams addBuffParams;

		[ShowIf("triggerType", EventTriggerType.onCDEnter)]
		[EventTrigger(EventTriggerType.onCDEnter)]
		public OnCDEnterParams onCDEnterParams;
	}
}