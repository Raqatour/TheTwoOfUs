using System;
using UnityEditor;
using UnityEngine;

namespace Reset_Scene_Transition.Editor
{
    [CustomEditor(typeof(ShatterController))]
    public class ShatterControllerEditor : UnityEditor.Editor
    {
        protected SerializedProperty meshParent;
        protected SerializedProperty sceneMaterial;

        protected ShatterController controller;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (meshParent == null)
            {
                return;
            }

            if (GUILayout.Button("Configure"))
            {
                Transform meshParentTransform = (Transform) meshParent.objectReferenceValue;
                Material shatterMateral = (Material) sceneMaterial.objectReferenceValue;
                meshParentTransform.gameObject.layer = controller.gameObject.layer;
                foreach (Transform transform in meshParentTransform)
                {
                    var glass = transform.GetComponent<ShatteredGlass>();
                    if (glass != null)
                    {
                        continue;
                    }

                    transform.gameObject.AddComponent<ShatteredGlass>();
                    
                    // Assign material
                    MeshRenderer meshRenderer = transform.GetComponent<MeshRenderer>();
                    meshRenderer.material = shatterMateral;
                    
                    // Assign layer
                    transform.gameObject.layer = controller.gameObject.layer;
                }
            }
        }

        protected virtual void OnEnable()
        {
            meshParent = serializedObject.FindProperty("meshParent");
            sceneMaterial = serializedObject.FindProperty("ScreenshotMaterial");
            controller = (ShatterController) target;
        }
    }
}