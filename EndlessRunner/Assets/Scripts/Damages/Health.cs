using System;
using System.Collections;
using UnityEngine;

namespace Damages
{
    public class Health : MonoBehaviour
    {
        public delegate void OnLifeEvent();
        public delegate void OnLifeChanged(float lifeValue);

        public static event Action<Health> OnAnyDeath;
        public OnLifeEvent OnDeath;
        public OnLifeEvent OnHit;

        public OnLifeChanged OnLifeSet;
        public OnLifeChanged OnDamageTaken;

        [SerializeField] protected float maxLife = 3;
        [SerializeField] protected float currentLife = 0;
        [SerializeField] protected bool isInvincible = false;
        [SerializeField] protected bool deactivateOnDeath = false;
        [Space(10)]
        [Header("Die Fields")]
        [SerializeField] protected GameObject hitEffect;
        [SerializeField] protected GameObject deathEffect;
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

            if (deathEffect)
                Instantiate(deathEffect, transform.position, Quaternion.identity);

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

            float damageTaken = Mathf.Clamp(damageAmount, 0f, currentLife);

            currentLife -= damageAmount;
            currentLife = Mathf.Clamp(currentLife, 0f, MaxLife);

            OnLifeSet?.Invoke(currentLife);
            OnDamageTaken?.Invoke(damageTaken);

            if (hitEffect)
                Instantiate(hitEffect, transform.position, Quaternion.identity);

            if (IsDead)
            {
                StartCoroutine(DieCoroutine());
                OnAnyDeath?.Invoke(this);
                OnDeath?.Invoke();
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
