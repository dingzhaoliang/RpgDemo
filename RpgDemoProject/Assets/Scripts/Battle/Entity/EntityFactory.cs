using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RpgDemo
{
	public class EntityFactory
	{
		private static uint _entityID = 0;
		public static Entity CreatePlayEntity(EntityParms entityParms)
		{
			Entity entity = new Entity(EntityKind.Player, entityParms.isLocalPlayer);
			entity.Init();
			IComponent gameObjectComponent = entity.AttachCompenent(ComponentID.GameObject, new GameObjectComponent(), new GameObjectComponentParams() { path = entityParms.prefabPath });
			IComponent stateMachineComponent = entity.AttachCompenent(ComponentID.StateMachine, new StateMachineComponent(), new StateMachineComponentParams() { defaultState = ActionState.Idle });
			entity.AttachCompenent(ComponentID.Move, new MoveComponent());
			if (entityParms.isLocalPlayer)
			{
				entity.AttachCompenent(ComponentID.CommandHandler, new CommandHandlerComponent());
			}
			uint entityId = _entityID++;
			entity.StartAll();
			EntityManager.Instance.AddEntity(entityId, entity);
			return entity;
		}
	}
}