using System.Collections;
using System.Collections.Generic;
using Triplano.Screens;
using UnityEngine;

namespace Triplano
{
    public class Leaderboard : ScreenObject
    {
        [SerializeField] private GameEvent loseEvent;

        protected override void Start()
        {
            base.Start();
            loseEvent.SubscribeToEvent(Show);
        }
        protected override void OnDestroy()
        {
            base.OnDestroy();
            loseEvent.UnsubscribeToEvent(Show);
        }
    }
}
