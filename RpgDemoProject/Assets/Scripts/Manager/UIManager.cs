using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RpgDemo
{
	public class UIManager : USingleton<UIManager>
	{
		private Camera _uiCamera;
		private Transform _root;
		private Transform _player3DUI;
		public Transform Player3DUI { get { return _player3DUI; } }
		public void Init()
		{
			_root = GameObject.Find("Canvas").transform;
			_player3DUI = GameObject.Find("Canvas/Player3DUI").transform;
		}
	}
}