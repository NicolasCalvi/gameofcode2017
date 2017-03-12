using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTapListener : AirTapListener
{
    #region Variables privées

    private PlayMakerFSM _fsm;

    #endregion

    #region Variable unity

    public GameObject _mailBox;

    #endregion

    #region AirTapListener implementation
    public override void OnAirTap()
    {
        Vector3 tDeplacement = Vector3.Lerp(gameObject.transform.position, _mailBox.transform.position, 10f);
        this.gameObject.transform.position = tDeplacement;
        this._fsm.Fsm.Event("Clicked");
        this._mailBox.GetComponent<MailBoxBehaviour>().ReceiveObject(this.gameObject.name);

    }
    #endregion

    #region Fontions privées

    /// <summary>
    /// Démarrage du behavior
    /// </summary>
    private void Start()
    {
        this._fsm = GetComponent<PlayMakerFSM>();
    }

    void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    Vector3 tDeplacement = Vector3.Lerp(gameObject.transform.position, _mailBox.transform.position, 10f);
        //    //this.gameObject.transform.position = this.gameObject.transform.position + Vector3.up;
        //    this.gameObject.transform.position = tDeplacement;
        //    this._fsm.Fsm.Event("Clicked");
        //}
    }

    #endregion

}
