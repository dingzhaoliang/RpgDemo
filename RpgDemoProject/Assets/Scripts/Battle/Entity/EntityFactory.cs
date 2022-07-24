using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RpgDemo
{
    public class EntityFactory
    {
        private static uint _entityID = 0;
        public static Entity CreatePlayEntity()
        {
            Entity entity = new Entity();
            entity.Init();
            IComponent gameObjectComponent = entity.AttachCompenent(ComponentID.GameObject, new GameObjectComponent());
            gameObjectComponent.Init(new GameObjectComponentParams() { path = "Assets/Suriyun/Eri/Prefab/Eri_schooluniform.prefab" });
            uint entityId = _entityID++;
            entity.StartAll();
            EntityManager.Instance.AddEntity(entityId, entity);
            return entity;
        }
    }
}