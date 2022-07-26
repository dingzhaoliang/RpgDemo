using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RpgDemo
{
	public class MoveComponent : IComponent
	{
		private Transform _transform;
		private bool _moveing;
		private float _speed;
		private Vector3 _direction;
		private Vector3 _curPos;
		public override void Init(BaseComponentParms baseComponentParms)
		{

		}
		public override void Start()
		{
			var goComp = _entity.GetCompenent<GameObjectComponent>(ComponentID.GameObject);
			if (goComp != null)
			{
				_transform = goComp.GameObject.transform;
			}
			_moveing = false;
			_curPos = _transform.position;
		}

		public override void AttachWorld()
		{
			GameLogic.Instance.RegistVisualUpdateEvent(OnVisualUpdate);
		}

		public override void DetachFromWorld()
		{
			GameLogic.Instance.UnregistVisualUpdateEvent(OnVisualUpdate);
		}

		private void OnVisualUpdate()
		{
			if (_moveing)
			{
				_curPos = _curPos + Time.deltaTime * _speed * _direction;
				_transform.position = _curPos;
			}
		}

		public void StartMove()
		{
			_moveing = true;
			_curPos = _transform.position;
		}

		public void UpdateSpeed(float speed)
		{
			_speed = speed;
		}

		public void UpdateDirection(Vector3 direction)
		{
			_direction = direction;
		}

		public void StopMove()
		{
			_moveing = false;
			_speed = 0;
			_direction = Vector3.zero;
		}
	}
}