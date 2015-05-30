using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace RendererUpdateEx {

    [CustomEditor(typeof(RendererUpdate))]
    public sealed class RendererUpdateEditor : Editor {
        #region FIELDS

        private RendererUpdate Script { get; set; }

        #endregion FIELDS

        #region SERIALIZED PROPERTIES

        private SerializedProperty targetGo;
        private SerializedProperty action;
        private SerializedProperty renderingMode;
        private SerializedProperty lerpValue;
        private SerializedProperty lerpSpeed;
        private SerializedProperty lerpFinishCallback;
        private SerializedProperty lerpMethod;
        private SerializedProperty mode;
        private SerializedProperty rendererTag;
        private SerializedProperty onStart;
        private SerializedProperty albedoEffect;

        #endregion SERIALIZED PROPERTIES

        #region UNITY MESSAGES

        public override void OnInspectorGUI() {
            serializedObject.Update();

            DrawVersionLabel();

            EditorGUILayout.Space();

            DrawModeDropdown();
            DrawOnStartToggle();
            HandleDrawTargetGoField();
            HandleDrawTagField();

            HandleDrawActionDropdown();
            HandleDrawAlbedoEffectDropdown();
            HandleDrawRenderingModeDropdown();
            HandleDrawLerpValueSlider();
            HandleDrawLerpSpeedValueField();
            HandleDrawLerpMethodDropdown();

            EditorGUILayout.Space();

            HandleDrawLerpFinishCallback();

            serializedObject.ApplyModifiedProperties();
        }
        private void OnEnable() {
            Script = (RendererUpdate)target;

            targetGo = serializedObject.FindProperty("targetGo");
            action = serializedObject.FindProperty("action");
            renderingMode = serializedObject.FindProperty("renderingMode");
            lerpValue = serializedObject.FindProperty("lerpValue");
            lerpSpeed = serializedObject.FindProperty("lerpSpeed");
            lerpFinishCallback =
                serializedObject.FindProperty("lerpFinishCallback");
            lerpMethod = serializedObject.FindProperty("lerpMethod");
            mode = serializedObject.FindProperty("mode");
            rendererTag =
                serializedObject.FindProperty("rendererTag");
            onStart = serializedObject.FindProperty("onStart");
            albedoEffect = serializedObject.FindProperty("albedoEffect");
        }

        #endregion UNITY MESSAGES

        #region INSPECTOR
        private void HandleDrawTagField() {
            if (mode.enumValueIndex != (int)Mode.Tag) return;

            rendererTag.stringValue = EditorGUILayout.TagField(
                new GUIContent(
                    "Tag",
                    ""),
                rendererTag.stringValue);
        }

        private void DrawOnStartToggle() {
            EditorGUILayout.PropertyField(
                onStart,
                new GUIContent(
                    "On Start",
                    "If true, renderer will be update in Start()."));
        }

        private void HandleDrawAlbedoEffectDropdown() {
            if (action.enumValueIndex
                != (int)RendererAction.ChangeAlbedoColor) {

                return;
            }

            EditorGUILayout.PropertyField(
                albedoEffect,
                new GUIContent(
                    "Albedo Effect",
                    "Action to perform on albedo color property."));
        }

        private void HandleDrawLerpMethodDropdown() {
            if (action.enumValueIndex != (int)RendererAction.LerpAlpha) {
                return;
            }

            EditorGUILayout.PropertyField(
                lerpMethod,
                new GUIContent(
                    "Lerp Method",
                    "Method used to lerp values."));
        }

        private void DrawModeDropdown() {
            EditorGUILayout.PropertyField(
                mode,
                new GUIContent(
                    "Mode",
                    "Get renderer from reference, find by tag or " +
                    "pass it in a method call."));
        }

        private void HandleDrawActionDropdown() {
            EditorGUILayout.PropertyField(
                action,
                new GUIContent(
                    "Action",
                    ""));
        }

        private void HandleDrawRenderingModeDropdown() {
            if (action.enumValueIndex != (int)RendererAction.SetRenderingMode) {
                return;
            }

            EditorGUILayout.PropertyField(
                renderingMode,
                new GUIContent(
                    "Rendering Mode",
                    ""));
        }

        private void HandleDrawLerpValueSlider() {
            if (action.enumValueIndex != (int)RendererAction.LerpAlpha) {
                return;
            }

            lerpValue.floatValue = EditorGUILayout.Slider(
                new GUIContent(
                    "Lerp Value",
                    ""),
                lerpValue.floatValue,
                0,
                1);
        }

        private void HandleDrawLerpSpeedValueField() {
            if (action.enumValueIndex != (int)RendererAction.LerpAlpha) {
                return;
            }

            EditorGUILayout.PropertyField(
                lerpSpeed,
                new GUIContent(
                    "Lerp Speed",
                    ""));
        }

        private void HandleDrawLerpFinishCallback() {
            if (action.enumValueIndex != (int)RendererAction.LerpAlpha) {
                return;
            }

            EditorGUILayout.PropertyField(
                lerpFinishCallback,
                new GUIContent(
                    "Callback",
                    "Callback executed when lerp method ends."));
        }


        private void HandleDrawTargetGoField() {
            if (mode.enumValueIndex != (int) Mode.Reference) return;

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
                    RendererUpdate.VERSION,
                    RendererUpdate.EXTENSION));
        }

        #endregion INSPECTOR

        #region METHODS

        [MenuItem("Component/RendererUpdate")]
        private static void AddUpdaterComponent() {
            if (Selection.activeGameObject != null) {
                Selection.activeGameObject.AddComponent(typeof(RendererUpdate));
            }
        }

        #endregion METHODS
    }

}