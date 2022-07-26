using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RpgDemo
{
	public class IStateAction
	{
		protected ActionState _actionState = ActionState.None;
		private Entity _entity;
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
		public virtual void OnCommand(Command command)
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

		public ActionState GetState()
		{
			return _actionState;
		}
		public void SetEntity(Entity entity)
		{
			_entity = entity;
		}
		public Entity GetEntity()
		{
			return _entity;
		}
	}
}