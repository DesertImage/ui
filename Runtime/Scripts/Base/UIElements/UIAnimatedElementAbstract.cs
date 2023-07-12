using System;
using UnityEngine;

namespace DesertImage.UI
{
    [RequireComponent(typeof(Canvas))]
    public abstract class UIAnimatedElementAbstract : UIElementAbstract, IUIAnimatedElement
    {
        [Space] [SerializeField] private TransitionAbstract showTransition;
        [SerializeField] private TransitionAbstract hideTransition;

        public override void Initialize()
        {
            base.Initialize();
            SetShowTransition(showTransition);
            SetHideTransition(hideTransition);
        }

        protected virtual void OnDestroy()
        {
            if (showTransition != null)
            {
                showTransition.OnFinished -= OnShowFinished;
            }

            if (hideTransition)
            {
                hideTransition.OnFinished -= OnHideFinished;
            }
        }

        public void SetShowTransition(ITransition transition)
        {
            SetTransition(ref showTransition, transition, OnShowFinished);
        }

        public void SetHideTransition(ITransition transition)
        {
            SetTransition(ref hideTransition, transition, OnHideFinished);
        }

        public override void Show()
        {
            base.Show();

            if (showTransition)
            {
                showTransition.Play();
                return;
            }

            OnShow();
        }

        public override void Hide()
        {
#if DEBUG
            if (!IsShowing) throw new Exception($"{name} is not showing");
#endif
            if (hideTransition)
            {
                hideTransition.Play();
                return;
            }

            OnHide();
        }

        protected virtual void OnShow()
        {
        }

        protected virtual void OnHide()
        {
            canvas.enabled = false;
            IsShowing = false;
        }

        private void SetTransition(ref TransitionAbstract current, ITransition value,
            Action<ITransition> callback)
        {
            if (current)
            {
                current.Cancel();
                current.OnFinished -= callback;
            }

            if (value == null) return;

            current = (TransitionAbstract)value;
            current.OnFinished += callback;

            if (value is IInject<Transform> inject)
            {
                inject.Inject(transform);
            }
        }

        private void OnShowFinished(ITransition obj) => OnShow();
        private void OnHideFinished(ITransition obj) => OnHide();
    }
}