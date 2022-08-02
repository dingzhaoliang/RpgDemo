using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RpgDemo {
	[Serializable]
	public class CircleRangeParams
	{
		public bool exceptself = false;
		public bool exceptDestructible = true;
		public RangeDirection direction = RangeDirection.forward;
		public Vector3 pos_offset;
		public float radius;
		public float inner_radius = 0;
	}
}