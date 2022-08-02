using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace RpgDemo {
	[Serializable]
	public class SectorRangeParams
	{
		public bool exceptself = false;
		public bool exceptDestructible = true;
		public RangeDirection direction = RangeDirection.forward;
		public Vector3 pos_offset;
		public float angle_offset;
		[Title("扇形扫掠半角")]
		public float theta;
		[Title("扇形边长")]
		public float l;
		public float inner_l = 0;
	}
}