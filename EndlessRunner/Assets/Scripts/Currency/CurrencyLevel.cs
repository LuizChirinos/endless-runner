using System;
using System.Collections;
using System.Collections.Generic;
using Triplano;
using Triplano.Levels;
using UnityEngine;

public class CurrencyLevel : MonoBehaviour
{
    [SerializeField] private LevelFlow levelFlow;
    [SerializeField] private ScriptableNumber currencyGame;
    [SerializeField] private ScriptableNumber currencyLevel;

    private void Start()
    {
        levelFlow.OnWon += ReceiveCurrency;
    }

    private void OnDestroy()
    {
        levelFlow.OnWon -= ReceiveCurrency;
        currencyLevel.Reset();
    }

    private void ReceiveCurrency()
    {
        currencyGame.Add(currencyLevel.SavedValue);
        currencyLevel.Reset();
    }

}
