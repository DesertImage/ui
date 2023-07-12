namespace DesertImage.UI
{
    public interface IInject<T>
    {
        void Inject(T instance);
    }
}