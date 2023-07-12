using UnityEditor;
using UnityEngine;

namespace DesertImage.UI.Editor
{
    [CustomEditor(typeof(TransitionAbstract), true)]
    public class UIAnimatedElementEditor : UnityEditor.Editor
    {
        private ITransition _target;

        private void OnEnable() => _target = (ITransition)target;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Play"))
            {
                _target.Play();
            }
        }
    }
}