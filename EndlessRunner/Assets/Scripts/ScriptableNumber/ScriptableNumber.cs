using System;
using UnityEngine;

namespace Triplano
{
    [CreateAssetMenu(fileName = nameof(ScriptableNumber), menuName = "Triplano/ScriptableNumber")]
    public class ScriptableNumber : ScriptableObject
    {
        public delegate void OnUpdateEvent(int amount);

        public event OnUpdateEvent OnUpdated;

        [SerializeField] private int initialValue = 0;

        public int SavedValue 
        {
            get => PlayerPrefs.GetInt(saveKey, initialValue);
            set
            {
                PlayerPrefs.SetInt(saveKey, value);
                OnUpdated?.Invoke(value);
            }
        }
        private string saveKey => name;

        public void Reset()
        {
            OnUpdated?.Invoke(initialValue);
            PlayerPrefs.DeleteKey(saveKey);
        }
        public void Add(int amount) => SavedValue += amount;
        public void Remove(int amount) => SavedValue -= amount;

    }
}
