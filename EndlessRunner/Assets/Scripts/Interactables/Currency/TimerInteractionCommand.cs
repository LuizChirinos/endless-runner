using System;
using UnityEngine;

namespace Triplano
{
    public class TimerInteractionCommand : InteractionCommand
    {
        [SerializeField] private float lifeTime = 5f;

        protected override void OnEnable()
        {
            OnFinished += DestroyCommand;
            base.OnEnable();
            Invoke(nameof(FinishCommand), lifeTime);
        }

        protected override void OnDisable()
        {
            OnFinished -= DestroyCommand;
            base.OnDisable();
        }

        private void ResetModifier()
        {
            CancelInvoke(nameof(FinishCommand));
            Invoke(nameof(FinishCommand), lifeTime);
        }
    }
}
