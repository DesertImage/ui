using UnityEngine;

namespace DesertImage.UI
{
    public interface IUIManager
    {
        void Register<T>(T element) where T : Object, IUIElement;

        void ShowScreen<T>() where T : IScreen;
        void ShowScreen<TScreen, TSettings>(TSettings settings) where TScreen : IScreen where TSettings : struct;
        void HideScreen<T>() where T : IScreen;

        void ShowPanel<T>(int priority = 0) where T : IPanel;

        void ShowPanel<TPanel, TSettings>(TSettings settings, int priority = 0)
            where TPanel : IPanel where TSettings : struct;

        void HidePanel<T>() where T : IPanel;

        void ShowPopup<T>() where T : IUIElement;
        void ShowPopup<TPopup, TSettings>(TSettings settings) where TPopup : IUIElement where TSettings : struct;
        void ShowPopup<T>(BlackScreenSettings blackScreenSettings) where T : IUIElement;

        void ShowPopup<TPopup, TSettings>(TSettings settings, BlackScreenSettings blackScreenSettings)
            where TPopup : IUIElement where TSettings : struct;

        void HidePopup<T>() where T : IUIElement;

        void Setup<TElement, TSettings>(TSettings settings) where TElement : IUIElement where TSettings : struct;

        void HideAll();
    }
}