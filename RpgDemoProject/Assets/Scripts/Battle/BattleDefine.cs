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

	public enum AttrID
	{
		maxHp = 1,
		curHp,
		physical_atk,
		magic_atk,
	}

	public enum CampType
	{
		enemy = 1 << 0,
		ally = 1 << 1,
		all = 1 << 2,
	}

	public enum SearchType
	{
		self = 1 << 0,
	}
}