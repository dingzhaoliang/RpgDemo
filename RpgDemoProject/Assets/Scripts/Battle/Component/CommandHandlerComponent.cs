using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RpgDemo
{
	public class CommandHandlerComponent : IComponent
	{

		public override void Init(BaseComponentParms baseComponentParms)
		{
		}
		public override void Start()
		{

		}

		public override void AttachWorld()
		{
			GameLogic.Instance.RegistLocalPlayerCommandHandler(OnCommand);
		}

		public override void DetachFromWorld()
		{
			GameLogic.Instance.UnregistLocalPlayerCommandHandler(OnCommand);
		}

		private void OnCommand(Command command)
		{
			_entity.GetCompenent<StateMachineComponent>(ComponentID.StateMachine)?.OnCommand(command);
		}
	}
}