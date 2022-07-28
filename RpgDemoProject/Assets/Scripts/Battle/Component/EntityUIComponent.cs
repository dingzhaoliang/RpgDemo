using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RpgDemo
{
	public class EntityUIComponenttParams : BaseComponentParms
	{
		public string path;
		public EntityUIKind kind;
	}

	public class EntityUIComponent : IComponent
	{
		private EntityUIComponenttParams componentParams;
		private GameObject _gameObject;
		private EntityUI _entityUI;
		private Transform _entityTransform;
		private Vector3 _entityPos;
		private RectTransform _rectTransform;
		private Vector3 _offset = new Vector3(0, 1.6f, 0);
		public override void Init(BaseComponentParms baseComponentParms)
		{
			componentParams = baseComponentParms as EntityUIComponenttParams;
		}
		public override void Start()
		{
			LoadPrefabAndInstantiate();
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
			_entityPos = _entityTransform.position + _offset;
			var pos = UIBehaviourUtil.WorldToLocalPointInRectangle(_entityPos, _rectTransform);
			UIBehaviourUtil.SetRectTransformAnchoredPosition(_gameObject, pos);
		}
		public override void Destroy()
		{
			if (_gameObject != null)
			{
				GameObject.Destroy(_gameObject);
			}
		}

		private void LoadPrefabAndInstantiate()
		{
			var go = ResourceManager.Instance.LoadReource(componentParams.path);
			_gameObject = GameObject.Instantiate(go);

			if (componentParams.kind == EntityUIKind.Player)
			{
				_gameObject.transform.SetParent(UIManager.Instance.GetLayerNode("3DUI").transform);
				_rectTransform = UIManager.Instance.GetLayerNode("3DUI").GetComponent<RectTransform>();
			}
			_gameObject.transform.localScale = Vector3.one;
			_gameObject.transform.position = Vector3.zero;
			_gameObject.transform.localPosition = Vector3.zero;
			var goComp = _entity.GetCompenent<GameObjectComponent>(ComponentID.GameObject);
			if (goComp != null)
			{
				_entityTransform = goComp.GameObject.transform;
			}

			_entityUI = _gameObject.GetComponent<EntityUI>();
		}
	}
}