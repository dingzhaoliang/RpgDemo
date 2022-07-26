using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RpgDemo
{
	public struct Command
	{
		public Vector3 Direction;
	}
	public enum ActionState
	{
		None = 0,
		Idle,
		Move,
		Attatck,
	}

	public enum EntityAnimationInteger
	{
		None = 0,
		IdleA = 1,
		IdleB = 2,
		Walk = 6,
		IdleF = 28,
	}
}