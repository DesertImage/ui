using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace DesertImage.UI
{
    public class PopupsLayer : IUILayer<IUIElement>, ISetupable<BlackScreenSettings>, IHideSilently<IUIElement>
    {
        private readonly IBlackScreen _blackScreen;
        private readonly HashSet<IUIElement> _showings;

        private readonly Transform _content;

        public PopupsLayer(Transform parent)
        {
            _showings = new HashSet<IUIElement>();

            var blackScreenObj = "BlackScreen".GetNewTransform(parent).gameObject;
            var blackScreenMono = blackScreenObj.AddComponent<BlackScreenMono>();
            var canvasGroup = blackScreenObj.AddComponent<CanvasGroup>();
            var image = blackScreenObj.AddComponent<Image>();

            blackScreenObj.transform.localScale = Vector3.one * 2f;

            blackScreenMono.Inject(image);
            blackScreenMono.Inject(canvasGroup);

            _blackScreen = blackScreenMono;

            _content = new GameObject("Content").transform;
            _content.SetParent(parent);
        }

        public void Show(IUIElement element)
        {
            if (!_blackScreen.IsShowing)
            {
                _blackScreen.Show();
            }

            // element.SetHierarchyIndex(_blackScreen.HierarchyIndex + _showings.Count + 1);
            element.SetParent(_content);

            if (!element.IsShowing)
            {
                element.Show();
            }

            _showings.Add(element);
        }

        public void Show<TSettings>(IUIElement element, TSettings settings) where TSettings : struct
        {
            var uiElement = (ISetupable<TSettings>)element;
            uiElement.Setup(settings);

            Show(element);
        }

        public bool IsShowing(IUIElement element) => _showings.Contains(element);

        public void Hide(IUIElement element)
        {
            element.Hide();
            _showings.Remove(element);

            if (_showings.Count > 0) return;

            _blackScreen.Hide();
        }

        public void HideAll()
        {
            if (_showings.Count == 0) return;

            foreach (var element in _showings.ToArray())
            {
                Hide(element);
            }

            _blackScreen.Hide();
        }

        public void HideSilently(IUIElement element)
        {
            _showings.Remove(element);

            if (_showings.Count > 0) return;

            _blackScreen.Hide();
        }

        public void Setup(BlackScreenSettings settings) => _blackScreen.Setup(settings);
    }
}