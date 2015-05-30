using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine.Events;

namespace RendererUpdateEx {

    public sealed class RendererUpdate : MonoBehaviour {

        #region CONSTANTS

        public const string VERSION = "v0.1.0";
        public const string EXTENSION = "RendererUpdate";
        public const float FloatPrecision = 0.01f;

        #endregion

        #region DELEGATES

        /// <summary>
        /// Method used to lerp values.
        /// </summary>
        /// <param name="current"></param>
        /// <param name="target"></param>
        /// <param name="speed"></param>
        /// <returns></returns>
        public delegate float Lerp(
            float current,
            float target,
            float speed);

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

        /// <summary>
        /// Method used to lerp values.
        /// </summary>
        [SerializeField]
        private LerpMethod lerpMethod;

        [SerializeField]
        private Mode mode;

        [SerializeField]
        private string rendererTag;

        [SerializeField]
        private bool onStart;

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

        public Lerp LerpHandler { get; set; }

        /// <summary>
        /// Callback executed when <c>LerpAlpha</c> coroutine ends its
        /// execution by itself.
        /// </summary>
        public UnityEvent LerpFinishCallback {
            get { return lerpFinishCallback; }
            set { lerpFinishCallback = value; }
        }

        public LerpMethod LerpMethod {
            get { return lerpMethod; }
            set { lerpMethod = value; }
        }

        /// <summary>
        /// Specifies how to obtain the renderer reference.
        /// </summary>
        public Mode Mode {
            get { return mode; }
            set { mode = value; }
        }

        /// <summary>
        /// Tag used to find target renderer.
        /// </summary>
        public string RendererTag {
            get { return rendererTag; }
            set { rendererTag = value; }
        }

        /// <summary>
        /// If true, renderer will be update in the Start().
        /// </summary>
        public bool OnStart {
            get { return onStart; }
            set { onStart = value; }
        }

        #endregion

        #region UNITY MESSAGES

        private void Awake() {
            SelectLerpMethod();
        }

        private void SelectLerpMethod() {

            switch (LerpMethod) {
                case LerpMethod.Lerp:
                    LerpHandler = Mathf.Lerp;
                    break;
                case LerpMethod.MoveTowards:
                    LerpHandler = Mathf.MoveTowards;
                    break;
            }
        }

        private void FixedUpdate() { }

        private void LateUpdate() { }

        private void OnEnable() { }

        private void Reset() { }

        private void Start() { }

        private void Update() { }

        private void OnValidate() {
            // Limit lerp speed.
            if (LerpSpeed < 0) {
                LerpSpeed = 0;
            }
        }
        #endregion

        #region EVENT INVOCATORS
        #endregion

        #region EVENT HANDLERS
        #endregion

        #region METHODS

        public void UpdateRenderer() {
            HandleRendererAction();
        }

        private void HandleRendererAction() {
            switch (Action) {
                case RendererAction.SetRenderingMode:
                    ApplyRenderingMode(RenderingMode);

                    break;
                case RendererAction.LerpAlpha:

                    StartCoroutine(LerpAlpha(LerpHandler));

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
        /// <returns></returns>
        private IEnumerator LerpAlpha(Lerp lerp) {
            var material = Utilities.GetMaterial(TargetGo);

            // Exit if material doesn't have color property.
            var endValueReached = !material.HasProperty("_Color");

            while (!endValueReached) {
                endValueReached = Utilities.FloatsEqual(
                    material.color.a,
                    lerpValue,
                    FloatPrecision);

                 var lerpedAlpha = lerp(
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
