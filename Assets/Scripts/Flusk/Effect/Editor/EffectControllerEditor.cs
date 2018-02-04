using UnityEditor;
using UnityEngine;

namespace Flusk.Effect.Editor
{
    [CustomEditor(typeof(EffectController))]
    public class EffectControllerEditor : UnityEditor.Editor
    {
        private EffectController effect;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            GUILayout.Space(10);
            if (GUILayout.Button("Activate Sparkle"))
            {
                effect.Sparkle(true);
            }

            if (GUILayout.Button("Deactivate Sparkle"))
            {
                effect.Sparkle(false);
            }
        }

        private void OnEnable()
        {
            effect = (EffectController) target;
        }
    }
}