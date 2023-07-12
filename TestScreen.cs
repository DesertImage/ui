using DesertImage.UI;

namespace DefaultNamespace
{
    public class TestScreen : ScreenAbstract<TestScreenSettings>
    {
        
        public override void Setup(TestScreenSettings settings)
        {
        }
    }

    public struct TestScreenSettings
    {
        public string Value;
    }
}