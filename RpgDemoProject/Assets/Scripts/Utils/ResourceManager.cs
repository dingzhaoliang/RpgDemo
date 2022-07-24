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

    }
}