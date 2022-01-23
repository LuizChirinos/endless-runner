namespace Triplano
{
    public interface IInteractor
    {
        public delegate void InteractorEventHandler();

        public void Interact(Interactable interactable);
        public void ReceiveCommand(InteractionCommand interactionCommand);
        public void RemoveCommand(InteractionCommand interactionCommand);
    }
}