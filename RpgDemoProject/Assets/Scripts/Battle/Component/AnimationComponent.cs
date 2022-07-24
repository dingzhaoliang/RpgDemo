using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RpgDemo
{
	public class AnimationComponent:IComponent
	{
        private Animator _animator;
        public override void Init(BaseComponentParms baseComponentParms)
        {
        }
        public override void Start()
        {
            _entity.GameObject.TryGetComponent<Animator>(out _animator);
        }
        public void PlayAnimation()
        {
            if (_animator == null)
                return;
        }
    }
}