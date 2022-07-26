using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RpgDemo
{
	public class MoveStateAction : IStateAction
	{
		private Entity _entity;
		private MoveComponent _moveComponent;
		public override void Init()
		{
			_actionState = ActionState.Move;
		}

		public override void StateBegin()
		{
			_entity = GetEntity();
			_entity.GetCompenent<AnimationComponent>(ComponentID.Animation).PlayAnimation(EntityAnimationInteger.Walk);
			_moveComponent = _entity.GetCompenent<MoveComponent>(ComponentID.Move);
			_moveComponent.StartMove();
			_moveComponent.UpdateSpeed(5);
		}

		public override void StateEnd()
		{
			_moveComponent.StopMove();
			_entity = null;
			_moveComponent = null;
		}

		public override void OnCommand(Command command)
		{
			if(command.Direction == Vector3.zero)
			{
				_entity?.GetCompenent<StateMachineComponent>(ComponentID.StateMachine)?.TransferToState(ActionState.Idle);
			}
			else
			{
				_moveComponent.UpdateDirection(command.Direction);
			}
		}
	}
}