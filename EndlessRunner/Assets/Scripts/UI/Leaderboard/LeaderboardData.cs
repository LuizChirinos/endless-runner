using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Triplano
{
    [CreateAssetMenu(fileName = nameof(LeaderboardData), menuName = "Screens/LeadearboardData")]
    public class LeaderboardData : ScriptableObject
    {
        [SerializeField] private int maxNumberOfScores = 5;

        [SerializeField] private List<UserRecordScore> userScores;

        public List<UserRecordScore> UserScores { get => userScores; }

        public int NumberOfRecords { get => PlayerPrefs.GetInt(NumberOfRecordsKey); set => PlayerPrefs.SetInt(NumberOfRecordsKey, value); }

        public int GetRecordByIndex(int index) => PlayerPrefs.GetInt(GetRecordKey(index));

        private string NumberOfRecordsKey { get => name + " records"; }

        private string GetRecordKey(int index) => $"{name} records {index}";


        private bool HasReachedMaxRecords()
        {
            return NumberOfRecords >= maxNumberOfScores;
        }

        public void UpdateList()
        {
            userScores = new List<UserRecordScore>(NumberOfRecords);
            for (int i = 0; i < NumberOfRecords; i++)
            {
                UserRecordScore newRecord = new UserRecordScore();
                newRecord.Score = GetRecordByIndex(i);
                userScores.Add(newRecord);
                UpdatePosition(i);
            }
        }

        private void AddScore(int score, int index)
        {
            if (NumberOfRecords == 0)
            {
                SaveScore(score, index);
                NumberOfRecords++;
                UpdateList();
            }
            else
            {
                TryReplacePreviousRecord(score);
            }

        }

        private void SaveScore(int score, int index)
        {
            PlayerPrefs.SetInt(GetRecordKey(index), score);
        }
        private void SaveAllScores()
        {
            for (int i = 0; i < NumberOfRecords; i++)
            {
                SaveScore(userScores[i].Score, i);
                UpdatePosition(i);
            }
        }

        private void UpdatePosition(int i)
        {
            userScores[i].Position = i + 1;
        }

        private void TryReplacePreviousRecord(int score)
        {
            UpdateList();

            for (int i = 0; i < NumberOfRecords; i++)
            {
                if (score == userScores[i].Score)
                    return;

                UserRecordScore newRecord = new UserRecordScore();
                newRecord.Score = score;
                if (score > userScores[i].Score)
                {

                    if (NumberOfRecords < maxNumberOfScores)
                        NumberOfRecords++;

                    userScores.Insert(i, newRecord);

                    if (userScores.Count > maxNumberOfScores)
                        userScores.RemoveAt(userScores.Count - 1);

                    SaveAllScores();
                    return;
                }
                else if(i == NumberOfRecords - 1 && !HasReachedMaxRecords())
                {
                    userScores.Add(newRecord);
                    NumberOfRecords++;
                    SaveAllScores();
                    return;
                }
            }
        }

        public void TryAddNewScore(int score)
        {
            if (HasReachedMaxRecords())
            {
                TryReplacePreviousRecord(score);
            }
            else
                AddScore(score, NumberOfRecords);
        }

        public void Clear()
        {
            userScores = new List<UserRecordScore>();
            for (int i = 0; i < NumberOfRecords; i++)
            {
                PlayerPrefs.DeleteKey(GetRecordKey(i));
            }
            PlayerPrefs.DeleteKey(NumberOfRecordsKey);
        }
    }
}
