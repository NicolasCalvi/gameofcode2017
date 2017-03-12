using UnityEngine;
using UnityEngine.Events;

namespace BlackGearVR
{
    /// <summary>
    /// Définit un objet interactif avec le Gaze
    /// </summary>
    public class GVRGazeItem : MonoBehaviour
    {
        #region Variables Unity

        [Header("Dependencies")]
        public GVRGazeCursorOptions Options;

        [Header("Events")]
        public UnityEvent Click;
        public UnityEvent Enter;
        public UnityEvent Leave;
        public UnityEvent SwipeDown;
        public UnityEvent SwipeLeft;
        public UnityEvent SwipeRight;
        public UnityEvent SwipeUp;

        #endregion

        #region Fonctions publiques

        /// <summary>
        /// A exécuter quand le gaze détecte un click dans l'objet
        /// </summary>
        /// <param name="fromTouchPad">TRUE vient du TouchPad, FALSE du Gaze Timer</param>
        public void RaiseClick(bool fromTouchPad)
        {
            bool active = true;

            if (this.Options != null)
                active = this.Options.RaiseClick(fromTouchPad);

            if (active)
                this.Click.Invoke();
        }

        /// <summary>
        /// A exécuter quand le gaze entre dans l'objet
        /// </summary>
        public void RaiseEnter()
        {
            bool active = true;

            if (this.Options != null)
                active = this.Options.RaiseEnter();

            if (active)
                this.Enter.Invoke();
        }

        int test = 0;

        /// <summary>
        /// A exécuter quand le gaze quitte dans l'objet
        /// </summary>
        public void RaiseLeave()
        {
            bool active = true;

            if (this.Options != null)
                active = this.Options.RaiseLeave();

            test++;

            if (test > 10)
                return;

            if (active)
                this.Leave.Invoke();
        }

        /// <summary>
        /// A exécuter quand le gaze détecte un swipe down dans l'objet
        /// </summary>
        public void RaiseSwipeDown()
        {
            bool active = true;

            if (this.Options != null)
                active = this.Options.RaiseSwipeDown();

            if (active)
                this.SwipeDown.Invoke();
        }

        /// <summary>
        /// A exécuter quand le gaze détecte un swipe left dans l'objet
        /// </summary>
        public void RaiseSwipeLeft()
        {
            bool active = true;

            if (this.Options != null)
                active = this.Options.RaiseSwipeLeft();

            if (active)
                this.SwipeLeft.Invoke();
        }

        /// <summary>
        /// A exécuter quand le gaze détecte un swipe right dans l'objet
        /// </summary>
        public void RaiseSwipeRight()
        {
            bool active = true;

            if (this.Options != null)
                active = this.Options.RaiseSwipeRight();

            if (active)
                this.SwipeRight.Invoke();
        }

        /// <summary>
        /// A exécuter quand le gaze détecte un swipe up dans l'objet
        /// </summary>
        public void RaiseSwipeUp()
        {
            bool active = true;

            if (this.Options != null)
                active = this.Options.RaiseSwipeUp();

            if (active)
                this.SwipeUp.Invoke();
        }

        #endregion
    }
}
