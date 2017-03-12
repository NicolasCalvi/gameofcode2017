using UnityEngine;

namespace BlackGearVR.Core
{
    /// <summary>
    /// Traite les interactions pour un objet de type GazeItem
    /// </summary>
    public sealed class GVRInteractionGazeItem : IGVRInteraction
    {
        #region Propriétés

        /// <summary>
        /// Obtient l'instance du GVRGazeItem en cour de sélection
        /// </summary>
        public GVRGazeItem Current
        {
            get
            {
                return (this._item);
            }
        }

        #endregion

        #region Variables d'instances

        private GVRGazeItem _item;

        #endregion

        #region Constructeurs

        /// <summary>
        /// Constructeur de la classe
        /// </summary>
        public GVRInteractionGazeItem()
        {
        }

        #endregion

        #region Fonctions publiques

        /// <summary>
        /// Récupère les options de l'élément si il en a
        /// </summary>
        /// <returns>Instance des options, FALSE sinon</returns>
        public GVRGazeCursorOptions GetOptions()
        {
            if (this._item == null)
                return (null);

            return (this._item.Options);
        }

        /// <summary>
        /// Vérifie si cet objet est concerné par ce manager
        /// </summary>
        /// <param name="collider">Instance de l'objet de collision</param>
        /// <returns>TRUE est concerné, FALSE sinon</returns>
        public bool IsInteractiveItem(Collider collider)
        {
            // Récupération de l'instance

            GVRGazeItem item = collider.GetComponent<GVRGazeItem>();

            // On retourne si une instance de GVRGazeItem a été trouvé

            return (item != null);
        }

        /// <summary>
        /// Effectue l'interaction avec le Touchpad
        /// </summary>
        /// <param name="type">Type d'interaction</param>
        /// <param name="movement">Longueur du mouvement</param>
        public void ProcessTouchpad(GVRTouchpadEvents type, Vector3 movement)
        {
            // Vérification

            if (this._item == null)
                return;

            // Traitement du message

            switch (type)
            {
                case (GVRTouchpadEvents.Touch):
                    this._item.RaiseClick(true);
                    break;

                case (GVRTouchpadEvents.SwipeDown):
                    this._item.RaiseSwipeDown();
                    break;

                case (GVRTouchpadEvents.SwipeLeft):
                    this._item.RaiseSwipeLeft();
                    break;

                case (GVRTouchpadEvents.SwipeRight):
                    this._item.RaiseSwipeRight();
                    break;

                case (GVRTouchpadEvents.SwipeUp):
                    this._item.RaiseSwipeUp();
                    break;
            }
        }

        /// <summary>
        /// Informe l'objet courant qu'il est actif
        /// </summary>
        public void RaiseEnter()
        {
            // Vérification

            if (this._item != null)
                this._item.RaiseEnter();

            // On réinitialise le curseur

            GVRGazeCursorOptions options = this.GetOptions();

            if (options != null)
                options.InitializeEnter();
        }

        /// <summary>
        /// Informe l'objet courant qu'il n'est plus actif
        /// </summary>
        public void RaiseLeave()
        {
            // Vérification

            if (this._item != null)
                this._item.RaiseLeave();

            this._item = null;
        }

        /// <summary>
        /// Affecte un objet interactif a ce manager
        /// </summary>
        /// <param name="collider">Instance de l'objet de collision</param>
        public void SetInteractiveItem(Collider collider)
        {
            // Récupération de l'instance

            GVRGazeItem item = collider.GetComponent<GVRGazeItem>();

            // On quitte l'ancien

            if (item != this._item)
                this.RaiseLeave();

            // Elément trouvé ?

            if (item != null && item != this._item)
            {
                // Affectation de l'instance

                this._item = item;

                // On notifie de l'entrée

                this.RaiseEnter();
            }
        }

        #endregion
    }
}
