using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RpgDemo
{
	public class AnimationComponent : IComponent
	{
		private Animator _animator;
		public override void Init(BaseComponentParms baseComponentParms)
		{
		}
		public override void Start()
		{
			var goComp = _entity.GetCompenent<GameObjectComponent>(ComponentID.GameObject);
			if (goComp != null)
			{
				goComp.GameObject.TryGetComponent<Animator>(out _animator);
			}
		}
		public void PlayAnimation(EntityAnimationInteger integer)
		{
			if (_animator == null)
				return;
			_animator.SetInteger("animation", (int)integer);
		}
	}
}