using System;
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
        [SerializeField] private float delayToHighlight = 1f;

        private Animator anim;
        private int popHash = Animator.StringToHash("pop");
        private int score;
        private int position;

        private void OnEnable()
        {
            anim = GetComponent<Animator>();
        }

        public int Score { get => score; }
        public int Position { get => position; }

        public void SetData(int score, int position)
        {
            this.score = score;
            this.position = position;

            scoreText.text = $"Score {score}";
            positionText.text = $"{position}.";
        }

        public  void Highlight()
        {
            anim.SetTrigger(popHash);
        }
    }
}
