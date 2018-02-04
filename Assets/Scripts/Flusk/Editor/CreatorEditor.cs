using UnityEditor;
using UnityEngine;

namespace Flusk.Editor
{
    [CustomEditor(typeof(Creator))]
    public class CreatorEditor : UnityEditor.Editor
    {
        private Creator creator;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Toggle Ignite"))
            {
                creator.IsIgnited = !creator.IsIgnited;
            }
        }

        private void OnEnable()
        {
            creator = (Creator) target;
        }
    }
}