using System.Collections;
using System.Collections.Generic;
using Triplano.Screens;
using UnityEngine;

namespace Triplano
{
    public class Leaderboard : ScreenObject
    {
        [SerializeField] private GameEvent loseEvent;
        [SerializeField] private float delayToAppear = 2f;

        protected override void Start()
        {
            base.Start();
            loseEvent.SubscribeToEvent(ShowScreen);
        }
        protected override void OnDestroy()
        {
            base.OnDestroy();
            loseEvent.UnsubscribeToEvent(ShowScreen);
        }

        public override void ShowScreen()
        {
            Invoke(nameof(Show), delayToAppear);
            
        }
    }
}
