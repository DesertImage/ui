
namespace DesertImage.UI
{
    public abstract class ScreenAbstract : UIElementAbstract, IScreen
    {
    }
    
    public abstract class ScreenAbstract<T> : ScreenAbstract, IScreen<T> where T : struct
    {
        public abstract void Setup(T settings);
    }
}