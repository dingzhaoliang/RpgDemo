using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RpgDemo
{
	public class MoveComponent : IComponent
	{
		private Transform _transform;
		private bool _moveing;
		private bool _canRotate;
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
			_canRotate = false;
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
			UpdatePosition();
		}

		private void UpdatePosition()
		{
			if (_moveing)
			{
				_curPos = _curPos + Time.deltaTime * _speed * _direction;
				_transform.position = _curPos;
			}
		}

		private void SetRotation()
		{
			if (_canRotate)
			{
				float angle = 90 - Mathf.Atan2(_direction.z, _direction.x) * 180 / Mathf.PI;
				angle = angle % 360;
				_transform.localRotation = Quaternion.Euler(0, angle, 0);
			}
		}

		public void StartMove()
		{
			_moveing = true;
			_canRotate = true;
			_curPos = _transform.position;
		}

		public void UpdateSpeed(float speed)
		{
			_speed = speed;
		}

		public void UpdateDirection(Vector3 direction)
		{
			_direction = direction;
			SetRotation();
		}

		public void StopMove()
		{
			_moveing = false;
			_canRotate = false;
			_speed = 0;
			_direction = Vector3.zero;
		}
	}
}