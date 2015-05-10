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

        #endregion

        #region INSPECTOR FIELDS

        [SerializeField]
        private RendererAction action;

        [SerializeField]
        private BlendMode renderingMode;

        [SerializeField]
        private float lerpValue;

        [SerializeField]
        private float lerpSpeed;

        /// <summary>
        /// Callback executed when <c>LerpAlpha</c> coroutine ends its
        /// execution by itself.
        /// </summary>
        [SerializeField]
        private UnityEvent lerpFinishCallback; 

        #endregion

        #region PROPERTIES
        public GameObject TargetGo {
            get { return targetGo; }
            set { targetGo = value; }
        }

        public RendererAction Action {
            get { return action; }
            set { action = value; }
        }

        public BlendMode RenderingMode {
            get { return renderingMode; }
            set { renderingMode = value; }
        }

        public float LerpValue {
            get { return lerpValue; }
            set { lerpValue = value; }
        }

        public float LerpSpeed {
            get { return lerpSpeed; }
            set { lerpSpeed = value; }
        }

        /// <summary>
        /// Callback executed when <c>LerpAlpha</c> coroutine ends its
        /// execution by itself.
        /// </summary>
        public UnityEvent LerpFinishCallback {
            get { return lerpFinishCallback; }
            set { lerpFinishCallback = value; }
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
            PerformAction();
        }

        private void PerformAction() {
            switch (Action) {
                case RendererAction.SetRenderingMode:
                    Logger.LogCall(this);
                    ApplyRenderingMode(RenderingMode);

                    break;
                case RendererAction.LerpAlpha:
                    Logger.LogCall(this);

                    StartCoroutine(LerpAlpha());

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
        private IEnumerator LerpAlpha() {
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
