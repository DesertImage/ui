namespace DesertImage.UI
{
    public interface IUIAnimatedElement : IUIElement
    {
        void SetShowTransition(ITransition transition);
        void SetHideTransition(ITransition transition);
    }
}