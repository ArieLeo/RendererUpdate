using System.Reflection;
using Rotorz.ReorderableList;
using UnityEditor;
using UnityEngine;

namespace RendererUpdate {

    [CustomEditor(typeof(MeshRendererUpdate))]
    public sealed class MeshRendererUpdateEditor : Editor {
        #region FIELDS

        private MeshRendererUpdate Script { get; set; }

        #endregion FIELDS

        #region SERIALIZED PROPERTIES

        private SerializedProperty targetGo;
        private SerializedProperty actionSlots;

        #endregion SERIALIZED PROPERTIES

        #region UNITY MESSAGES

        public override void OnInspectorGUI() {
            serializedObject.Update();

            DrawVersionLabel();
            DrawTargetGoField();
            DrawActionSlots();

            serializedObject.ApplyModifiedProperties();
        }
        private void OnEnable() {
            Script = (MeshRendererUpdate)target;

            targetGo = serializedObject.FindProperty("targetGo");
            actionSlots = serializedObject.FindProperty("actionSlots");
        }

        #endregion UNITY MESSAGES

        #region INSPECTOR
        private void DrawActionSlots() {
            ReorderableListGUI.Title("Actions");
            ReorderableListGUI.ListField(actionSlots);
        }

        private void DrawTargetGoField() {
            EditorGUILayout.PropertyField(
                targetGo,
                new GUIContent(
                    "Target",
                    "Game object that contains the renderer to update."));
        }

        private void DrawVersionLabel() {
            EditorGUILayout.LabelField(
                string.Format(
                    "{0} ({1})",
                    MeshRendererUpdate.VERSION,
                    MeshRendererUpdate.EXTENSION));
        }

        #endregion INSPECTOR

        #region METHODS

        [MenuItem("Component/RendererUpdate")]
        private static void AddUpdaterComponent() {
            if (Selection.activeGameObject != null) {
                Selection.activeGameObject.AddComponent(typeof(MeshRendererUpdate));
            }
        }

        #endregion METHODS
    }

}