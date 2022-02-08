using Triplano.Levels;
using Triplano.Screens;
using UnityEngine;
using UnityEngine.UI;

namespace Triplano
{
    public class Leaderboard : ScreenObject
    {
        [SerializeField] private Button buttonRestart;
        [SerializeField] private GameEvent loseEvent;
        [SerializeField] private LevelFlow levelFlow;
        [SerializeField] private float delayToAppearScreen = 2f;

        protected override void Start()
        {
            base.Start();
            loseEvent.SubscribeToEvent(ShowScreen);
            buttonRestart.onClick.AddListener(levelFlow.ResetLevel);
        }
        protected override void OnDestroy()
        {
            base.OnDestroy();
            loseEvent.UnsubscribeToEvent(ShowScreen);
        }

        public override void ShowScreen()
        {
            Invoke(nameof(Show), delayToAppearScreen);
            
        }
    }
}
