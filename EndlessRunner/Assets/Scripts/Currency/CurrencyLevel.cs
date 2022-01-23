using System.Collections;
using System.Collections.Generic;
using Triplano;
using Triplano.Levels;
using UnityEngine;

public class CurrencyLevel : MonoBehaviour
{
    [SerializeField] private LevelFlow levelFlow;
    [SerializeField] private ScriptableNumber currencyLevel;

    private void OnDestroy()
    {
        currencyLevel.Reset();
    }
}
