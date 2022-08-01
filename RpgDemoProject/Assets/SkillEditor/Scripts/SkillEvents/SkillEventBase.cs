using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEditor;
using UnityEngine.Timeline;
using System;
using Sirenix.OdinInspector;

namespace RpgDemo
{
    [HideInMenu]
    public class SkillEventBase : PlayableAsset
    {
        [SkillBaseParams]
        [FoldoutGroup("BaseParams")]
        [InlineButton("New")]
        [InlineButton("Copy")]
        [DisplayAsString]
        public long eventID;

        [SkillBaseParams]
        [FoldoutGroup("BaseParams")]
        [InlineButton("Reset")]
        [InlineButton("Paste")]
        [DisplayAsString]
        public long preID;

        [SkillBaseParams]
        [FoldoutGroup("BaseParams")]
        public int loopCount;

        [SkillBaseParams]
        [FoldoutGroup("BaseParams")]
        public float loopInterval;

        [SkillBaseParams]
        [Range(0, 1)]

        [FoldoutGroup("BaseParams")]
        public float prob = 1f;

        [FoldoutGroup("Trigger")]
        [PropertyOrder(1)]
        [HideLabel]
        public EventTriggerManager triggerManager;

		public SkillEventBase()
        {
            NewEventID();
        }

        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            return Playable.Null;
        }

        public void NewEventID()
        {
            eventID = Convert.ToInt64((DateTime.UtcNow - new DateTime(2022, 1, 1, 0, 0, 0, 0)).TotalMilliseconds);
        }

        private void New()
        {
            NewEventID();
        }

        private void Copy()
        {
            GUIUtility.systemCopyBuffer = eventID.ToString();
        }

        private void Paste()
        {
            long id = 0;
            if (long.TryParse(GUIUtility.systemCopyBuffer, out id))
            {
                preID = id;
            }
        }

        private void Reset()
        {
            preID = 0;
        }
    }
}