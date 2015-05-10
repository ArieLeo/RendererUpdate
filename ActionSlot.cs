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
        private float lerpValue;

        [SerializeField]
        private float lerpSpeed;

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
        public float LerpValue {
            get { return lerpValue; }
            set { lerpValue = value; }
        }

        public float LerpSpeed {
            get { return lerpSpeed; }
            set { lerpSpeed = value; }
        }

    }

}