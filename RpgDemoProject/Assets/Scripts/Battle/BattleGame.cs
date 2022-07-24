using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RpgDemo
{
    public class BattleGame : MonoBehaviour
    {
        private const int frame = 10;
        private readonly float _frameRate = (float)1 / frame;
        private float deltaTime = 0;
        private void Awake()
        {
            DontDestroyOnLoad(this);
            deltaTime = 0;
            EntityManager.Instance.Init();
        }
        // Start is called before the first frame update
        void Start()
        {
            EntityFactory.CreatePlayEntity();
        }

        // Update is called once per frame
        void Update()
        {
            deltaTime = Time.deltaTime + deltaTime;
            while (deltaTime >= _frameRate)
            {
                deltaTime = deltaTime - _frameRate;
                EntityManager.Instance.LogicUpdate();
            }
        }
    }
}