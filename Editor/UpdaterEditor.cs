using UnityEditor;
using UnityEngine;

namespace RendererUpdate {

    [CustomEditor(typeof(Updater))]
    public sealed class UpdaterEditor : Editor {
        #region FIELDS

        private Updater Script { get; set; }

        #endregion FIELDS

        #region SERIALIZED PROPERTIES

        private SerializedProperty targetGo;
        private SerializedProperty rendererType;

        #endregion SERIALIZED PROPERTIES

        #region UNITY MESSAGES

        public override void OnInspectorGUI() {
            serializedObject.Update();

            DrawVersionLabel();
            DrawTargetGoField();
            DrawRendererTypeDropdown();

            serializedObject.ApplyModifiedProperties();
        }
        private void OnEnable() {
            Script = (Updater)target;

            targetGo = serializedObject.FindProperty("targetGo");
            rendererType = serializedObject.FindProperty("rendererType");
        }

        #endregion UNITY MESSAGES

        #region INSPECTOR
        private void DrawTargetGoField() {
            EditorGUILayout.PropertyField(
                targetGo,
                new GUIContent(
                    "Target",
                    "Game object that contains the renderer to update."));
        }

        private void DrawRendererTypeDropdown() {
            EditorGUILayout.PropertyField(
                rendererType,
                new GUIContent(
                    "Renderer Type",
                    "Type of the renderer component."));
        }


        private void DrawVersionLabel() {
            EditorGUILayout.LabelField(
                string.Format(
                    "{0} ({1})",
                    Updater.VERSION,
                    Updater.EXTENSION));
        }

        #endregion INSPECTOR

        #region METHODS

        [MenuItem("Component/RendererUpdate")]
        private static void AddUpdaterComponent() {
            if (Selection.activeGameObject != null) {
                Selection.activeGameObject.AddComponent(typeof(Updater));
            }
        }

        #endregion METHODS
    }

}