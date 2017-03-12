using BlackGearVR.Core;
using System;
using UnityEngine;

namespace BlackGearVR
{
    /// <summary>
    /// Manager pour le Touchpad du GearVR
    /// </summary>
    public class GVRTouchpadManager : MonoBehaviour
    {
        #region Evénements

        public event EventHandler<GVRTouchpadEventArgs> UserInteracted;

        #endregion

        #region Singleton

        public static GVRTouchpadManager Default = null;

        #endregion

        #region Variables Unity

        public float Sensibility = 100.0f;

        #endregion

        #region Variables d'intances

        private Vector3 _move;

        #endregion

        #region Fonctions publiques

        /// <summary>
        /// Simule une action sur le touchpad
        /// </summary>
        /// <param name="type">Type d'événément</param>
        public void VirtualAction(GVRTouchpadEvents type)
        {
            this.VirtualAction(type, Vector3.zero);
        }

        /// <summary>
        /// Simule une action sur le touchpad
        /// </summary>
        /// <param name="type">Type de l'événement</param>
        /// <param name="move">Déplacement simulé</param>
        public void VirtualAction(GVRTouchpadEvents type, Vector3 move)
        {
            this.RaiseUserInteracted(type, move);
        }

        #endregion

        #region Fonctions privées

        /// <summary>
        /// Détermine le mouvement et son type
        /// </summary>
        /// <param name="move">Mouvement total</param>
        private void HandleInput(Vector3 move)
        {
            if (move.magnitude < this.Sensibility)
                this.RaiseUserInteracted(GVRTouchpadEvents.Touch, move);
            else
            {
                move.Normalize();

                if (Mathf.Abs(move.x) > Mathf.Abs(move.y))
                {
                    if (move.x > 0.0f)
                        this.RaiseUserInteracted(GVRTouchpadEvents.SwipeLeft, move);
                    else
                        this.RaiseUserInteracted(GVRTouchpadEvents.SwipeRight, move);
                }
                else
                {
                    if (move.y > 0.0f)
                        this.RaiseUserInteracted(GVRTouchpadEvents.SwipeDown, move);
                    else
                        this.RaiseUserInteracted(GVRTouchpadEvents.SwipeUp, move);
                }
            }
        }

        /// <summary>
        /// Envoi l'événement d'interaction
        /// </summary>
        /// <param name="type">Type d'interaction</param>
        /// <param name="move">Mouvement de l'interaction</param>
        private void RaiseUserInteracted(GVRTouchpadEvents type, Vector3 move)
        {
            if (this.UserInteracted != null)
                this.UserInteracted(this, new GVRTouchpadEventArgs(type, move));
        }

        #endregion

        #region Fonctions Unity

        /// <summary>
        /// Initialisation du gameobject
        /// </summary>
        private void Start()
        {
            GVRTouchpadManager.Default = this;
        }

        /// <summary>
        /// Mise à jour de la logique
        /// </summary>
        private void Update()
        {
            // Tentative de détection du mouvement

            if (Input.GetMouseButtonDown(0))
                this._move = Input.mousePosition;
            else if (Input.GetMouseButtonUp(0))
            {
                this._move -= Input.mousePosition;
                this.HandleInput(this._move);
            }
        }

        #endregion
    }
}
