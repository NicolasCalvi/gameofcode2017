using UnityEngine;

namespace BlackGearVR
{
    /// <summary>
    /// Définit les options pour le GazeItem
    /// </summary>
    public sealed class GVRGazeCursorOptions : MonoBehaviour
    {
        #region Variables Unity

        [Header("Selection Mode")]
        public bool TouchPadActive = true;
        public bool RingTimerActive = false;

        [Header("Options")]
        public float ResetTime = 0.0f;
        public float SelectionTime = 2000.0f;

        #endregion

        #region Variables d'instances

        private bool _gazeOver;
        private bool _ringFired;
        private float _ringStep;
        private float _selectionTimer;
        private float _resetTimer;

        #endregion

        #region Fonctions publiques

        /// <summary>
        /// Renvoi le niveau de sélection de l'anneau
        /// </summary>
        /// <returns>Niveau de séletion entre 0 et 1</returns>
        public float GetRingStep()
        {
            return (this._ringStep);
        }

        /// <summary>
        /// Initialise les options
        /// </summary>
        public void InitializeEnter()
        {
            this.ClearTimer(true);
            this._ringFired = false;
        }

        /// <summary>
        /// Retoure si l'object peut être cliquer après un reset
        /// </summary>
        /// <returns>TRUE est clickable, FALSE sinon</returns>
        public bool IsResetReady()
        {
            if (this.ResetTime <= 0 && this._ringFired)
                return (false);

            return (this._resetTimer <= 0.0f);
        }

        /// <summary>
        /// A exécuter quand le gaze détecte un click dans l'objet
        /// </summary>
        /// <param name="fromTouchPad">TRUE vient du TouchPad, FALSE du Gaze Timer</param>
        /// <returns>TRUE l'événement peut être levé, FALSE sinon</returns> 
        public bool RaiseClick(bool fromTouchPad)
        {
            // Est ce que des options existes

            if ((fromTouchPad && this.TouchPadActive) || (!fromTouchPad && this.RingTimerActive))
                return (true);

            // Pas de clic

            return (false);
        }

        /// <summary>
        /// A exécuter quand le gaze entre dans l'objet
        /// </summary>
        /// <returns>TRUE l'événement peut être levé, FALSE sinon</returns> 
        public bool RaiseEnter()
        {
            this._ringFired = false;
            this.ClearTimer(true);

            return (true);
        }

        /// <summary>
        /// A exécuter quand le gaze quitte dans l'objet
        /// </summary>
        /// <returns>TRUE l'événement peut être levé, FALSE sinon</returns> 
        public bool RaiseLeave()
        {
            this.ClearTimer(false);
            return (true);
        }

        /// <summary>
        /// A exécuter quand le gaze détecte un swipe down dans l'objet
        /// </summary>
        /// <returns>TRUE l'événement peut être levé, FALSE sinon</returns> 
        public bool RaiseSwipeDown()
        {
            if (this.TouchPadActive)
                return (true);

            return (false);
        }

        /// <summary>
        /// A exécuter quand le gaze détecte un swipe left dans l'objet
        /// </summary>
        /// <returns>TRUE l'événement peut être levé, FALSE sinon</returns> 
        public bool RaiseSwipeLeft()
        {
            if (this.TouchPadActive)
                return (true);

            return (false);
        }

        /// <summary>
        /// A exécuter quand le gaze détecte un swipe right dans l'objet
        /// </summary>
        /// <returns>TRUE l'événement peut être levé, FALSE sinon</returns> 
        public bool RaiseSwipeRight()
        {
            if (this.TouchPadActive)
                return (true);

            return (false);
        }

        /// <summary>
        /// A exécuter quand le gaze détecte un swipe up dans l'objet
        /// </summary>
        /// <returns>TRUE l'événement peut être levé, FALSE sinon</returns> 
        public bool RaiseSwipeUp()
        {
            if (this.TouchPadActive)
                return (true);

            return (false);
        }

        #endregion

        #region Fonctions privées

        /// <summary>
        /// Réinitialise les informations de timer
        /// </summary>
        /// <param name="gazeOver">TRUE le Gaze est sur l'objet, FALSE sinon</param>
        private void ClearTimer(bool gazeOver)
        {
            this._gazeOver = gazeOver;
            this._resetTimer = 0.0f;
            this._selectionTimer = 0.0f;
            this._ringStep = 0.0f;
        }

        #endregion

        #region Fonctions Unity

        /// <summary>
        /// Initialisation gameobject
        /// </summary>
        private void Start()
        {
            this._ringFired = false;
            this.ClearTimer(false);
        }

        /// <summary>
        /// Mise à jour de la logique
        /// </summary>
        private void Update()
        {
            // Vérification update du timer ring

            if (!this._gazeOver || !this.RingTimerActive)
                return;

            if (this.ResetTime <= 0 && this._ringFired)
                return;

            // En sélection ou en attente

            if (this.IsResetReady())
            {
                // Mise à jour de la variable temps

                this._selectionTimer += Time.deltaTime * 1000f;

                // Mise à jour du Gaze Timer

                this._ringStep = this._selectionTimer / this.SelectionTime;
                this._ringStep = Mathf.Clamp(this._ringStep, 0.0f, 1.0f);

                // Vérification de la sélection

                if (this._ringStep >= 1.0f)
                {
                    if (this.RaiseClick(false) && GVRTouchpadManager.Default != null)
                    {
                        this._ringFired = true;
                        GVRTouchpadManager.Default.VirtualAction(Core.GVRTouchpadEvents.Touch);
                    }

                    this._resetTimer = this.ResetTime;
                }
            }
            else
            {
                // Mise à jour de la variable temps

                this._resetTimer -= Time.deltaTime * 1000f;

                // Vérification fin Timer reset

                if (this._resetTimer <= 0.0f)
                    this.ClearTimer(true);
            }

            // Vérifiation fin d'action ring

            if (this.ResetTime <= 0 && this._ringFired)
                this.ClearTimer(true);
        }

        #endregion
    }
}
