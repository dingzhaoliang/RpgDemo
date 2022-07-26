using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace RpgDemo
{
	public class StateMachineComponentParams : BaseComponentParms
	{
		public ActionState defaultState;
	}

	public class StateMachineComponent : IComponent
	{
		private StateMachineComponentParams _stateMachineComponentParams;
		private IStateAction _curAction;
		private IStateAction _preAction;
		private IStateAction _defaultAction;
		private List<IStateAction> _actionStates;

		public override void Init(BaseComponentParms baseComponentParms)
		{
			_actionStates = new List<IStateAction>();
			_stateMachineComponentParams = baseComponentParms as StateMachineComponentParams;
		}
		public override void Start()
		{
			SteDefaultState();
		}

		public override void Update()
		{
			if(_curAction != null)
			{
				_curAction.ActionUpdate();
			}
		}

		private void SteDefaultState()
		{
			if (_stateMachineComponentParams != null)
			{
				var action = GetActionState(_stateMachineComponentParams.defaultState);
				_curAction = action;
				_defaultAction = action;
				_preAction = action;
				action.StateBegin();
			}
		}

		public void TransferToState(ActionState actionState)
		{
			var nextAction = GetActionState(_stateMachineComponentParams.defaultState);

			if (_curAction.IsPermited() && nextAction.IsConditioned())
			{
				_curAction.StateEnd();
				nextAction.StateBegin();
				_preAction = _curAction;
				_curAction = nextAction;
			}
		}

		private IStateAction GetActionState(ActionState state)
		{
			IStateAction actionState = null;
			foreach(var action in _actionStates)
			{
				if(action.GetState() == state)
				{
					actionState = action;
				}
			}
			if(actionState == null)
			{
				actionState = CreateActionState(state);
			}
			return actionState;
		}

		private IStateAction CreateActionState(ActionState state)
		{
			IStateAction action = new IStateAction();
			switch (state)
			{
				case ActionState.Idle:
					action = new IdleStateAction();
					break;
				case ActionState.Move:
					action = new MoveStateAction();
					break;
			}
			action.Init();
			action.SetEntity(_entity);
			Assert.IsTrue(action.GetState() != ActionState.None);
			_actionStates.Add(action);
			return action;
		}

		public void OnCommand(Command command)
		{
			if(_curAction != null)
			{
				_curAction.OnCommand(command);
			}
		}
	}
}