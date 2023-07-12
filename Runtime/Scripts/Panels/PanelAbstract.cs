namespace DesertImage.UI
{
    public abstract class PanelAbstract : UIAnimatedElementAbstract, IPanel
    {
    }

    public abstract class PanelAbstract<T> : PanelAbstract, IPanel<T> where T : struct
    {
        public abstract void Setup(T settings);
    }
}