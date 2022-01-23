using UnityEngine;

namespace Triplano.Currency
{
    public class CurrencyInteractable : Interactable
    {
        public delegate void CurrencyEventHandler();

        public CurrencyEventHandler OnCurrencyCollected;

        [SerializeField] private ScriptableNumber currencyNumber;
        [SerializeField] private ScriptableNumber currencyModifier;
        [SerializeField] private int amountOfCurrency = 1;

        private void OnEnable()
        {
            collected = false;
            OnInteracted += AddCurrency;
        }

        private void OnDisable()
        {
            OnInteracted -= AddCurrency;
        }

        private void AddCurrency()
        {
            currencyNumber.Add(amountOfCurrency*currencyModifier.SavedValue);
            OnCurrencyCollected?.Invoke();
        }


    }
}
