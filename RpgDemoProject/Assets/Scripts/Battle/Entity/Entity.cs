using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RpgDemo
{
	public class EntityParms
    {
		public EntityKind entityKind;
		public string prefabPath;
    }
	public class Entity
	{
		private EntityKind _entityKind = EntityKind.None;
		private Dictionary<int, IComponent> _componentDic;
		private List<IComponent> _componentList;
		public GameObject GameObject { get; set; }
		public EntityKind Kind { get; set; }

		public void Init()
		{
			_componentDic = new Dictionary<int, IComponent>();
			_componentList = new List<IComponent>();
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
		public virtual void StartAll()
		{
			foreach (var comp in _componentList)
			{
				comp.Start();
			}
		}
		public virtual void Update()
		{
			foreach(var comp in _componentList)
			{
				comp.Update();
			}
		}
	}
}