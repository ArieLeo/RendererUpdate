#define DEBUG_LOGGER

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using FileLogger;
using uFAction;

namespace RendererUpdate {

    public sealed class MeshRendererUpdate : MonoBehaviour {

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
            Material material;

            switch (actionSlot.Action) {
                case RendererAction.SetRenderingMode:
                    Logger.LogCall(this);
                    // todo extract
                    material = Utilities.GetMaterial(TargetGo);

                    Utilities.SetupMaterialWithBlendMode(
                        material,
                        actionSlot.RenderingMode);

                    break;
                case RendererAction.LerpAlphaIn:
                    Logger.LogCall(this);

                    StartCoroutine(LerpAlpha(actionSlot.LerpValue));

                    break;
            }
        }

        /// <summary>
        /// Lerp alpha of the renderer's material to a specified value.
        /// </summary>
        /// <param name="lerpValue"></param>
        /// <returns></returns>
        private IEnumerator LerpAlpha(float lerpValue) {
            var material = Utilities.GetMaterial(TargetGo);
            var endValueReached = false;

            while (!endValueReached) {
                Logger.LogString("{1}, lerp alpha: {0}",
                    material.color.a,
                    endValueReached);

                endValueReached = Utilities.FloatsEqual(
                    material.color.a,
                    lerpValue,
                    0.01f);

                 var lerpedAlpha = Mathf.Lerp(
                    material.color.a,
                    lerpValue,
                    // todo create inspector field: Lerp Speed
                    0.01f);

                material.color = new Color(
                    material.color.r,
                    material.color.g,
                    material.color.b,
                    lerpedAlpha);

                yield return null;
            }
        }

        #endregion

    }

}
