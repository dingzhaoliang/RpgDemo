using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RpgDemo
{
	public enum CollideType
	{
		circle = 1 << 0,
		obb = 1 << 1,
		sector = 1 << 2,
	}

	public enum RangeExcept
	{
		none = 1<<0,
		self = 1<<1,
	}

	public enum RangeDirection
	{
		forward = 0, //目标方向
		attackDir = 1, //攻击方向
	}
}