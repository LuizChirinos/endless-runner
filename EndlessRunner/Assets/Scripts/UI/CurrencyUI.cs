using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Triplano
{
    public class CurrencyUI : MonoBehaviour
    {
        [SerializeField] private ScriptableNumber scriptableNumber;
        [SerializeField] private TextMeshProUGUI currencyText;
        [SerializeField] private int iconNumber;
        private void Start()
        {
            scriptableNumber.OnUpdated += UpdateCurrencyText;

            currencyText.text = $"<sprite={iconNumber}>{scriptableNumber.SavedValue}";
        }

        private void OnDestroy()
        {
            scriptableNumber.OnUpdated -= UpdateCurrencyText;
        }

        private void UpdateCurrencyText(int amount)
        {
            currencyText.text = $"<sprite={iconNumber}>{amount}";
        }

    }
}
