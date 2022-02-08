using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Triplano
{
    public class RecordUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private TextMeshProUGUI positionText;

        private int score;
        private int position;

        public void SetData(int score, int position)
        {
            this.score = score;
            this.position = position;

            scoreText.text = $"Score {score}";
            positionText.text = $"{position}.";
        }
    }
}
