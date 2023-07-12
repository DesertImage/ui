using System;

namespace DesertImage.UI
{
    public interface ITransition
    {
        event Action<ITransition> OnStarted;
        event Action<ITransition> OnFinished;

        bool IsInProcess { get; }

        void Play();
        void Cancel();
    }
}