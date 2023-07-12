using System;
using DG.Tweening;
using UnityEngine;

namespace DesertImage.UI
{
    public class SlideTransition : TransitionAbstract, IInject<Transform>
    {
        [SerializeField] private Direction direction;
        [SerializeField] private float duration;
        [SerializeField] private Ease ease = Ease.OutExpo;

        [Space] [SerializeField] private bool isHideAnimation;

        private RectTransform _rectTransform;
        private Vector2 _defaultPosition;

        private Vector2 _topRightCoord;
        private Vector2 _bottomLeftCoord;

        public void Inject(Transform instance)
        {
            _rectTransform = (RectTransform)instance;
            _defaultPosition = _rectTransform.anchoredPosition;

            var mainCamera = Camera.main;

            RectTransformUtility.ScreenPointToLocalPointInRectangle
            (
                _rectTransform,
                new Vector2(0, 0),
                mainCamera,
                out _bottomLeftCoord
            );

            var size = _rectTransform.rect.size;
            
            _bottomLeftCoord -= size;
            _topRightCoord = _bottomLeftCoord + new Vector2(Screen.width, Screen.height) + size;
        }

        protected override void OnStart()
        {
            var offScreenPosition = GetOffScreenPosition(_defaultPosition);
            var start = isHideAnimation ? _defaultPosition : offScreenPosition;
            var end = isHideAnimation ? offScreenPosition : _defaultPosition;

            _rectTransform
                .DOAnchorPos(end, duration)
                .From(start)
                .SetEase(ease)
                .OnComplete(OnFinish)
                .SetUpdate(true);
        }

        protected override void OnCancel() => _rectTransform.DOKill();

        private Vector2 GetOffScreenPosition(Vector2 start)
        {
            var targetPosition = direction switch
            {
                Direction.Left => new Vector2(_bottomLeftCoord.x, start.y),
                Direction.Right => new Vector2(_topRightCoord.x, start.y),
                Direction.Bottom => new Vector2(start.x, _bottomLeftCoord.y),
                _ => new Vector2(start.x, _topRightCoord.y)
            };

            return targetPosition;
        }
    }
}