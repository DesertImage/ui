using UnityEngine;

namespace DesertImage.UI
{
    internal static class CanvasExtensions
    {
        internal static void SetStretch(this RectTransform rectTransform)
        {
            rectTransform.anchoredPosition = Vector2.zero;
            
            rectTransform.anchorMin = Vector2.zero;
            rectTransform.anchorMax = Vector2.one;
            
            rectTransform.offsetMin = Vector2.zero;
            rectTransform.offsetMax = Vector2.zero;
        }
        
        internal static RectTransform GetNewTransform(this string name)
        {
            var obj = new GameObject(name, typeof(RectTransform));

            var rectTransform = (RectTransform)obj.transform;
            rectTransform.SetStretch();

            return rectTransform;
        }
        
        internal static RectTransform GetNewTransform(this string name, Transform parent)
        {
            var transform = name.GetNewTransform();
            transform.SetParent(parent, false);
            return transform;
        }
    }
}