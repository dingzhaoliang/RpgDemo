using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RpgDemo
{
	public enum EntityKind
	{
		None = 0,
		Player = 1 << 0,
		Monster = 1 << 1,
		Bullet = 1 << 2,
	}
}