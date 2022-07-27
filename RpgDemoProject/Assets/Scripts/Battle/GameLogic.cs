using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RpgDemo
{
	public class GameLogic : USingleton<GameLogic>
	{
		private const int frame = 10;
		private readonly float _frameRate = (float)1 / frame;
		private float deltaTime = 0;
		private event Action _visualUpdateEvent;
		private event Action<Command> _localPlayerCommandHandler;
		private Command _command;
		public void RegistVisualUpdateEvent(Action action)
		{
			_visualUpdateEvent += action;
		}

		public void UnregistVisualUpdateEvent(Action action)
		{
			_visualUpdateEvent -= action;
		}

		public void RegistLocalPlayerCommandHandler(Action<Command> action)
		{
			_localPlayerCommandHandler += action;
		}

		public void UnregistLocalPlayerCommandHandler(Action<Command> action)
		{
			_localPlayerCommandHandler -= action;
		}
		private void Awake()
		{
			DontDestroyOnLoad(this);
			deltaTime = 0;
			EntityManager.Instance.Init();
			CameraManager.Instance.Init();
			UIManager.Instance.Init();
		}
		// Start is called before the first frame update
		void Start()
		{
			EntityParms entityParms = new EntityParms() { prefabPath = "Assets/Suriyun/Eri/Prefab/Eri_schooluniform.prefab" , isLocalPlayer = true};
			var entity = EntityFactory.CreatePlayEntity(entityParms);
			CameraManager.Instance.SetTarget(entity.GetCompenent<GameObjectComponent>(ComponentID.GameObject).GameObject.transform);
		}

		// Update is called once per frame
		void Update()
		{
			_visualUpdateEvent?.Invoke();
			UpdateGameInput();

			// 逻辑帧update
			deltaTime = Time.deltaTime + deltaTime;
			while (deltaTime >= _frameRate)
			{
				deltaTime = deltaTime - _frameRate;
				EntityManager.Instance.LogicUpdate();
			}
		}

		void UpdateGameInput()
		{
			_command.Direction = KeyBoardUtils.GetKeyBoardMoveDirection();
			_localPlayerCommandHandler?.Invoke(_command);
		}
	}
}