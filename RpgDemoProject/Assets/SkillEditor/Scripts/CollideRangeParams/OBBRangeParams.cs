using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace RpgDemo {
	[Serializable]
	public class OBBRangeParams
	{
		public bool exceptself = false;
		public bool exceptDestructible = true;
		public RangeDirection direction = RangeDirection.forward;
		public Vector3 pos_offset;
		public float angle_offset;
		[Title("半长")]
		public float half_l;
		[Title("半高")]
		public float half_h;
	}
}