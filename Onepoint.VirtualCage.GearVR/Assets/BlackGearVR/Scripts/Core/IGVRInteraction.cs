using UnityEngine;

namespace BlackGearVR.Core
{
    /// <summary>
    /// Interface pour l'interaction avec le gaze
    /// </summary>
    public interface IGVRInteraction
    {
        #region Fonctions

        /// <summary>
        /// Récupère les options de l'élément si il en a
        /// </summary>
        /// <returns>Instance des options, FALSE sinon</returns>
        GVRGazeCursorOptions GetOptions();

        /// <summary>
        /// Vérifie si cet objet est concerné par ce manager
        /// </summary>
        /// <param name="collider">Instance de l'objet de collision</param>
        /// <returns>TRUE est concerné, FALSE sinon</returns>
        bool IsInteractiveItem(Collider collider);

        /// <summary>
        /// Effectue l'interaction avec le Touchpad
        /// </summary>
        /// <param name="type">Type d'interaction</param>
        /// <param name="movement">Longueur du mouvement</param>
        void ProcessTouchpad(GVRTouchpadEvents type, Vector3 movement);

        /// <summary>
        /// Informe l'objet courant qu'il est actif
        /// </summary>
        void RaiseEnter();

        /// <summary>
        /// Informe l'objet courant qu'il n'est plus actif
        /// </summary>
        void RaiseLeave();

        /// <summary>
        /// Affecte un objet interactif a ce manager
        /// </summary>
        /// <param name="collider">Instance de l'objet de collision</param>
        void SetInteractiveItem(Collider collider);

        #endregion
    }
}
