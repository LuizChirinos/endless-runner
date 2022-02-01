using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Damages
{
    public class Health : MonoBehaviour
    {
        public delegate void OnLifeEvent();
        public delegate void OnLifeChanged(float lifeValue);

        public static event Action<Health> OnAnyDeath;
        public OnLifeEvent OnDeath;
        public UnityEvent OnDamagedUnityEvent;
        public UnityEvent OnDeathUnityEvent;

        public OnLifeChanged OnLifeSet;
        public OnLifeChanged OnDamageTaken;

        [SerializeField] protected float maxLife = 3;
        [SerializeField] protected float currentLife = 0;
        [SerializeField] protected bool isInvincible = false;
        [SerializeField] protected bool deactivateOnDeath = false;
        [Space(10)]
        [Header("Die Fields")]
        [SerializeField] protected float dieLifetime = 1f;

        public float MaxLife { get => maxLife; }
        public float CurrentLife { get => currentLife; }
        public bool IsDead { get => currentLife <= 0; }
        public bool IsInvincible { get => isInvincible; }

        protected virtual void OnEnable()
        {
            currentLife = maxLife;
            OnLifeSet?.Invoke(currentLife);
        }

        protected IEnumerator DieCoroutine()
        {
            float elapsedTime = 0;

            while (elapsedTime < dieLifetime)
            {
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            if(deactivateOnDeath)
                gameObject.SetActive(false);
        }

        public bool Damage(float damageAmount)
        {
            if (IsDead)
                return false;
            if (IsInvincible)
                return false;
            if (damageAmount <= 0)
                return false;

            OnDamagedUnityEvent?.Invoke();

            float damageTaken = Mathf.Clamp(damageAmount, 0f, currentLife);

            currentLife -= damageAmount;
            currentLife = Mathf.Clamp(currentLife, 0f, MaxLife);

            OnLifeSet?.Invoke(currentLife);
            OnDamageTaken?.Invoke(damageTaken);

            if (IsDead)
            {
                StartCoroutine(DieCoroutine());
                OnAnyDeath?.Invoke(this);
                OnDeath?.Invoke();
                OnDeathUnityEvent?.Invoke();
            }

            return true;
        }

        [ContextMenu("Kill")]
        public void Kill()
        {
            Damage(MaxLife);
        }

    }
}
