using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR.WSA.Input;

public class GestureManager : MonoBehaviour {

    #region Variables Unity
    public LayerMask _mask;
    #endregion

    #region Variables d'instance
    private GestureRecognizer _gestureRecognizer;
    #endregion

    #region Fonctions Unity

    // Use this for initialization
    void Start()
    {
        _gestureRecognizer = new GestureRecognizer();
        _gestureRecognizer.TappedEvent += OnTapEvent;
        _gestureRecognizer.StartCapturingGestures();
    }

    private void OnTapEvent(InteractionSourceKind source, int tapCount, Ray headRay)
    {
        var tHeadPosition = Camera.main.transform.position;
        var tGazePosition = Camera.main.transform.forward;

        RaycastHit tHit;
        if (Physics.Raycast(tHeadPosition, tGazePosition, out tHit/*, 300f, _mask*/))
        {
            Debug.Log(tHit.transform.gameObject.name);
            AirTapListener tListener = tHit.collider.GetComponent<AirTapListener>();
            if(tListener != null)
            {
                tListener.OnAirTap();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    #endregion
}
