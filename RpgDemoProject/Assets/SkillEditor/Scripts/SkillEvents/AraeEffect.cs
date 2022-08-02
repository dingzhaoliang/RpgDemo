using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using Sirenix.OdinInspector;

namespace RpgDemo
{
	public class AraeEffect : SkillEventBase
	{
		[SkillCustomParams]
		[FoldoutGroup("CounstomParams")]
		public CampType campType = CampType.enemy;

		[SkillCustomParams]
		[FoldoutGroup("CounstomParams")]
		public EntityKind entityKind = EntityKind.Monster;

		[SkillCustomParams]
		[FoldoutGroup("CounstomParams")]
		[MinValue(0)]
		public int searchCount = 1;

		[SkillCustomParams]
		[FoldoutGroup("CounstomParams")]
		[MinValue(0)]
		public int searchRange = 1;

		[SkillCustomParams]
		[FoldoutGroup("CounstomParams")]
		public SearchType searchType = SearchType.self;

		[SkillCustomParams]
		[FoldoutGroup("CounstomParams")]
		public CollideType collideType = CollideType.circle;

		[SkillCustomParams]
		[FoldoutGroup("CounstomParams")]
		[ShowIf("collideType", CollideType.circle)]
		[LabelText("rangeParams")]
		public CircleRangeParams circleRangeParams;

		[SkillCustomParams]
		[FoldoutGroup("CounstomParams")]
		[ShowIf("collideType", CollideType.obb)]
		[LabelText("rangeParams")]
		public OBBRangeParams oBBRangeParams;

		[SkillCustomParams]
		[FoldoutGroup("CounstomParams")]
		[ShowIf("collideType", CollideType.sector)]
		[LabelText("rangeParams")]
		public SectorRangeParams sectorRangeParams;
	}
}

