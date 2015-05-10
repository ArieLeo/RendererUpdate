#define DEBUG_LOGGER

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using FileLogger;
using uFAction;
using UnityEngine.Events;
using Vexe.Runtime.Extensions;

namespace RendererUpdate {

    public sealed class MeshRendererUpdate : MonoBehaviour {

        #region CONSTANTS

        public const string VERSION = "v0.1.0";
        public const string EXTENSION = "RendererUpdate";
        public const float FloatPrecision = 0.01f;

        #endregion

        #region EVENTS
        #endregion

        #region FIELDS

        [SerializeField]
        private GameObject targetGo;

        [SerializeField]
        private List<ActionSlot> actionSlots;

        #endregion

        #region INSPECTOR FIELDS
        #endregion

        #region PROPERTIES
        public GameObject TargetGo {
            get { return targetGo; }
            set { targetGo = value; }
        }

        public List<ActionSlot> ActionSlots {
            get { return actionSlots; }
            set { actionSlots = value; }
        }

        #endregion

        #region UNITY MESSAGES

        private void Awake() { }

        private void FixedUpdate() { }

        private void LateUpdate() { }

        private void OnEnable() { }

        private void Reset() { }

        private void Start() { }

        private void Update() { }

        private void Validate() { }
        #endregion

        #region EVENT INVOCATORS
        #endregion

        #region EVENT HANDLERS
        #endregion

        #region METHODS

        public void UpdateRenderer() {
            foreach (var actionSlot in ActionSlots) {
                PerformAction(actionSlot);
            }
        }

        private void PerformAction(ActionSlot actionSlot) {
            switch (actionSlot.Action) {
                case RendererAction.SetRenderingMode:
                    Logger.LogCall(this);
                    ApplyRenderingMode(actionSlot.RenderingMode);

                    break;
                case RendererAction.LerpAlpha:
                    Logger.LogCall(this);

                    StartCoroutine(LerpAlpha(
                        actionSlot.LerpFinishCallback,
                        actionSlot.LerpValue,
                        actionSlot.LerpSpeed));

                    break;
            }
        }

        private void ApplyRenderingMode(BlendMode renderingMode) {
            var material = Utilities.GetMaterial(TargetGo);

            Utilities.SetupMaterialWithBlendMode(
                material,
                renderingMode);
        }

        /// <summary>
        /// Lerp alpha of the renderer's material to a specified value.
        /// </summary>
        /// <param name="lerpValue"></param>
        /// <returns></returns>
        private IEnumerator LerpAlpha(
            UnityEvent lerpFinishCallback,
            float lerpValue, float lerpSpeed) {
            var material = Utilities.GetMaterial(TargetGo);

            // Exit if material doesn't have color property.
            var endValueReached = !material.HasProperty("_Color");

            while (!endValueReached) {
                Logger.LogString("{1}, lerp alpha: {0}",
                    material.color.a,
                    endValueReached);

                endValueReached = Utilities.FloatsEqual(
                    material.color.a,
                    lerpValue,
                    FloatPrecision);

                 var lerpedAlpha = Mathf.Lerp(
                    material.color.a,
                    lerpValue,
                    lerpSpeed * Time.deltaTime);

                material.color = new Color(
                    material.color.r,
                    material.color.g,
                    material.color.b,
                    lerpedAlpha);

                yield return null;
            }

            lerpFinishCallback.Invoke();
        }

        #endregion

    }

}
