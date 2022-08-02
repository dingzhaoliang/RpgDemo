using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RpgDemo
{
	public class EventContainer
	{
		private string _eventPath;
		public void Init(string path)
		{
			_eventPath = path;
		}
		public void ParseEvent()
		{
			JSONClass json = ResourceManager.Instance.LoadJson(_eventPath);
		}
	}
}