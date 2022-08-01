using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using Sirenix.OdinInspector;

namespace RpgDemo
{
	public class PlayAnimation : SkillEventBase
	{
		[SkillCustomParams]
		[FoldoutGroup("CounstomParams")]
		public string animation;
	}
}