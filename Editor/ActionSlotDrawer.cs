using UnityEditor;
using UnityEngine;

namespace RendererUpdate {

    [CustomPropertyDrawer(typeof(ActionSlot))]
    public sealed class ActionSlotDrawer : PropertyDrawer {

        #region CONSTANTS

        // Hight of a single property.
        private const int PropHeight = 16;

        // Margin between properties.
        private const int PropMargin = 4;

        // Space between rows.
        private const int RowSpace = 8;

        // Number of rows.
        private const int Rows = 1;

        #endregion

        #region UNITY METHODS

        public override float GetPropertyHeight(
            SerializedProperty property,
            GUIContent label) {

            // Calculate property height.
            return base.GetPropertyHeight(property, label)
                   * Rows // Each row is 16 px high.
                   + (Rows - 1) * RowSpace;
        }

        public override void OnGUI(
            Rect pos,
            SerializedProperty prop,
            GUIContent label) {

            var action = prop.FindPropertyRelative("action");
            var renderingMode =
                prop.FindPropertyRelative("renderingMode");
            var alphaInValue =
                prop.FindPropertyRelative("alphaInValue");
            var alphaOutValue =
                prop.FindPropertyRelative("alphaOutValue");

            DrawActionDropdown(pos, action);
        }

        #endregion

        #region METHODS

        private void DrawActionDropdown(
            Rect pos,
            SerializedProperty prop) {

            EditorGUI.PropertyField(
                new Rect(
                    pos.x,
                    pos.y,
                    pos.width,
                    PropHeight),
                prop,
                new GUIContent("Action Type", ""));
        }

        #endregion

    }

}