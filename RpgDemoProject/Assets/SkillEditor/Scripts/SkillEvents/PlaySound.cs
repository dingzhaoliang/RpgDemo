using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using Sirenix.OdinInspector;

namespace RpgDemo
{
	public class PlaySound : SkillEventBase
	{
		[SkillCustomParams]
		[FoldoutGroup("CounstomParams")]
		public string animation;
	}
}

