using UnityEngine;

namespace DesertImage.UI
{
    [CreateAssetMenu(fileName = "UIConfig", menuName = "DesertImage/UI/Config")]
    public class UIConfigScriptable : ScriptableObject, IUIConfig
    {
        public Canvas MainCanvas => mainCanvas;
        public IUIElement[] Elements => elements;

        [SerializeField] private Canvas mainCanvas;
        [Space] [SerializeField] private UIElementAbstract[] elements;
    }
}