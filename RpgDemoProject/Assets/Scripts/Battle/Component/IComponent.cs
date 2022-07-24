using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RpgDemo
{
	public class BaseComponentParms
	{

	}

	public class IComponent
	{
		protected Entity _entity;
		public virtual void Init(BaseComponentParms baseComponentParms)
		{

		}

		public virtual void Start()
		{

		}
		public virtual void SetParent(Entity entity)
		{
			_entity = entity;
		}
		public virtual void Update()
		{

		}
	}
}