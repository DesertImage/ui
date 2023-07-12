using DesertImage.UI;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class TestPanel : PanelAbstract<TestPanelSettings>
    {
        [SerializeField] private Text label;

        public override void Setup(TestPanelSettings settings) => label.text = settings.Value;
    }

    public struct TestPanelSettings
    {
        public string Value;
    }
}