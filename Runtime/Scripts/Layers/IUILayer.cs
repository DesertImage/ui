namespace DesertImage.UI
{
    public interface IUILayer
    {
        void HideAll();
    }

    public interface IUILayer<T> : IUILayer where T : IUIElement
    {
        void Show(T element);
        void Show<TSettings>(T element, TSettings settings) where TSettings : struct;

        bool IsShowing(T element);
        
        void Hide(T element);
    }
}