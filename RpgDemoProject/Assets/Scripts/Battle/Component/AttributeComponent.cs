using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RpgDemo
{
	public class AttributeComponent : IComponent
	{
		private Dictionary<int, int> attrs;
		public override void Init(BaseComponentParms baseComponentParms)
		{
			attrs = new Dictionary<int, int>();
		}
		public override void Start()
		{

		}
	}
}