using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RpgDemo
{
	[CreateAssetMenu(fileName = "New EntityConfig", menuName = "ScriptableObject/EntityConfig")]
	public class EntityConfig : ScriptableObject
	{
		public int templateID;
		public string entityName;
		public string prefabPath;
		public EntityKind entityKind;
		public List<AttributeKeyValue> attributes;
	}

	[Serializable]
	public class AttributeKeyValue
	{
		public AttrID attrID;
		public int value;
	}
}