using UnityEngine;

namespace Triplano
{
    [System.Serializable]
    public class UserRecordScore
    {
        [SerializeField] private int position;
        [SerializeField] private int score;

        public int Position { get => position; set => position = value; }
        public int Score { get => score; set => score = value; }
    }
}
