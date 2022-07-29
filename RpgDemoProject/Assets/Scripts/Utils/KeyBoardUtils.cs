using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RpgDemo
{
	public class KeyBoardUtils
	{

		public static Vector3 GetMoveDirection()
		{
			Vector3 forward = CameraManager.Instance.MainCamera.transform.forward;
			forward.y = 0;
			Quaternion inputFrame = Quaternion.LookRotation(forward.normalized, Vector3.up);
			Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
			return inputFrame * input;
		}
		public static Vector3 GetKeyBoardMoveDirection()
		{
			bool keyUp = false;
			bool keyDown = false;
			bool keyLeft = false;
			bool keyRight = false;
			Vector3 getDirection = Vector3.zero;

			if (Input.GetKey(KeyCode.W))
			{
				keyUp = true;
			}
			if (Input.GetKeyUp(KeyCode.W))
			{
				keyUp = false;
			}
			if (Input.GetKey(KeyCode.S))
			{
				keyDown = true;
			}
			if (Input.GetKeyUp(KeyCode.S))
			{
				keyDown = false;
			}
			if (Input.GetKey(KeyCode.A))
			{
				keyLeft = true;
			}
			if (Input.GetKeyUp(KeyCode.A))
			{
				keyLeft = false;
			}
			if (Input.GetKey(KeyCode.D))
			{
				keyRight = true;
			}
			if (Input.GetKeyUp(KeyCode.D))
			{
				keyRight = false;
			}

			if (keyUp)
			{
				if (keyLeft)
				{
					getDirection = GetDirection(135);
				}
				else if (keyRight)
				{
					getDirection = GetDirection(45);
				}
				else
				{
					getDirection = GetDirection(90);
				}
			}
			else if (keyDown)
			{
				if (keyLeft)
				{
					getDirection = GetDirection(225);
				}
				else if (keyRight)
				{
					getDirection = GetDirection(315);
				}
				else
				{
					getDirection = GetDirection(270);
				}
			}
			else if (keyLeft)
			{
				getDirection = GetDirection(180);
			}
			else if (keyRight)
			{
				getDirection = GetDirection(360);
			}

			return getDirection;
		}

		public static Vector3 GetDirection(float angle)
		{
			Vector3 getDirection = Vector3.zero;
			getDirection.x = (float)Math.Cos(angle * Math.PI / 180);
			getDirection.z = (float)Math.Sin(angle * Math.PI / 180);
			return getDirection;
		}
	}
}