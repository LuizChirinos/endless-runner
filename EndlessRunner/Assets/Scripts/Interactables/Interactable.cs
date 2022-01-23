using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Triplano
{
    public class Interactable : MonoBehaviour
    {
        public delegate void InteractableEventHandler();

        public InteractableEventHandler OnInteracted;

        [SerializeField] protected bool canInteractMultipleTimes = false;
        [SerializeField] protected bool disableOnInteraction = true;
        [SerializeField] protected InteractionCommand interactionCommand;
        protected bool collected = false;
        public virtual bool ExecuteInteraction(IInteractor actor)
        {
            if (collected && !canInteractMultipleTimes)
                return false;

            OnInteracted?.Invoke();
            if (interactionCommand)
                actor.ReceiveCommand(Instantiate(interactionCommand));

            gameObject.SetActive(!disableOnInteraction);

            return true;
        }
    }
}
