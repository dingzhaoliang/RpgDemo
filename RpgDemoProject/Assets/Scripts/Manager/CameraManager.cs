using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RpgDemo
{
	public class CameraManager : USingleton<CameraManager>
	{
		private Camera _mainCamera;
		private Transform _target;
		public Camera MainCamera { get { return _mainCamera; } }

		public void Init()
		{
			_mainCamera = Camera.main;
		}
		public void SetTarget(Transform transform)
        {
			_target = transform;
        }
		private void LateUpdate()
		{
			if(_target != null && _mainCamera != null)
			{
				_mainCamera.transform.LookAt(_target);
			}
		}
	}
}