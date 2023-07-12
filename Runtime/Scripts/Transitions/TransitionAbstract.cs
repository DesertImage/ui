using System;
using UnityEngine;

namespace DesertImage.UI
{
    public abstract class TransitionAbstract : MonoBehaviour, ITransition
    {
        public event Action<ITransition> OnStarted;
        public event Action<ITransition> OnFinished;

        public bool IsInProcess { get; private set; }

        public void Play()
        {
            if (IsInProcess) Cancel();

            IsInProcess = true;
            OnStarted?.Invoke(this);
            OnStart();
        }

        public void Cancel()
        {
            if (!IsInProcess) return;

            IsInProcess = false;
            OnCancel();
        }

        protected abstract void OnStart();
        protected abstract void OnCancel();

        protected void OnFinish()
        {
            IsInProcess = false;
            OnFinished?.Invoke(this);
        }
    }
}