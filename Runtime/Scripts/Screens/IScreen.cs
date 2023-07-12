namespace DesertImage.UI
{
    public interface IScreen : IUIElement
    {
    }

    public interface IScreen<T> : IScreen, ISetupable<T> where T : struct
    {
    }
}