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
			EntityConfig entityConfig = Resources.Load<EntityConfig>(entityParms.configName);
			Entity entity = new Entity(EntityKind.Player, entityParms.isLocalPlayer);
			entity.Init();
			IComponent gameObjectComponent = entity.AttachCompenent(ComponentID.GameObject, new GameObjectComponent(), new GameObjectComponentParams() { path = entityConfig.prefabPath });
			entity.AttachCompenent(ComponentID.Move, new MoveComponent());
			entity.AttachCompenent(ComponentID.Animation, new AnimationComponent());
			entity.AttachCompenent(ComponentID.EntityUI, new EntityUIComponent(), new EntityUIComponenttParams() { path = "Assets/GameAssets/UI/Prefabs/Player3DUI.prefab", kind = EntityUIKind.Player });
			if (entityParms.isLocalPlayer)
			{
				entity.AttachCompenent(ComponentID.CommandHandler, new CommandHandlerComponent());
			}
			IComponent stateMachineComponent = entity.AttachCompenent(ComponentID.StateMachine, new StateMachineComponent(), new StateMachineComponentParams() { defaultState = ActionState.Idle });
			uint entityId = _entityID++;
			entity.StartAll();
			EntityManager.Instance.AddEntity(entityId, entity);
			return entity;
		}
	}
}