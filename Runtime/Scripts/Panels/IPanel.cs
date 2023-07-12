namespace DesertImage.UI
{
    public interface IPanel : IUIElement
    {
    }

    public interface IPanel<T> : IPanel, ISetupable<T> where T : struct
    {
    }
}