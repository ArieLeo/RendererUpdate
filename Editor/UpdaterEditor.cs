using UnityEditor;
using UnityEngine;

namespace RendererUpdate {

    [CustomEditor(typeof(Updater))]
    public sealed class UpdaterEditor : Editor {
        #region FIELDS

        private Updater Script { get; set; }

        #endregion FIELDS

        #region SERIALIZED PROPERTIES

        #endregion SERIALIZED PROPERTIES

        #region UNITY MESSAGES

        public override void OnInspectorGUI() {
            serializedObject.Update();

            DrawVersionLabel();

            serializedObject.ApplyModifiedProperties();
        }

        private void OnEnable() {
            Script = (Updater)target;

        }

        #endregion UNITY MESSAGES

        #region INSPECTOR

        private void DrawVersionLabel() {
            EditorGUILayout.LabelField(
                string.Format(
                    "{0} ({1})",
                    Updater.VERSION,
                    Updater.EXTENSION));
        }

        #endregion INSPECTOR

        #region METHODS

        [MenuItem("Component/MyNamespace/Updater")]
        private static void AddUpdaterComponent() {
            if (Selection.activeGameObject != null) {
                Selection.activeGameObject.AddComponent(typeof(Updater));
            }
        }

        #endregion METHODS
    }

}