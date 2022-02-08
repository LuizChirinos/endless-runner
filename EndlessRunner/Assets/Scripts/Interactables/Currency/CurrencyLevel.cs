using System;
using System.Collections;
using System.Collections.Generic;
using Triplano;
using Triplano.Levels;
using UnityEngine;

public class CurrencyLevel : MonoBehaviour
{
    [SerializeField] private GameEvent loseEvent;
    [SerializeField] private ScriptableNumber currencyGame;
    [SerializeField] private ScriptableNumber currencyLevel;

    private void Start()
    {
        loseEvent.SubscribeToEvent(ReceiveCurrency);
    }

    private void OnDestroy()
    {
        loseEvent.UnsubscribeToEvent(ReceiveCurrency);
        currencyLevel.Reset();
    }

    private void ReceiveCurrency()
    {
        currencyGame.Add(currencyLevel.SavedValue);
    }

}
