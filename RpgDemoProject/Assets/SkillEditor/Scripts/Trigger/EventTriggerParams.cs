using System;

[Serializable]

public class BaseEventTriggerParams
{

}

[Serializable]
[EventTrigger(EventTriggerType.time)]
public class TimeParams : BaseEventTriggerParams
{
	public double time;
}

[Serializable]
[EventTrigger(EventTriggerType.onCDEnter)]
public class OnCDEnterParams : BaseEventTriggerParams
{
	public int skillID;
}

[Serializable]
[EventTrigger(EventTriggerType.addBuff)]
public class AddBuffParams : BaseEventTriggerParams
{
	public int buffID;
}
