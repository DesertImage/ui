using UnityEngine;

namespace DesertImage.UI
{
    public class ScreensLayer : IUILayer<IScreen>
    {
        private IScreen _current;

        private readonly Transform _content;

        public ScreensLayer(Transform parent) => _content = parent;

        public void Show(IScreen element) => ShowScreen(element);

        public void Show<TSettings>(IScreen element, TSettings settings) where TSettings : struct
        {
            var screen = (IScreen<TSettings>)element;
            screen.Setup(settings);

            ShowScreen(screen);
        }

        public bool IsShowing(IScreen element) => _current == element;

        public void Hide(IScreen element) => HideScreen(element);
        public void HideAll() => HideScreen(_current);

        private void ShowScreen(IScreen screen)
        {
            Hide(_current);

            screen.SetParent(_content);
            screen.Show();
            
            _current = screen;
        }

        private void HideScreen(IHideable screen)
        {
            if (screen == null) return;
            if (screen != _current) return;

            screen.Hide();
            
            _current = null;
        }
    }
}