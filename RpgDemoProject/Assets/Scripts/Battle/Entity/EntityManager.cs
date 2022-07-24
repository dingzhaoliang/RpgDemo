using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RpgDemo
{
    public class EntityManager:USingleton<EntityManager>
    {
        private Dictionary<uint, Entity> _entities = new Dictionary<uint, Entity>(); 
        public void Init()
        {

        }

        public void AddEntity(uint entityID, Entity entity)
        {
            if (_entities.ContainsKey(entityID))
            {
                Debug.LogError("entity is existed, entityID = " + entityID.ToString());
                return;
            }
            _entities.Add(entityID, entity);
        }

        public void RemoveEntity(Entity entity)
        {

        }

        public void LogicUpdate()
        {
            foreach(var entity in _entities.Values)
            {
                entity.Update();
            }
            
        }
    }
}