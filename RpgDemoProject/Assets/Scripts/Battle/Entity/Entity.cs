using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RpgDemo
{
	public class EntityParms
    {
		public bool isLocalPlayer;
		public string configName;
    }
	public class Entity
	{
		private EntityKind _entityKind = EntityKind.None;
		private Dictionary<int, IComponent> _componentDic;
		private List<IComponent> _componentList;
		public EntityKind Kind { get { return _entityKind; } private set { }}
		public bool IsLocalPlayer { get; private set; }

		public Entity(EntityKind entityKind, bool isLocalPlayer = false)
		{
			_entityKind = entityKind;
			IsLocalPlayer = isLocalPlayer;
		}

		public void Init()
		{
			_componentDic = new Dictionary<int, IComponent>();
			_componentList = new List<IComponent>();
		}

		public virtual void AttachWorld()
		{
			foreach (var comp in _componentList)
			{
				comp.AttachWorld();
			}
		}

		public virtual void DetachFromWorld()
		{
			foreach (var comp in _componentList)
			{
				comp.DetachFromWorld();
			}
		}

		public void Destroy()
		{
			if(_componentDic != null)
			{
				_componentDic.Clear();
				_componentDic = null;
			}
		}
		public IComponent AttachCompenent(ComponentID componentID, IComponent component)
		{
			if (!_componentDic.ContainsKey((int)componentID))
			{
				component.SetParent(this);
				component.Init(null);
				_componentDic.Add((int)componentID, component);
				_componentList.Add(component);
			}
			return component;
		}
		public IComponent AttachCompenent(ComponentID componentID, IComponent component, BaseComponentParms baseComponentParms)
		{
			if (!_componentDic.ContainsKey((int)componentID))
			{
				component.SetParent(this);
				component.Init(baseComponentParms);
				_componentDic.Add((int)componentID, component);
				_componentList.Add(component);
			}
			return component;
		}
		public IComponent GetCompenent(ComponentID componentID)
		{
			IComponent component = null;
			_componentDic.TryGetValue((int)componentID, out component);
			return component;
		}

		public T GetCompenent<T>(ComponentID componentID) where T:IComponent
		{
			IComponent component = null;
			_componentDic.TryGetValue((int)componentID, out component);
			return component as T;
		}
		public virtual void StartAll()
		{
			for(int i = 0; i < _componentList.Count; i++)
            {
				_componentList[i].Start();

			}
		}
		public virtual void Update()
		{
			for (int i = 0; i < _componentList.Count; i++)
			{
				_componentList[i].Update();

			}
		}
	}
}