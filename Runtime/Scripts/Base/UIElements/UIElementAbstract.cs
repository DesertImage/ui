using System;
using UnityEngine;

namespace DesertImage.UI
{
    [RequireComponent(typeof(Canvas))]
    public abstract class UIElementAbstract : MonoBehaviour, IUIElement, IInitializable
    {
        public int HierarchyIndex => transform.GetSiblingIndex();
        public bool IsShowing { get; protected set; }

        [SerializeField] protected Canvas canvas;

        public virtual void Initialize() => canvas.enabled = false;

        public virtual void Show()
        {
#if DEBUG
            if (IsShowing) throw new Exception($"{name} already showing");
#endif
            canvas.enabled = true;
            IsShowing = true;
        }

        public virtual void Hide()
        {
#if DEBUG
            if (!IsShowing) throw new Exception($"{name} is not showing");
#endif
            canvas.enabled = false;
            IsShowing = false;
        }

        public void SetHierarchyIndex(int value) => transform.SetSiblingIndex(value);
        public void SetLastHierarchyIndex() => transform.SetAsLastSibling();

        public void SetParent(Transform parent, bool worldPositionsStay = true)
        {
            transform.SetParent(parent, worldPositionsStay);
        }

        protected virtual void OnValidate()
        {
            if (!canvas) canvas = GetComponent<Canvas>();
        }
    }
}