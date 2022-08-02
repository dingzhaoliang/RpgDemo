using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
namespace RpgDemo
{
    public class ResourceManager : USingleton<ResourceManager>
    {
        public GameObject LoadReource(string path)
        {
            GameObject gameObject;
            gameObject = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            return gameObject;
        }


		public JSONClass LoadJson(string path)
		{
			Object jsonObject;
            jsonObject = AssetDatabase.LoadAssetAtPath<Object>(path);
            JSONClass jsonCfg = JSONNode.Parse(jsonObject.ToString()) as JSONClass;
            return jsonCfg;
		}
	}
}