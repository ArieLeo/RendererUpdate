using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering;

namespace RendererUpdate {

    // todo move to separate file
    public enum RendererType { MeshRenderer }

    public sealed class Updater : MonoBehaviour {

        #region CONSTANTS

        public const string VERSION = "v0.1.0";
        public const string EXTENSION = "RendererUpdate";

        #endregion

        #region EVENTS
        #endregion

        #region FIELDS

        [SerializeField]
        private GameObject targetGo;

        [SerializeField]
        private RendererType rendererType;

        [SerializeField]
        private List<ActionSlot> actions; 

        #endregion

        #region INSPECTOR FIELDS
        #endregion

        #region PROPERTIES
        public GameObject TargetGo {
            get { return targetGo; }
            set { targetGo = value; }
        }

        public RendererType RendererType {
            get { return rendererType; }
            set { rendererType = value; }
        }

        public List<ActionSlot> Actions {
            get { return actions; }
            set { actions = value; }
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
        #endregion

    }

    // todo move to separate file
    public enum RendererAction {

        SetRenderingMode,

        // Increase transparency.
        LerpAlphaIn,

        // Decrease transparency.
        LerpAlphaOut

    }

    // todo move to separate file
    public sealed class ActionSlot {

        private RendererAction action;

        private BlendMode renderingMode;

        private float alphaInValue;

        private float alphaOutValue;

        public RendererAction Action {
            get { return action; }
            set { action = value; }
        }

        public BlendMode RenderingMode {
            get { return renderingMode; }
            set { renderingMode = value; }
        }

        /// <summary>
        /// End alpha value in LerpAlphaIn mode.
        /// </summary>
        public float AlphaInValue {
            get { return alphaInValue; }
            set { alphaInValue = value; }
        }

        /// <summary>
        /// End alpha value in LerpAlphaOut mode.
        /// </summary>
        public float AlphaOutValue {
            get { return alphaOutValue; }
            set { alphaOutValue = value; }
        }

    }

}
