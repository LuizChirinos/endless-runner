using UnityEngine;
using UnityEngine.Events;

namespace Triplano
{
    public class InteractionCommand : MonoBehaviour
    {
        public delegate void CommandEventHandler(InteractionCommand command);
        public CommandEventHandler OnStarted;
        public CommandEventHandler OnRestarted;
        public CommandEventHandler OnStopped;
        public CommandEventHandler OnFinished;

        [SerializeField] private UnityEvent OnStartedUnityEvent;
        [SerializeField] private UnityEvent OnRestartedUnityEvent;
        [SerializeField] private UnityEvent OnStoppedUnityEvent;
        [SerializeField] private UnityEvent OnFinishedUnityEvent;
        [SerializeField] private bool destroyWhenStarted = true;

        private bool isFinished = false;
        private bool started = false;

        public bool IsFinished { get => isFinished; }
        public bool Started { get => started; }

        protected virtual void OnEnable()
        {
            StartCommand();
        }

        protected virtual void OnDisable()
        {
            FinishCommand();
        }

        protected void AskForDestroyingCommand(InteractionCommand interactionCommand)
        {
            if (destroyWhenStarted)
                DestroyCommand(interactionCommand);
        }

        protected virtual void DestroyCommand(InteractionCommand interactionCommand)
        {
            Destroy(interactionCommand.gameObject);
        }

        public virtual void StartCommand()
        {
            isFinished = false;
            started = true;
            OnStarted?.Invoke(this);
            OnStartedUnityEvent?.Invoke();
            AskForDestroyingCommand(this);
        }

        public virtual void StopCommand()
        {
            if (isFinished)
                return;
            OnStopped?.Invoke(this);
            OnStoppedUnityEvent?.Invoke();
        }

        public virtual void RestartCommand()
        {
            OnRestarted?.Invoke(this);
            OnRestartedUnityEvent?.Invoke();
        }

        public virtual void FinishCommand()
        {
            started = false;
            isFinished = true;
            OnFinished?.Invoke(this);
            OnFinishedUnityEvent?.Invoke();
        }
    }
}
