using UnityEngine;

public class GazeCursor : MonoBehaviour
{
    #region UNITY ATTRIBUTES

    public Vector3 _defaultPosition;
    public Vector3 _defaultScale;
    public Vector3 _raycastScale;
    public LayerMask _masque;

    #endregion

    #region ATTRIBUTES
    private MeshRenderer _meshRenderer;
    #endregion

    #region UNITY METHODS
    // Use this for initialization
    void Start()
    {
        // Grab the mesh renderer that's on the same object as this script.
        _meshRenderer = this.gameObject.GetComponentInChildren<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // Do a raycast into the world based on the user's
        // head position and orientation.
        var tHeadPosition = Camera.main.transform.position;
        var tGazePosition = Camera.main.transform.forward;

        RaycastHit tHitInfo;

        if (Physics.Raycast(tHeadPosition, tGazePosition, out tHitInfo ,300f, _masque))
        {
            // If the raycast hit a hologram...
            // Display the cursor mesh.
            _meshRenderer.enabled = true;

            // Move thecursor to the point where the raycast hit.
            this.transform.localScale = _raycastScale;

            this.transform.position = tHitInfo.point;

            // Rotate the cursor to hug the surface of the hologram.
            // this.transform.rotation = Quaternion.FromToRotation(Vector3.up, tHitInfo.normal);
            this.transform.rotation = Quaternion.FromToRotation(Vector3.forward, tHitInfo.normal);
        }
        else
        {
            // If the raycast did not hit a hologram, replace the cursor at its default location.
            this.transform.localPosition = _defaultPosition;
            this.transform.localRotation = Quaternion.identity;
            this.transform.localScale = _defaultScale;
        }
    }
    #endregion
}