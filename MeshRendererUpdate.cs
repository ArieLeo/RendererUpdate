#define DEBUG_LOGGER

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FileLogger;

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
        private RendererType rendererType;

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

        public RendererType RendererType {
            get { return rendererType; }
            set { rendererType = value; }
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
            Logger.LogCall();
            switch (RendererType) {
                case RendererType.MeshRenderer:
                    HandleMeshRenderer(actionSlot);
                    break;
            }
        }

        private void HandleMeshRenderer(ActionSlot actionSlot) {
            switch (actionSlot.Action) {
                case RendererAction.SetRenderingMode:
                    // todo extract
                    var material = GetMaterial(TargetGo, RendererType);

                    Utilities.SetupMaterialWithBlendMode(
                        material,
                        actionSlot.RenderingMode);

                    break;
            }
        }

        private static Material GetMaterial(
            GameObject targetGo,
            RendererType rendererType) {
            switch (rendererType) {

                case RendererType.MeshRenderer:
                    var rendererCo = targetGo.GetComponent<MeshRenderer>();
                    var material = rendererCo.material;

                    return material;
            }

            return null;
        }

        #endregion

    }

}
