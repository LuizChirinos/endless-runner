using System;
using System.Collections.Generic;
using UnityEngine;

namespace Triplano
{
    public class EntityInterator : MonoBehaviour, IInteractor
    {
        [SerializeField] private List<TagData> targetTags;
        [SerializeField] private List<InteractionCommand> commands;
        [SerializeField] private Transform commandParent;

        public void Interact(Interactable interactable)
        {
            interactable.ExecuteInteraction(this);
        }

        public void ReceiveCommand(InteractionCommand interactionCommand)
        {
            interactionCommand.transform.SetParent(commandParent);
            interactionCommand.OnFinished += RemoveCommand;
            commands.Add(interactionCommand);
        }
        public void RemoveCommand(InteractionCommand interactionCommand)
        {
            commands.Remove(interactionCommand);
            interactionCommand.OnFinished -= RemoveCommand;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out Interactable interactable))
                return;
            if (!other.TryGetComponent(out TagContainer tagContainer))
                return;
            if (!targetTags.Contains(tagContainer.TagData))
                return;

            Interact(interactable);
        }
    }
}
