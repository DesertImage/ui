using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DesertImage.UI
{
    public class PanelsLayer : IUILayer<IPanel>, IHideSilently<IPanel>
    {
        private static int _id;

        private readonly HashSet<IPanel> _showings = new HashSet<IPanel>();
        private readonly Transform _content;

        public PanelsLayer(Transform transform)
        {
            _content = $"Layer {_id}".GetNewTransform(transform);
            _id++;
        }

        public void Show(IPanel element)
        {
            element.SetParent(_content);

            if (!element.IsShowing)
            {
                element.Show();
            }

            _showings.Add(element);
        }

        public void Show<TSettings>(IPanel element, TSettings settings) where TSettings : struct
        {
            var panel = (IPanel<TSettings>)element;
            panel.Setup(settings);

            Show(element);
        }

        public bool IsShowing(IPanel element) => _showings.Contains(element);

        public void Hide(IPanel element)
        {
            element.Hide();
            _showings.Remove(element);
        }

        public void HideSilently(IPanel element) => _showings.Remove(element);

        public void HideAll()
        {
            if (_showings.Count == 0) return;

            foreach (var panel in _showings.ToArray())
            {
                Hide(panel);
            }
        }
    }
}