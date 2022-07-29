using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace RpgDemo
{
	public class CameraManager : USingleton<CameraManager>
	{
		private Camera _mainCamera;
		private CinemachineFreeLook _freeLook;
		private CinemachineBrain _cinemachine;
		private Transform _target;
		private float ValueMul = 30;
		Vector3 pos = Vector3.zero;

		public float Value;
		public Camera MainCamera { get { return _mainCamera; } }

		public void Init()
		{
			_mainCamera = Camera.main;
			_cinemachine = _mainCamera.GetComponent<CinemachineBrain>();
			_freeLook = GameObject.Find("CM FreeLook1").GetComponent<CinemachineFreeLook>();
		}
		public void SetTarget(Transform transform)
        {
			_target = transform;
			_freeLook.LookAt = _target;
			_freeLook.Follow = _target;

        }
		private void Update()
		{
			UpdateNormalRot();
		}

		private void UpdateNormalRot()
		{
			if (Input.GetMouseButtonDown(0))
			{
				Value = 0;
				pos = Input.mousePosition;
			}
			if (Input.GetMouseButton(0))
			{
				var delta = Input.mousePosition - pos;
				//Debug.LogError( delta );
				if (delta.x > 0)
				{
					Value = -1;
					//right
				}
				else if (delta.x < 0)
				{
					Value = 1;
					//left
				}
				else if (delta.x == 0)
				{
					Value = 0;
				}
				pos = Input.mousePosition;
			}
			if (Input.GetMouseButtonUp(0))
			{
				Value = 0;
				pos = Input.mousePosition;
			}

			if (_cinemachine.ActiveVirtualCamera.Equals(_freeLook) && _cinemachine.ActiveBlend == null)
			{
				_freeLook.m_XAxis.TouchInputValue = Value * ValueMul;
			}
		}
	}
}