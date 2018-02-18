using UnityEditor;
using UnityEngine;

namespace TwoOfUs.Player.Editor
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(ControllerCreator))]
    public class EffectCreatorEditor : UnityEditor.Editor
    {
        private ControllerCreator creator;

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
            creator = (ControllerCreator) target;
        }
    }
}