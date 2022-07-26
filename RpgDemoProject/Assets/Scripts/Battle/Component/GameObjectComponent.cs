using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RpgDemo
{
    public class GameObjectComponentParams : BaseComponentParms
    {
        public string path;
    }

    public class GameObjectComponent : IComponent
	{
        private GameObjectComponentParams componentParams;
        private GameObject _gameObject;
        public override void Init(BaseComponentParms baseComponentParms)
        {
            componentParams = baseComponentParms as GameObjectComponentParams;
        }
        public override void Start()
        {
            LoadPrefabAndInstantiate();
        }

        private void LoadPrefabAndInstantiate()
        {
            var go = ResourceManager.Instance.LoadReource(componentParams.path);
            _gameObject = GameObject.Instantiate(go);
            _gameObject.transform.SetParent(EntityManager.Instance.transform);
        }

        public GameObject GameObject
		{
			get { return _gameObject; }
		}
    }
}