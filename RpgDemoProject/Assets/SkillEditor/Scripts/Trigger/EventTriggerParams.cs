using System;

namespace RpgDemo
{
	[Serializable]

	public class BaseEventTriggerParams
	{

	}

	[Serializable]
	public class TimeParams : BaseEventTriggerParams
	{
		public double time;
	}

	[Serializable]
	public class OnCDEnterParams : BaseEventTriggerParams
	{
		public int skillID;
	}

	[Serializable]
	public class AddBuffParams : BaseEventTriggerParams
	{
		public int buffID;
	}
}

