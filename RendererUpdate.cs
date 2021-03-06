﻿using UnityEngine;
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

#pragma warning disable 0414
        /// <summary>
        ///     Allows identify component in the scene file when reading it with
        ///     text editor.
        /// </summary>
        [SerializeField]
        private string componentName = "MyClass";
#pragma warning restore0414
 
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

        [SerializeField]
        private AlbedoEffect albedoEffect;

        [SerializeField]
        private Color startColor;

        [SerializeField]
        private Color endColor;

        [SerializeField]
        private float duration;

        [SerializeField]
        private string description = "Description";
 
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

        public AlbedoEffect AlbedoEffect {
            get { return albedoEffect; }
            set { albedoEffect = value; }
        }

        public Color StartColor {
            get { return startColor; }
            set { startColor = value; }
        }

        public Color EndColor {
            get { return endColor; }
            set { endColor = value; }
        }

        public float Duration {
            get { return duration; }
            set { duration = value; }
        }

        public string Description {
            get { return description; }
            set { description = value; }
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

        public void UpdateRenderer(GameObject go) {
            var material = Utilities.GetMaterial(go);

            if (material == null) return;

            StartCoroutine(PingPongAlbedo(material));
        }

        public void UpdateRenderer(RaycastHit hitInfo) {
            var go = hitInfo.transform.gameObject;
            var material = Utilities.GetMaterial(go);

            if (material == null) return;

            StartCoroutine(PingPongAlbedo(material));
        }

        private IEnumerator PingPongAlbedo(Material material) {
            var time = 0f;

            while (true) {
                var lerp = Mathf.PingPong(time, duration);
                material.color = Color.Lerp(startColor, endColor, lerp);

                // Break after pong.
                if (time >= duration * 2) break;

                time += Time.deltaTime;

                yield return null;
            }
        }


        private IEnumerator LerpAlbedo(Material material) {
            while (true) {
                material.color =
                    Color.Lerp(material.color, EndColor, Time.deltaTime);

                if (material.color == EndColor) break;

                yield return null;
            }
        }

        private void HandleRendererAction() {
            switch (Action) {
                case RendererAction.SetRenderingMode:
                    ApplyRenderingMode(RenderingMode);

                    break;
                case RendererAction.LerpAlpha:
                    StartCoroutine(LerpAlpha(LerpHandler));

                    break;
                case RendererAction.ChangeAlbedoColor:

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
