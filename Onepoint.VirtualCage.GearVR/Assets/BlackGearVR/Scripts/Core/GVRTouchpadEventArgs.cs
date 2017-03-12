using System;
using UnityEngine;

namespace BlackGearVR.Core
{
    /// <summary>
    /// Classe d'argument pour un événement du Touchpad
    /// </summary>
    public class GVRTouchpadEventArgs : EventArgs
    {
        #region Propriétés

        /// <summary>
        /// Obtient la distance parcourue
        /// </summary>
        public Vector3 Move
        {
            get;
            private set;
        }

        /// <summary>
        /// Obtient le type de l'événement
        /// </summary>
        public GVRTouchpadEvents Type
        {
            get;
            private set;
        }

        #endregion

        #region Constructeurs

        /// <summary>
        /// Constructeur de la classe
        /// </summary>
        /// <param name="type">Type de l'événement</param>
        /// <param name="move">Distance parcourue</param>
        public GVRTouchpadEventArgs(GVRTouchpadEvents type, Vector3 move)
        {
            this.Move = move;
            this.Type = type;
        }

        #endregion
    }
}
