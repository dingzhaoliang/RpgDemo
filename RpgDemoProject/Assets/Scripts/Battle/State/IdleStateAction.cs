using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RpgDemo
{
	public class IdleStateAction:IStateAction
	{
		public override void Init()
		{
			_actionState = ActionState.Idle;
		}

		public override void StateBegin()
		{
			var entity = GetEntity();
			entity.GetCompenent<AnimationComponent>(ComponentID.Animation).PlayAnimation(EntityAnimationInteger.IdleB);
		}

		public override void OnCommand(Command command)
		{
			if (command.Direction != Vector3.zero)
			{
				var entity = GetEntity();
				entity?.GetCompenent<StateMachineComponent>(ComponentID.StateMachine)?.TransferToState(ActionState.Move);
			}
		}
	}
}