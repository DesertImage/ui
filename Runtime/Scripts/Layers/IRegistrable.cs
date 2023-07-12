
namespace DesertImage.UI
{
    public interface IRegistrable
    {
        void Register<T>(T element) where T : IUIElement;
    }
}