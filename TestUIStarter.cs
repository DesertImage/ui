using System.Collections;
using DesertImage.UI;
using UnityEngine;

namespace DefaultNamespace
{
    public class TestUIStarter : MonoBehaviour
    {
        [SerializeField] private UIConfigScriptable config;

        private IUIManager _uiManager;

        private void Awake() => _uiManager = new UIManager(config);

        private IEnumerator Start()
        {
            // _uiManager.ShowPanel<TestPanel, TestPanelSettings>(new TestPanelSettings { Value = "Test" });

            yield break;
            yield return new WaitForSeconds(1f);

            _uiManager.Setup<TestPanel, TestPanelSettings>(new TestPanelSettings { Value = "I've been setuped again" });
            _uiManager.ShowPanel<TestPanelSecond, TestPanelSettings>
            (
                new TestPanelSettings { Value = "I'm a second panel!" }
            );

            yield return new WaitForSeconds(1f);

            _uiManager.HidePanel<TestPanel>();

            yield return new WaitForSeconds(1f);

            _uiManager.ShowPopup<TestPanel, TestPanelSettings>(new TestPanelSettings { Value = "I'm popup now!" });

            yield return new WaitForSeconds(1f);
            
            _uiManager.ShowPopup<TestPanelSecond, TestPanelSettings>(new TestPanelSettings { Value = "I'm popup now too!" });
            
            yield return new WaitForSeconds(1f);
            
            _uiManager.ShowPanel<TestPanel>(1);

            yield return new WaitForSeconds(1f);
            
            _uiManager.ShowPanel<TestPanelSecond>(0);

            yield return new WaitForSeconds(1f);

            _uiManager.HideAll();
        }
    }
}