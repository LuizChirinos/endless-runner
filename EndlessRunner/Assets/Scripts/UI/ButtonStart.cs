using Triplano.Levels;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Triplano.UI
{
    [RequireComponent(typeof(Button))]
    public class ButtonStart : MonoBehaviour
    {
        [SerializeField] private LevelFlow levelFlow;
        private Button button;
        void Start()
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(PressedStart);
        }
        private void OnDestroy()
        {
            button.onClick.RemoveListener(PressedStart);
        }

        private void PressedStart()
        {
            levelFlow.NextLevel();
        }
    }
}
