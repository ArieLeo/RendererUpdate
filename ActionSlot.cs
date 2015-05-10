using UnityEngine;
using UnityEngine.Events;
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

        /// <summary>
        /// Callback executed when <c>LerpAlpha</c> coroutine ends its
        /// execution by itself.
        /// </summary>
        [SerializeField]
        private UnityEvent lerpFinishCallback; 

        public RendererAction Action {
            get { return action; }
            set { action = value; }
        }

        public BlendMode RenderingMode {
            get { return renderingMode; }
            set { renderingMode = value; }
        }

        /// <summary>
        /// End alpha value in LerpAlpha mode.
        /// </summary>
        public float LerpValue {
            get { return lerpValue; }
            set { lerpValue = value; }
        }

        public float LerpSpeed {
            get { return lerpSpeed; }
            set { lerpSpeed = value; }
        }

        public UnityEvent LerpFinishCallback {
            get { return lerpFinishCallback; }
            set { lerpFinishCallback = value; }
        }

    }

}