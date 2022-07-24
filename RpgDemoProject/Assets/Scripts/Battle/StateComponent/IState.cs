using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RpgDemo
{
	public class IState
	{
		public virtual void Init()
		{

		}
		public virtual void StateBegin()
		{

		}
		public virtual void StateEnd()
		{

		}
		public virtual void UpdateStateParams()
		{

		}
		public virtual void ActionUpdate()
		{

		}
		public virtual void OnCommand()
		{

		}
		public virtual bool IsPermited()
		{
			return true;
		}
		public virtual bool IsConditioned()
		{
			return true;
		}
	}
}