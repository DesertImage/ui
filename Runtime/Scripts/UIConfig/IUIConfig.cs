using UnityEngine;

namespace DesertImage.UI
{
    public interface IUIConfig
    {
        Canvas MainCanvas { get; }
        IUIElement[] Elements { get; }
    }
}