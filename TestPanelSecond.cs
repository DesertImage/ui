using DesertImage.UI;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class TestPanelSecond : PanelAbstract<TestPanelSettings>
    {
        [SerializeField] private Text label;

        public override void Setup(TestPanelSettings settings) => label.text = settings.Value;
    }
}