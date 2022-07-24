using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RpgDemo
{
    public abstract class USingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T instance;

        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    T[] objs = GameObject.FindObjectsOfType<T>();
                    if (objs.Length > 0)
                    {
                        instance = objs[0];
                        for (int i = 1; i < objs.Length; i++)
                        {
                            GameObject.Destroy(objs[i].gameObject); //去除重复
                        }
                    }
                    else
                    {
                        GameObject newObj = new GameObject(typeof(T).Name); //自动创建 object
                        DontDestroyOnLoad(newObj); //生命周期跨场景
                        instance = newObj.AddComponent<T>();
                    }
                }

                return instance;
            }
        }
    }
}