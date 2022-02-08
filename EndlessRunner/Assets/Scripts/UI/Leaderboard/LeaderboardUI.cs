using System;
using Triplano.Levels;
using Triplano.Screens;
using UnityEngine;
using UnityEngine.UI;

namespace Triplano
{
    public class LeaderboardUI : ScreenObject
    {
        [SerializeField] private ScriptableNumber scoreNumber;
        [SerializeField] private LeaderboardData leaderboardData;
        [SerializeField] private Transform recordsContainer;
        [SerializeField] private Button buttonRestart;
        [SerializeField] private GameEvent loseEvent;
        [SerializeField] private LevelFlow levelFlow;
        [SerializeField] private float delayToAppearScreen = 2f;

        private RecordUI[] recordUIs;

        protected override void Start()
        {
            base.Start();

            recordUIs = recordsContainer.GetComponentsInChildren<RecordUI>(true);

            loseEvent.SubscribeToEvent(ShowScreen);
            buttonRestart.onClick.AddListener(levelFlow.ResetLevel);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            loseEvent.UnsubscribeToEvent(ShowScreen);
        }

        private void UpdateScoresUI()
        {
            if (scoreNumber.SavedValue > 0)
            {
                leaderboardData.TryAddNewScore(scoreNumber.SavedValue);
                scoreNumber.Reset();
            }

            for (int i = 0; i < leaderboardData.UserScores.Count; i++)
            {
                leaderboardData.UpdateList();
                recordUIs[i].SetData(leaderboardData.UserScores[i].Score, leaderboardData.UserScores[i].Position);
            }
        }

        public override void ShowScreen()
        {
            UpdateScoresUI();

            foreach (RecordUI item in recordUIs)
            {
                item.gameObject.SetActive(item.transform.GetSiblingIndex() < leaderboardData.NumberOfRecords);
            }

            Invoke(nameof(Show), delayToAppearScreen);
        }
    }
}
