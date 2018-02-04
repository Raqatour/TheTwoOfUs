using System;
using Flusk.Effect.Particles;
using UnityEditor;
using UnityEngine;

namespace Flusk.Effect.Editor
{
    [CustomEditor(typeof(SparkleManager))]
    public class SparkleManagerEditor : UnityEditor.Editor
    {
        private SparkleManager sparkle;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            if (GUILayout.Button("Activate Sparkle"))
            {
                sparkle.SetSparkle(true);
            }

            if (GUILayout.Button("Deactivate Sparkle"))
            {
                sparkle.SetSparkle(false);
            }
        }

        private void OnEnable()
        {
            sparkle = (SparkleManager) target;
        }
    }
}