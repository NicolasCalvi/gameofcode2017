using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR.WSA.Input;

public class CageInitializer : AirTapListener {

    #region Variable Unity
    public Vector3 _cageDecalage;
    #endregion

    #region Variables d'instance
    private bool _placing;
    private Quaternion _initialRotation;
  //  private GestureRecognizer _recognizer;
    #endregion

    #region Fonctions Unity

    private void Start()
    {
        _placing = true;
        _initialRotation = this.gameObject.transform.rotation;
        //SpatialMapping.Instance.DrawVisualMeshes = true;

        //_recognizer = new GestureRecognizer();
        //_recognizer.TappedEvent += Placement_Callback;
        //_recognizer.StartCapturingGestures();
    }

   /* private void Placement_Callback(InteractionSourceKind source, int tapCount, Ray headRay)
    {
        OnPlace();
    }*/

    // Update is called once per frame
    void Update()
    {
        if (_placing)
        {
            //// Do a raycast into the world that will only hit the Spatial Mapping mesh.
            var tHeadPosition = Camera.main.transform.position;
            var tGazePosition = Camera.main.transform.forward;

            Ray tRay = new Ray(tHeadPosition, tGazePosition);

            RaycastHit tHitInfo;
            if (Physics.Raycast(tRay, out tHitInfo, 300f, (int)(1 << 31)))
            {
                // Move this object's parent object to
                // where the raycast hit the Spatial Mapping mesh.
                this.gameObject.transform.position = tHitInfo.point;
                this.gameObject.transform.Translate(_cageDecalage,Camera.main.transform);


            }else
            {
                float tLength = Vector3.Distance(this.gameObject.transform.position, tHeadPosition);
                Vector3 tNewPosition = tHeadPosition+ (tGazePosition.normalized * tLength);
                this.gameObject.transform.position = tNewPosition;
            }
            // Rotate this object's parent object to face the user.
            Quaternion tToQuat = Camera.main.transform.localRotation;
            tToQuat.x = 0;
            tToQuat.z = 0;
            this.gameObject.transform.rotation = _initialRotation *  tToQuat;
        }

        if (Input.GetMouseButtonDown(0))
        {
            OnPlace();
        }
    }
    #endregion

    #region Fonctions privées
    void OnPlace()
    {
        // On each Select gesture, toggle whether the user is in placing mode.
        _placing = false;
        gameObject.GetComponent<PlayMakerFSM>().Fsm.Event("cagePlaced");
    }
    #endregion

    #region AirTapListener implementation
    public override void OnAirTap()
    {
        OnPlace();
    }
    #endregion
}
