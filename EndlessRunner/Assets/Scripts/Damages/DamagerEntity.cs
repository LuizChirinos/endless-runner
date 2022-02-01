using UnityEngine;
using UnityEngine.Events;

namespace Damages
{
    public class DamagerEntity : MonoBehaviour
    {
        public delegate void OnDamagerEvent();

        public OnDamagerEvent OnCollided;
        public UnityEvent OnCollidedUnityEvent;

        [SerializeField] protected TagData targetTagData;
        [SerializeField] private float damageAmount = 1f;
        [SerializeField] private bool disappearsOnCollision = true;
        [SerializeField] private bool instaKill;

        public virtual float DamageAmount { get => damageAmount; }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out Health health))
                return;
            if (!other.TryGetComponent(out TagContainer tagContainer))
                return;
            if (tagContainer.TagData != targetTagData)

                return;
            if (instaKill)
                health.Kill();
            else
                health.Damage(damageAmount);
            OnCollided?.Invoke();

            if (disappearsOnCollision)
                gameObject.SetActive(false);
        }
    }
}
