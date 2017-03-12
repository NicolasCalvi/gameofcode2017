using BlackGearVR.Core;
using System.Collections.Generic;
using UnityEngine;

namespace BlackGearVR
{
    /// <summary>
    /// Gestion du curseur dans le monde 3D
    /// </summary>
    public class GVRGazeCursor : MonoBehaviour
    {
        #region Propriétés

        /// <summary>
        /// Obtient la liste des providers
        /// </summary>
        public List<IGVRInteraction> Providers
        {
            get
            {
                return (this._interactionProviders);
            }
        }

        #endregion

        #region Variables Unity

        [Header("General")]
        public OVRCameraRig CameraController = null;
        public GameObject Ring = null;
        public float PointerDistance = 1f;

        [Header("Detection")]
        public bool Active = false;
        public float Range = 20f;

        #endregion

        #region Variables d'instances

        private bool _isInitialized;
        private Renderer _ringRenderer;
        private IGVRInteraction _current;
        private List<IGVRInteraction> _interactionProviders;

        #endregion

        #region Fonctions privées

        /// <summary>
        /// Détermine si le collider est gérer par pro
        /// </summary>
        /// <param name="collider">Instance de l'objet de collision</param>
        /// <returns>Instance du provider, NULL sinon</returns>
        private IGVRInteraction IsInteractiveItem(Collider collider)
        {
            // On recherche dans les providers a disposition

            foreach (IGVRInteraction provider in this._interactionProviders)
            {
                if (provider.IsInteractiveItem(collider))
                    return (provider);
            }

            // Aucun élément trouvé

            return (null);
        }

        /// <summary>
        /// Se produit sur un click du Touchpad
        /// </summary>
        /// <param name="sender">Source de l'appel</param>
        /// <param name="e">Argument de l'appel</param>
        private void OnTouchpadClick(object sender, GVRTouchpadEventArgs e)
        {
            // Vérification

            if (this._current == null)
                return;

            // Traitement de l'information du Touchpad

            this._current.ProcessTouchpad(e.Type, e.Move);
        }

        /// <summary>
        /// Vérifie l'état du ring
        /// </summary>
        private void ProcessRing()
        {

            // Vérification

            if (!this.Ring || this._ringRenderer == null || this._current == null)
                return;

            // Recherche des options

            GVRGazeCursorOptions options = this._current.GetOptions();

            if (options == null)
                return;

            // Activation du ring

            bool active = this._current != null && options.IsResetReady();
            this.Ring.SetActive(active);

            // Mise à jour du clip 

            if (active)
                this._ringRenderer.material.SetFloat("_Range", options.GetRingStep());
        }

        #endregion

        #region Fonctions Unity

        /// <summary>
        /// Initialisation du GameObject
        /// </summary>
        private void Start()
        {
            // Initialisation

            this._interactionProviders = new List<IGVRInteraction>();
            this._interactionProviders.Add(new GVRInteractionGazeItem());

            // On récupère l'instance du cercle d'attente

            if (this.Ring != null)
            {
                this._ringRenderer = this.Ring.GetComponent<Renderer>();
                this._ringRenderer.material.SetFloat("_Range", 0f);

                this.Ring.SetActive(false);
            }
        }

        /// <summary>
        /// Mise à jour de la logique
        /// </summary>
        private void Update()
        {
            // Connexion au Touchpad

            if (!this._isInitialized && GVRTouchpadManager.Default != null)
            {
                GVRTouchpadManager.Default.UserInteracted += this.OnTouchpadClick;
                this._isInitialized = true;
            }

            // Récumération rayon de vue VR

            Ray ray = new Ray(this.CameraController.centerEyeAnchor.position, CameraController.centerEyeAnchor.forward);

            // On replace le Gaze

            this.gameObject.transform.position = ray.GetPoint(this.PointerDistance);
            this.gameObject.transform.LookAt(this.CameraController.centerEyeAnchor.transform);

            // On va chercher des items interactifs

            RaycastHit hit;

            bool detection = false;

            if (this.Active)
                detection = Physics.Raycast(ray, out hit, this.Range);
            else
                detection = Physics.Raycast(ray, out hit);

            if (detection)
            {
                // Est un élément géré

                IGVRInteraction item = this.IsInteractiveItem(hit.collider);

                // Si pas le même on désélectionne tous

                if (item != this._current && this._current != null)
                    this._current.RaiseLeave();

                // Affectation de la nouvelle instance

                if (item != null && hit.collider != null)
                {
                    this._current = item;
                    this._current.SetInteractiveItem(hit.collider);
                }
            }
            else
            {
                if (this._current != null)
                {
                    this._current.RaiseLeave();
                    this._current = null;
                }
            }
                
            // Gestion du ring

            this.ProcessRing();
        }

        #endregion
    }
}
