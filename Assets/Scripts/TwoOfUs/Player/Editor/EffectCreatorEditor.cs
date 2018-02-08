using UnityEditor;
using UnityEngine;

namespace TwoOfUs.Player.Editor
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(EffectCreator))]
    public class EffectCreatorEditor : UnityEditor.Editor
    {
        private EffectCreator creator;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Spawn Effect"))
            {
                creator.Spawn();
            }
        }

        protected virtual void OnEnable()
        {
            creator = (EffectCreator) target;
        }
    }
}