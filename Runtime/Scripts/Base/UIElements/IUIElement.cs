namespace DesertImage.UI
{
    public interface IUIElement : IShowable, IHideable, IHierarchyItem
    {
        bool IsShowing { get; }
    }
}