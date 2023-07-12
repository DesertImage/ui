using UnityEngine;

namespace DesertImage.UI
{
    public interface IUISlot
    {
        ITransition ShowTransition { get; }
        ITransition HideTransition { get; }

        Vector2 Position { get; }
        Vector2 Size { get; }
        Vector2 Pivot { get; }
    }
}