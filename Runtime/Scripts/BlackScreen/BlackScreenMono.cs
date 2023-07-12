using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace DesertImage.UI
{
    public class BlackScreenMono : MonoBehaviour, IBlackScreen
    {
        public int HierarchyIndex => transform.GetSiblingIndex();
        public bool IsShowing { get; private set; }

        [SerializeField] protected CanvasGroup canvasGroup;
        [SerializeField] private Image image;

        private BlackScreenSettings _settings;

        public void Inject(Image img)
        {
            image = img;
            image.color = _settings.Color;
        }

        public void Inject(CanvasGroup group)
        {
            canvasGroup = group;
            canvasGroup.alpha = IsShowing ? _settings.Alpha : 0f;
            canvasGroup.blocksRaycasts = IsShowing;
        }

        public void Show()
        {
            IsShowing = true;
            canvasGroup.blocksRaycasts = true;
            canvasGroup
                .DOFade(_settings.Alpha, _settings.AlphaAnimationDuration)
                .SetUpdate(true);
        }

        public void Hide()
        {
            canvasGroup
                .DOFade(0f, _settings.AlphaAnimationDuration)
                .OnStepComplete(() =>
                {
                    canvasGroup.blocksRaycasts = false;
                    IsShowing = false;
                })
                .SetUpdate(true);
        }

        public void SetHierarchyIndex(int value) => transform.SetSiblingIndex(value);
        public void SetLastHierarchyIndex() => transform.SetAsLastSibling();

        public void SetParent(Transform parent, bool worldPositionsStay = true)
        {
            transform.SetParent(parent, worldPositionsStay);
        }

        public void Setup(BlackScreenSettings settings)
        {
            var color = settings.Color;
            var alpha = settings.Alpha;

            color.a = alpha;
            image.color = color;

            _settings = settings;
        }
    }
}