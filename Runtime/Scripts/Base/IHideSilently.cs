namespace DesertImage.UI
{
    public interface IHideSilently<T> where T : IUIElement
    {
        void HideSilently(T element);
    }
}