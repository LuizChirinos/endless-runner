using UnityEngine;

namespace Triplano.Currency
{
    public class CurrencyCollectable : MonoBehaviour
    {
        public delegate void CurrencyEventHandler();

        public CurrencyEventHandler OnCollected;

        [SerializeField] private ScriptableNumber currencyNumber;
        [SerializeField] private int amountOfCurrency = 1;

        private bool collected = false;

        private void OnEnable()
        {
            collected = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (collected)
                return;

            Collect();
        }

        private void Collect()
        {
            currencyNumber.Add(amountOfCurrency);
            gameObject.SetActive(false);
            OnCollected?.Invoke();
        }
    }
}
