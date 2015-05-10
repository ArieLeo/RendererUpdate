using UnityEngine;
using UnityEngine.Rendering;

namespace RendererUpdate {

    [System.Serializable]
    public sealed class ActionSlot {

        [SerializeField]
        private RendererAction action;

        [SerializeField]
        private BlendMode renderingMode;

        [SerializeField]
        private float lerpInValue;

        [SerializeField]
        private float lerpOutValue;

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
        public float LerpInValue {
            get { return lerpInValue; }
            set { lerpInValue = value; }
        }

        /// <summary>
        /// End alpha value in LerpAlphaOut mode.
        /// </summary>
        public float LerpOutValue {
            get { return lerpOutValue; }
            set { lerpOutValue = value; }
        }

    }

}