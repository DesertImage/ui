using System.Linq;
using UnityEngine;

namespace DesertImage.UI
{
    [RequireComponent(typeof(RectTransform))]
    public class UISlotMono : MonoBehaviour, IUISlot
    {
        public ITransition ShowTransition => showTransition;
        public ITransition HideTransition => hideTransition;

        public Vector2 Position => ((RectTransform)transform).anchoredPosition;
        public Vector2 Size => ((RectTransform)transform).sizeDelta;
        public Vector2 Pivot => ((RectTransform)transform).pivot;

        [SerializeField] private TransitionAbstract showTransition;
        [SerializeField] private TransitionAbstract hideTransition;

        private void OnValidate()
        {
            if (!showTransition) showTransition = GetComponent<TransitionAbstract>();
            if (!hideTransition) hideTransition = GetComponents<TransitionAbstract>()?.LastOrDefault();
        }
    }
}