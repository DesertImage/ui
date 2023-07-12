using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using Object = UnityEngine.Object;

namespace DesertImage.UI
{
    public class UIManager : IUIManager
    {
        private readonly Dictionary<int, IUIElement> _elements = new Dictionary<int, IUIElement>();
        private readonly Dictionary<IPanel, PanelsLayer> _panelShowingLayers = new Dictionary<IPanel, PanelsLayer>();

        private readonly ScreensLayer _screensLayer;
        private readonly PopupsLayer _popupsLayer;
        private PanelsLayer[] _panelsLayers;

        private readonly Transform _panelsTransform;
        private readonly Transform _inactiveTransform;

        public UIManager(IUIConfig config)
        {
            var mainCanvas = Object.Instantiate(config.MainCanvas);
            mainCanvas.name = "[UI]";

            var main = mainCanvas.transform;

            _inactiveTransform = "Inactive".GetNewTransform(main);
            _panelsTransform = "[PANELS]".GetNewTransform(main);
            var screensTransform = "[SCREENS]".GetNewTransform(main);
            var popupsTransform = "[POPUPS]".GetNewTransform(main);

            _panelsLayers = new PanelsLayer[1];
            _panelsLayers[0] = new PanelsLayer(_panelsTransform);

            _screensLayer = new ScreensLayer(screensTransform);

            _popupsLayer = new PopupsLayer(popupsTransform);
            _popupsLayer.Setup
            (
                new BlackScreenSettings
                {
                    Alpha = .7f,
                    Color = Color.black,
                    AlphaAnimationDuration = .8f
                }
            );

            foreach (var element in config.Elements)
            {
                Register((UIElementAbstract)element);
            }
        }

        public void Register<T>(T element) where T : Object, IUIElement
        {
            var type = element.GetType();

            var genericMethod = GetType()
                .GetMethod("GetId", BindingFlags.Static | BindingFlags.NonPublic)!
                .MakeGenericMethod(type);

            var id = (int)genericMethod.Invoke(this, null);
            // var id = GetId<T>();

            if (_elements.ContainsKey(id))
            {
#if DEBUG
                Debug.LogError($"[UIManager] {typeof(T).Name} already registered");
#endif
                return;
            }

            var uiElement = Object.Instantiate(element, _inactiveTransform);

            if (uiElement is IInitializable initializable)
            {
                initializable.Initialize();
            }

            _elements.Add(id, uiElement);
        }

        #region [SCREENS]

        public void ShowScreen<T>() where T : IScreen => _screensLayer.Show((IScreen)_elements[GetId<T>()]);

        public void ShowScreen<TScreen, TSettings>(TSettings settings) where TScreen : IScreen where TSettings : struct
        {
            var screen = (IScreen<TSettings>)_elements[GetId<TScreen>()];
            screen.Setup(settings);

            _screensLayer.Show(screen);
        }

        public void HideScreen<T>() where T : IScreen => _screensLayer.Hide((IScreen)_elements[GetId<T>()]);

        #endregion

        #region [PANELS]

        public void ShowPanel<T>(int priority = 0) where T : IPanel
        {
            var panel = (IPanel)_elements[GetId<T>()];
            ShowPanelProcess(panel, priority);
        }

        public void ShowPanel<TPanel, TSettings>(TSettings settings, int priority = 0)
            where TPanel : IPanel where TSettings : struct
        {
            var panel = (IPanel<TSettings>)_elements[GetId<TPanel>()];
            panel.Setup(settings);

            ShowPanelProcess(panel, priority);
        }

        public void HidePanel<T>() where T : IPanel
        {
            var panel = (IPanel)_elements[GetId<T>()];

            _panelShowingLayers[panel].Hide(panel);
            _panelShowingLayers.Remove(panel);
        }

        #endregion

        #region [POPUPS]

        public void ShowPopup<T>() where T : IUIElement => ShowPopupProcess(_elements[GetId<T>()]);

        public void ShowPopup<TPopup, TSettings>(TSettings settings) where TPopup : IUIElement where TSettings : struct
        {
            var element = _elements[GetId<TPopup>()];
            var setup = (ISetupable<TSettings>)element;

            setup.Setup(settings);

            ShowPopupProcess(element);
        }

        public void ShowPopup<T>(BlackScreenSettings blackScreenSettings) where T : IUIElement
        {
            _popupsLayer.Setup(blackScreenSettings);
            ShowPopup<T>();
        }

        public void ShowPopup<TPopup, TSettings>(TSettings settings, BlackScreenSettings blackScreenSettings)
            where TPopup : IUIElement where TSettings : struct
        {
            _popupsLayer.Setup(blackScreenSettings);
        }

        public void HidePopup<T>() where T : IUIElement => _popupsLayer.Hide(_elements[GetId<T>()]);

        #endregion

        public void Setup<TElement, TSettings>(TSettings settings) where TElement : IUIElement where TSettings : struct
        {
            var element = (ISetupable<TSettings>)_elements[GetId<TElement>()];
            element.Setup(settings);
        }

        public void HideAll()
        {
            _screensLayer.HideAll();
            _popupsLayer.HideAll();

            for (var i = 0; i < _panelsLayers.Length; i++)
            {
                _panelsLayers[i]?.HideAll();
            }
        }

        private void ShowPanelProcess(IPanel panel, int priority = 0)
        {
            var layer = GetPanelsLayer(priority);

            if (_panelShowingLayers.TryGetValue(panel, out var showingLayer))
            {
                showingLayer.HideSilently(panel);
                _panelShowingLayers[panel] = layer;
            }
            else
            {
                _panelShowingLayers.Add(panel, layer);
            }

            if (_popupsLayer.IsShowing(panel))
            {
                _popupsLayer.HideSilently(panel);
            }

            layer.Show(panel);
        }

        private void ShowPopupProcess(IUIElement element)
        {
            if (element is IPanel panel)
            {
                if (_panelShowingLayers.TryGetValue(panel, out var showingLayer))
                {
                    showingLayer.HideSilently(panel);
                }
            }

            _popupsLayer.Show(element);
        }

        private PanelsLayer GetPanelsLayer(int priority)
        {
            var length = _panelsLayers.Length;

            if (length <= priority)
            {
                Array.Resize(ref _panelsLayers, length << 1);
            }

            return _panelsLayers[priority] ?? (_panelsLayers[priority] = new PanelsLayer(_panelsTransform));
        }

        private static int GetId<T>()
        {
            var id = UIElementId<T>.Id;
            if (id == -1)
            {
                id = UIElementId<T>.Id += ++UIElementsCounter.Counter;
            }

            return id;
        }
    }
}