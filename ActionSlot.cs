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
        private float alphaInValue;

        [SerializeField]
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